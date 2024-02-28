using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace com.daxode.imgui
{
    struct EnumValueData
    {
        public string calc_value;
        public string name;
        public string value;
    }
    
    struct StructsAndEnums
    {
        public Dictionary<string, EnumValueData[]> enums;
        public Dictionary<string, string> enumtypes;
        public Dictionary<string, string> locations;
        public Dictionary<string, StructFieldData[]> structs;
        public Dictionary<string, TemplatedStructFieldData[]> templated_structs;
        public Dictionary<string, Dictionary<string, bool>> templates_done;
        public Dictionary<string, string> typenames;
    }
    
    struct StructFieldData
    {
        public string name;
        public string template_type;
        public string type;
    }

    struct TemplatedStructFieldData
    {
        public string name;
        public string type;
    }
    
    public static class GenerateWrappersFromJSON
    {
        const bool k_IsDebug = true;
        
        [MenuItem("Tools/ImGUI/Generate Wrappers")]
        public static void GenerateWrappers()
        {
            // Read the structs and enums
            var structsAndEnumsReader = new JsonTextReader(new StreamReader(Path.GetFullPath("Packages/com.daxode.imgui/cimgui~/generator/output/structs_and_enums.json")));
            var structsAndEnums = new JsonSerializer().Deserialize<StructsAndEnums>(structsAndEnumsReader);
            var sourceOutputPath = Path.GetFullPath("Packages/com.daxode.imgui/GeneratedWrappers.cs");
            
            // Write the source file
            using (var sourceWriter = new StreamWriter(sourceOutputPath))
            {
                sourceWriter.WriteLine("using System;");
                sourceWriter.WriteLine("namespace com.daxode.imgui.generated");
                sourceWriter.WriteLine("{");
                WriteEnums(sourceWriter, structsAndEnums);
                sourceWriter.WriteLine("}");
            }
            
            // Log
            Debug.Log("Generated Wrappers");
        }

        // Writes the enums
        static void WriteEnums(StreamWriter sourceWriter, StructsAndEnums structsAndEnums)
        {
            var oldToNewTypeName = new Dictionary<string, string>();
            var previousIsPrivate = false;
            foreach (var (enumRawName, enumValuesRaw) in structsAndEnums.enums)
            {
                var enumValues = new List<EnumValueData>(enumValuesRaw);
                var enumName = enumRawName.EndsWith('_') ? enumRawName[..^1] : enumRawName;
                
                // Check if the enum is private every enum that ends with Private is followed by a public enum with the same name
                if (enumName.EndsWith("Private"))
                {
                    previousIsPrivate = true;
                    continue;
                }
                if (previousIsPrivate)
                {
                    enumValues.AddRange(structsAndEnums.enums[enumName + "Private_"]);
                    previousIsPrivate = false;
                }
                
                // Write Enum start
                if (enumName.EndsWith("Flags"))
                    sourceWriter.WriteLine($"\t[Flags]");
                sourceWriter.Write($"\tpublic enum {enumName}");
                if (structsAndEnums.enumtypes.TryGetValue(enumName, out var enumType))
                    sourceWriter.Write($" : {enumType}");
                sourceWriter.WriteLine();
                sourceWriter.WriteLine("\t{");
                
                // Write Enum values
                for (var i = 0; i < enumValues.Count; i++)
                {
                    if (i >= enumValuesRaw.Length) // Mark the private enum values
                        sourceWriter.WriteLine("\t\t/// <remarks> This is a private member </remarks>");
                    WriteEnumValue(sourceWriter, enumValues[i], enumName, oldToNewTypeName);
                }
                
                // Write Enum end
                sourceWriter.WriteLine("\t}\n");
            }
        }

        // Writes the enum value
        static void WriteEnumValue(TextWriter sourceWriter, EnumValueData enumValueData, string owningEnumName, Dictionary<string, string> oldToNewTypeName)
        {
            // check if enum value's name is part of the current enum
            if (!enumValueData.name.StartsWith(owningEnumName))
            {
                LogHiddenEnumValueSkip(enumValueData, owningEnumName);
                return;
            }

            var enumValueName = enumValueData.name[(owningEnumName.Length + 1)..];

            // if _ is present then it is a hidden enum value (except for the last character)
            var indexToUnderscore = enumValueName.IndexOf('_');
            if (indexToUnderscore != -1 && indexToUnderscore != enumValueName.Length - 1)
            {
                LogHiddenEnumValueSkip(enumValueData, owningEnumName);
                return;
            }

            enumValueName = enumValueName!.EndsWith('_') ? enumValueName[..^1] : enumValueName;
            if (enumValueName[0] >= '0' && enumValueName[0] <= '9')
                enumValueName = "No" + enumValueName;
            oldToNewTypeName[enumValueData.name] = $"{owningEnumName}.{enumValueName}";

            var value = enumValueData.value;

            // loop binary or values if one
            if (value.Contains(" | "))
            {
                var values = value.Split(" | ");
                sourceWriter.Write($"\t\t{enumValueName} = ");
                sourceWriter.Write(GetEnumNameFromRaw(values[0], owningEnumName, oldToNewTypeName));
                for (var i = 1; i < values.Length; i++)
                {
                    sourceWriter.Write(" | ");
                    sourceWriter.Write(GetEnumNameFromRaw(values[i], owningEnumName, oldToNewTypeName));
                }
                sourceWriter.WriteLine(",");
            }
            else
            {
                value = GetEnumNameFromRaw(value, owningEnumName, oldToNewTypeName);
                sourceWriter.WriteLine($"\t\t{enumValueName} = {value},");
            }
        }

        // Converts the raw enum name to the new enum name
        static string GetEnumNameFromRaw(string currentEnumValue, string owningEnumName, IReadOnlyDictionary<string, string> oldToNewTypeName)
        {
            // if the enum value is already in the dictionary then return it
            if (oldToNewTypeName.TryGetValue(currentEnumValue, out var existingEnumName))
                return existingEnumName;
            
            if (currentEnumValue.StartsWith(owningEnumName + "_"))
                currentEnumValue = currentEnumValue[(owningEnumName.Length + 1)..];
            currentEnumValue = currentEnumValue!.EndsWith('_') ? currentEnumValue[..^1] : currentEnumValue;
            var members = currentEnumValue.Split('_');
            var lastMember = members[^1];
            if (lastMember[0] >= '0' && lastMember[0] <= '9' && members.Length > 1)
                return string.Join('.', members[..^1]) + ".No" + lastMember;

            return currentEnumValue.Replace('_', '.');
        }

        static void LogHiddenEnumValueSkip(EnumValueData enumValueData, string enumName)
        {
            if (k_IsDebug)
            {
                var actualEnumName = string.Join("", enumValueData.name.Split('_')[..^1]);
                Debug.Log($"Enum value <color=#00FF00>{enumValueData.name}</color> is not part of the current enum <color=#FF0000>{enumName}</color>. Actual enum name is <color=#1155FF>{actualEnumName}</color>. Skipping...");
            }
        }
    }
}
