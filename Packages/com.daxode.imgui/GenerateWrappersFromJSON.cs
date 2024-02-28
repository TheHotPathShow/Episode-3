using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace com.daxode.imgui
{
    struct EnumData
    {
        public string calc_value;
        public string name;
        public string value;
    }
    
    struct StructsAndEnums
    {
        public Dictionary<string, EnumData[]> enums;
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
            var structEnumTextPath = Path.GetFullPath("Packages/com.daxode.imgui/cimgui~/generator/output/structs_and_enums.json");
            var jsonReader = new JsonTextReader(new StreamReader(structEnumTextPath));
            var jsonSerializer = new JsonSerializer();
            var structsAndEnums = jsonSerializer.Deserialize<StructsAndEnums>(jsonReader);
            
            var sourceOutputPath = Path.GetFullPath("Packages/com.daxode.imgui/GeneratedWrappers.cs");
            var sourceWriter = new StreamWriter(sourceOutputPath);
            sourceWriter.WriteLine("using System;");
            sourceWriter.WriteLine("namespace com.daxode.imgui.generated");
            sourceWriter.WriteLine("{");
            var oldToNewTypeName = new Dictionary<string, string>();
            var enumValuesHidingInsideOtherEnums = new List<string>();
            var previousIsPrivate = false;
            foreach (var (enumRawName, enumValuesRaw) in structsAndEnums.enums)
            {
                var enumValues = new List<EnumData>(enumValuesRaw);
                var enumName = enumRawName.EndsWith('_') ? enumRawName[..^1] : enumRawName;
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
                
                if (enumName.EndsWith("Flags"))
                    sourceWriter.WriteLine($"\t[Flags]");
                sourceWriter.Write($"\tpublic enum {enumName}");
                if (structsAndEnums.enumtypes.TryGetValue(enumName, out var enumType))
                    sourceWriter.Write($" : {enumType}");
                sourceWriter.WriteLine();
                sourceWriter.WriteLine("\t{");
                for (var indexToEnumValue = 0; indexToEnumValue < enumValues.Count; indexToEnumValue++)
                {
                    var enumData = enumValues[indexToEnumValue];
                    if (indexToEnumValue >= enumValuesRaw.Length) 
                        sourceWriter.WriteLine("\t\t/// <remarks> This is a private member </remarks>");

                    // check if enum value's name is part of the current enum
                    if (!enumData.name.StartsWith(enumName))
                    {
                        if (k_IsDebug)
                        {
                            var actualEnumName = string.Join("", enumData.name.Split('_')[..^1]);
                            Debug.Log($"Enum value <color=#00FF00>{enumData.name}</color> is not part of the current enum <color=#FF0000>{enumName}</color>. Actual enum name is <color=#1155FF>{actualEnumName}</color>. Skipping...");
                        }
                        enumValuesHidingInsideOtherEnums.Add(enumData.name);
                        continue;
                    }

                    var enumValueName = enumData.name[(enumName.Length + 1)..];

                    // if _ is present then it is a hidden enum value (except for the last character)
                    var indexToUnderscore = enumValueName.IndexOf('_');
                    if (indexToUnderscore != -1 && indexToUnderscore != enumValueName.Length - 1)
                    {
                        if (k_IsDebug)
                        {
                            var actualEnumName = string.Join("", enumData.name.Split('_')[..^1]);
                            Debug.Log($"Enum value <color=#00FF00>{enumData.name}</color> is not part of the current enum <color=#FF0000>{enumName}</color>. Actual enum name is <color=#1155FF>{actualEnumName}</color>. Skipping...");
                        }
                        enumValuesHidingInsideOtherEnums.Add(enumData.name);
                        continue;
                    }

                    enumValueName = enumValueName!.EndsWith('_') ? enumValueName[..^1] : enumValueName;
                    enumValueName = enumValueName.Split('_')[^1];
                    if (enumValueName[0] >= '0' && enumValueName[0] <= '9')
                        enumValueName = "No" + enumValueName;
                    oldToNewTypeName[enumData.name] = $"{enumName}.{enumValueName}";

                    var value = enumData.value;

                    // loop binary or values if one
                    if (value.Contains(" | "))
                    {
                        var values = value.Split(" | ");
                        sourceWriter.Write($"\t\t{enumValueName} = ");

                        var currentEnumValue = values[0];
                        if (oldToNewTypeName.TryGetValue(currentEnumValue, out var newEnumValue))
                            currentEnumValue = newEnumValue;
                        else
                        {
                            currentEnumValue = currentEnumValue.Replace(enumName + "_", "");
                            currentEnumValue = currentEnumValue!.EndsWith('_') ? currentEnumValue[..^1] : currentEnumValue;
                            currentEnumValue = currentEnumValue.Replace('_', '.');
                            if (currentEnumValue[0] >= '0' && currentEnumValue[0] <= '9')
                                currentEnumValue = "No" + currentEnumValue;
                        }

                        sourceWriter.Write(currentEnumValue);

                        for (var i = 1; i < values.Length; i++)
                        {
                            sourceWriter.Write(" | ");
                            currentEnumValue = values[i];
                            if (oldToNewTypeName.TryGetValue(currentEnumValue, out newEnumValue))
                                currentEnumValue = newEnumValue;
                            else
                            {
                                currentEnumValue = currentEnumValue.Replace(enumName + "_", "");
                                currentEnumValue = currentEnumValue!.EndsWith('_') ? currentEnumValue[..^1] : currentEnumValue;
                                currentEnumValue = currentEnumValue.Replace('_', '.');
                                var members = currentEnumValue.Split('.');
                                var lastMember = members[^1];
                                if (lastMember[0] >= '0' && lastMember[0] <= '9')
                                    currentEnumValue = string.Join('.', members[..^1]) + ".No" + lastMember;
                            }

                            sourceWriter.Write(currentEnumValue);
                        }

                        sourceWriter.WriteLine(",");
                    }
                    else
                    {
                        if (oldToNewTypeName.TryGetValue(value, out var newEnumValue))
                            value = newEnumValue;
                        else
                        {
                            value = value.Replace(enumName + "_", "");
                            value = value!.EndsWith('_') ? value[..^1] : value;
                            value = value.Replace('_', '.');
                        }

                        sourceWriter.WriteLine($"\t\t{enumValueName} = {value},");
                    }
                }

                sourceWriter.WriteLine("\t}\n");
            }
            sourceWriter.WriteLine("}");
            sourceWriter.Flush();
            sourceWriter.Close();
            Debug.Log("Generated Wrappers");
        }
    }
}
