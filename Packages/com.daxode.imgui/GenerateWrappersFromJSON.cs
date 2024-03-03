using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Dictionary<string, StructFieldData[]> templated_structs;
        public Dictionary<string, Dictionary<string, bool>> templates_done;
        public Dictionary<string, string> typenames;
    }
    
    struct StructFieldData
    {
        public string name;
        public string template_type;
        public string type;
        int size;
    }
    
    public static class GenerateWrappersFromJSON
    {
        const bool k_IsDebug = true;
        
        [MenuItem("Tools/Generate ImGui Wrappers")]
        public static void GenerateWrappers() => GenerateWrappers(false);
        
        [MenuItem("Tools/Generate ImGui Wrappers (with structs)")]
        public static void GenerateWrappersWithStructs() => GenerateWrappers(true);
        
        public static void GenerateWrappers(bool includeStructs)
        {
            // Read the structs and enums
            var structsAndEnumsReader = new JsonTextReader(new StreamReader(Path.GetFullPath("Packages/com.daxode.imgui/cimgui~/generator/output/structs_and_enums.json")));
            var typedefsDictionaryReader = new JsonTextReader(new StreamReader(Path.GetFullPath("Packages/com.daxode.imgui/cimgui~/generator/output/typedefs_dict.json")));
            var structsAndEnums = new JsonSerializer().Deserialize<StructsAndEnums>(structsAndEnumsReader);
            var typedefsDictionary = new JsonSerializer().Deserialize<Dictionary<string, string>>(typedefsDictionaryReader);
            var sourceOutputPath = Path.GetFullPath("Packages/com.daxode.imgui/GeneratedWrappers.cs");
            
            // remove typedefs where value starts with "struct "
            var listToRemove = new List<string>();
            foreach (var (key, value) in typedefsDictionary)
            {
                if (value.StartsWith("struct "))
                    listToRemove.Add(key);
                if (structsAndEnums.enums.ContainsKey(key+"_"))
                    listToRemove.Add(key);
            }
            foreach (var key in listToRemove)
                typedefsDictionary.Remove(key);
            foreach (var kvp in k_TypeMap)
                typedefsDictionary[kvp.Key] = kvp.Value;
            
            // log typedefs
            if (k_IsDebug)
            {
                foreach (var (key, value) in typedefsDictionary)
                    Debug.Log($"Typedef: {key} -> {value}");
            }
            
            // Write the source file
            using (var sourceWriter = new StreamWriter(sourceOutputPath))
            {
                // Write the header
                sourceWriter.WriteLine("using System;");
                sourceWriter.WriteLine("using System.Runtime.InteropServices;");
                
                
                // Write the namespace
                sourceWriter.WriteLine("namespace com.daxode.imgui");
                sourceWriter.WriteLine("{");
                
                // Constants
                sourceWriter.WriteLine("\tpublic static class ImGuiConstants");
                sourceWriter.WriteLine("\t{");
                sourceWriter.WriteLine("#if UNITY_64");
                sourceWriter.WriteLine("\t\tpublic const int PtrSize = 64/4;");
                sourceWriter.WriteLine("#else");
                sourceWriter.WriteLine("\t\tpublic const int PtrSize = 32/4;");
                sourceWriter.WriteLine("#endif");
                sourceWriter.WriteLine("\t}");
                sourceWriter.WriteLine();
                
                WriteEnums(sourceWriter, structsAndEnums);
                if (includeStructs)
                    WriteStructs(sourceWriter, structsAndEnums, typedefsDictionary);
                sourceWriter.WriteLine("}");
            }
            
            // Log
            Debug.Log("Generated Wrappers");
        }

        static readonly Dictionary<string, string> k_TypeMap = new Dictionary<string, string>
        {
            // basic types
            { "bool", "byte" },
            { "char", "byte" },
            { "double", "double" },
            { "float", "float" },
            { "int", "int" },
            { "long", "long" },
            { "short", "short" },
            { "unsigned char", "byte" },
            { "unsigned int", "uint" },
            { "unsigned long", "uint" },
            { "unsigned short", "ushort" },
            { "unsigned long long", "ulong" },
            { "signed char", "sbyte"},
            { "signed int", "int"},
            { "signed long", "int"},
            { "signed short", "short"},
            { "size_t", "System.UIntPtr" },
            { "ptrdiff_t", "System.IntPtr" },
            { "intptr_t", "System.IntPtr" },
            { "uintptr_t", "System.UIntPtr" },
            { "signed long long", "long" },
            { "void*", "System.IntPtr" },
            
            
            // mathematics
            { "ImVec1", "float" },
            { "ImVec2", "Unity.Mathematics.float2" },
            { "ImVec2ih", "Unity.Mathematics.int2" },
            { "ImVec4", "Unity.Mathematics.float4" },
            { "ImColor", "UnityEngine.Color" },
            { "ImTextureID", "UnityObjRef<UnityEngine.Texture2D>" },
            { "FILE", "void" },
            
            // { "ImU8", "byte"},
            // { "ImU16", "ushort"},
            // { "ImU32", "uint"},
            // { "ImWchar", "uint"},
            // { "ImWchar16", "uint"},
            // { "ImS8", "sbyte"},
            // { "ImS16", "short"},
            // { "ImS32", "int"},
            // { "ImS64", "long"},
        };
        
        static readonly Dictionary<string, int> k_SizeMap = new Dictionary<string, int>
        {
            // basic types
            { "bool", sizeof(bool) },
            { "byte", sizeof(byte) },
            { "double", sizeof(double) },
            { "float", sizeof(float) },
            { "int", sizeof(int) },
            { "long", sizeof(long) },
            { "short", sizeof(short) },
            { "uint", sizeof(uint) },
            { "ulong", sizeof(ulong) },
            { "ushort", sizeof(ushort) },
            { "sbyte", sizeof(sbyte) },
            { "void", 0 },
            // { "System.UIntPtr", sizeof(System.UIntPtr) },
            // { "System.IntPtr", sizeof(System.IntPtr) },
            
            // mathematics
            { "Unity.Mathematics.float2", 2*sizeof(float) },
            { "Unity.Mathematics.int2", 2*sizeof(int) },
            { "Unity.Mathematics.float4", 4*sizeof(float) },
            { "UnityEngine.Color", 4*sizeof(float) },
        };
        
        // force type to
        static readonly Dictionary<string, string> k_ForceStructMap = new Dictionary<string, string>
        {
            { "ImSpan", "ImSpan<T> where T : unmanaged" },
            { "ImPool", "ImPool<T> where T : unmanaged" },
            { "ImVector", "ImVector<T> where T : unmanaged" },
            { "ImChunkStream", "ImChunkStream<T> where T : unmanaged" },
            { "ImDrawDataBuilder_LayersArray", "ImDrawDataBuilder_LayersArray<T> where T : unmanaged" },
        };
        
        static readonly Dictionary<string, string> k_ForceTypeFieldMap = new Dictionary<string, string>
        {
            { "ImBitArray<ImGuiKey_NamedKey_COUNT,-ImGuiKey_NamedKey_BEGIN>", "ImBitArray" },
            { "ImVector<ImGuiDockRequest>", "ImVectorRaw" },
            { "ImVector<ImGuiDockNodeSettings>", "ImVectorRaw" },
            { "union { ImGuiInputEventMousePos MousePos; ImGuiInputEventMouseWheel MouseWheel; ImGuiInputEventMouseButton MouseButton; ImGuiInputEventMouseViewport MouseViewport; ImGuiInputEventKey Key; ImGuiInputEventText Text; ImGuiInputEventAppFocused AppFocused;}", "ImGuiInputEventUnion union" },
            { "union { int val_i; float val_f; void* val_p;}", "ImGuiStoragePairUnion union" },
            { "union { int BackupInt[2]; float BackupFloat[2];}", "ImGuiStyleModUnion union" },
        };
        
        static readonly HashSet<string> k_PublicTypes = new HashSet<string>
        {
            "ImVector",
            
        };
        static readonly HashSet<string> k_PartialTypes = new HashSet<string>
        {
            "ImVector",
            "ImDrawCmd",
            "ImFontAtlas",
            "ImGuiIO"
        };

        struct ValueArrayInfo
        {
            public string name;
            public string type;
            public string sizeText;
        }
        
        static void WriteStructs(TextWriter sourceWriter, StructsAndEnums structsAndEnums, Dictionary<string, string> typedefsDictionary)
        {
            foreach (var (structName, structFields) in structsAndEnums.structs.Concat(structsAndEnums.templated_structs))
            {
                if (typedefsDictionary.ContainsKey(structName))
                    continue;
                
                var valueArraysToAdd = new List<ValueArrayInfo>();
                var structNameWithForceType = structName;
                if (k_ForceStructMap.TryGetValue(structName, out var structNameWithForceTypeOverride))
                    structNameWithForceType = structNameWithForceTypeOverride;
                sourceWriter.WriteLine($"\t[StructLayout(LayoutKind.Sequential)]");
                sourceWriter.Write('\t');
                if (k_PublicTypes.Contains(structName))
                    sourceWriter.Write("public ");
                sourceWriter.Write("unsafe ");
                if (k_PartialTypes.Contains(structName))
                    sourceWriter.Write("partial ");
                sourceWriter.WriteLine($"struct {structNameWithForceType}");
                sourceWriter.WriteLine("\t{");
                foreach (var field in structFields)
                {
                    var fieldType = field.type;
                    var templateType = field.template_type;
                    var fieldTypePointerCount = 0;
                    var templateTypePointerCount = 0;
                    if (fieldType.StartsWith("const ")) 
                        fieldType = fieldType[6..];
                    
                    while (fieldType.EndsWith("*"))
                    {
                        fieldType = fieldType[..^1];
                        fieldTypePointerCount++;
                    }
                    if (templateType != null)
                    {
                        
                        while (templateType.EndsWith("*"))
                        {
                            templateType = templateType[..^1];
                            fieldType = fieldType[..^3];
                            templateTypePointerCount++;
                        }
                        fieldType = fieldType.EndsWith(templateType.Replace(' ', '_')) ? fieldType[..^(templateType.Length+1)] : fieldType;
                        ApplyTypedefs(typedefsDictionary, ref templateType);
                        while (templateType.EndsWith("*"))
                        {
                            templateType = templateType[..^1];
                            templateTypePointerCount++;
                        }
                    }
                    ApplyTypedefs(typedefsDictionary, ref fieldType);
                    while (fieldType.EndsWith("*"))
                    {
                        fieldType = fieldType[..^1];
                        fieldTypePointerCount++;
                    }
                    ApplyTypedefs(typedefsDictionary, ref fieldType);

                    // if function pointer
                    var functionPointerIndex = fieldType.IndexOf("(*)");
                    if (functionPointerIndex != -1)
                    {
                        var returnType = fieldType[..functionPointerIndex];
                        var parameters = fieldType[(functionPointerIndex + 4)..].Split(',');
                        var functionPointerTypeBuilder = new System.Text.StringBuilder();
                        functionPointerTypeBuilder.Append($"delegate* unmanaged[Cdecl]<");
                        var first = true;
                        foreach (var parameterRaw in parameters)
                        {
                            if (!first)
                                functionPointerTypeBuilder.Append(", ");
                            else
                                first = false;
                            
                            var parameter = parameterRaw;
                            if (parameter.StartsWith("const "))
                                parameter = parameter[6..];
                            
                            parameter = parameter.Split(' ')[0];
                            ApplyTypedefs(typedefsDictionary, ref parameter);
                            var paramaterPointerCount = 0;
                            while (parameter.EndsWith("*"))
                            {
                                parameter = parameter[..^1];
                                paramaterPointerCount++;
                            }
                            ApplyTypedefs(typedefsDictionary, ref parameter);
                            functionPointerTypeBuilder.Append(parameter);
                            functionPointerTypeBuilder.Append('*', paramaterPointerCount);
                        }
                        if (!first)
                            functionPointerTypeBuilder.Append(", ");
                        ApplyTypedefs(typedefsDictionary, ref returnType);
                        functionPointerTypeBuilder.Append($"{returnType}");
                        functionPointerTypeBuilder.Append(">");
                        fieldType = functionPointerTypeBuilder.ToString();
                    }
                    
                    // if Value Array
                    var fieldName = field.name;
                    if (field.name.EndsWith(']'))
                    {
                        var startOfArray = fieldName.IndexOf('[');
                        var sizeText = fieldName[(startOfArray + 1)..^1];
                        fieldName = fieldName[..startOfArray];
                        var valueArrayType = fieldType;
                        for (int i = 0; i < fieldTypePointerCount; i++)
                            valueArrayType += '*';
                        valueArraysToAdd.Add(new ValueArrayInfo
                        {
                            name = fieldName,
                            type = valueArrayType,
                            sizeText = sizeText
                        });
                        fieldType = $"{structName}_{fieldName}Array";
                    }
                    
                        
                    sourceWriter.Write($"\t\tpublic ");
                    
                    var fieldBuilder = new System.Text.StringBuilder(fieldType);
                    // add type arguments
                    if (templateType != null)
                    {
                        fieldBuilder.Append('<');
                        for (var i = 0; i < templateTypePointerCount; i++)
                            fieldBuilder.Append("Ptr<");
                        fieldBuilder.Append(templateType);
                        for (var i = 0; i < templateTypePointerCount; i++)
                            fieldBuilder.Append('>');
                        fieldBuilder.Append('>');
                    }
                    for (var i = 0; i < fieldTypePointerCount; i++)
                        fieldBuilder.Append('*');
                    fieldType = fieldBuilder.ToString();
                    
                    if (k_ForceTypeFieldMap.TryGetValue(fieldType, out var forcedType))
                        fieldType = forcedType;
                    
                    sourceWriter.Write(fieldType);
                    if (fieldName != string.Empty)
                        sourceWriter.Write(' ');
                    sourceWriter.Write(fieldName);
                    sourceWriter.Write(';');
                    sourceWriter.WriteLine();
                }
                sourceWriter.WriteLine("\t}\n");
                
                foreach (var valueArrayInfo in valueArraysToAdd)
                {
                    var valueArrayStructName = $"{structName}_{valueArrayInfo.name}Array";
                    if (k_ForceStructMap.TryGetValue(valueArrayStructName, out structNameWithForceTypeOverride))
                        valueArrayStructName = structNameWithForceTypeOverride;
                    var type = valueArrayInfo.type;
                    var sizeText = valueArrayInfo.sizeText;
                    var members = sizeText.Split('_');
                    var lastMember = members[^1];
                    if (members.Length > 1)
                        sizeText = string.Join("", members[..^1]) + "." + lastMember;
                    else
                        sizeText = sizeText.Replace('_', '.');
                    if (sizeText == "CHUNKS")
                        sizeText = "1";
                    else if (sizeText == "(BITCOUNT+31)>>5")
                        sizeText = "ImGuiKeyNamedKey.COUNT";
                    
                    var name = valueArrayInfo.name;
                    var typeSize = type == "ImGuiKeyData" ? "16" : SizeOfStruct(type, structsAndEnums, typedefsDictionary);
                    
                    sourceWriter.WriteLine("\t[StructLayout(LayoutKind.Sequential)]");
                    sourceWriter.WriteLine($"\tunsafe struct {valueArrayStructName}");
                    sourceWriter.WriteLine("\t{");
                    sourceWriter.WriteLine($"\t\tpublic fixed byte {name}[((int)({sizeText}))*({typeSize})];");
                    sourceWriter.WriteLine("\t}\n");
                }
            }
        }

        static void ApplyTypedefs(Dictionary<string, string> typedefsDictionary, ref string parameter)
        {
            var didChange = true;
            while (didChange && typedefsDictionary.TryGetValue(parameter, out var parameterOverride))
            {
                parameter = parameterOverride;
                didChange = parameterOverride != parameter;
            }
            
            if (typedefsDictionary.TryGetValue(parameter, out var parameterOverrideFinal))
                parameter = parameterOverrideFinal;
            if (typedefsDictionary.TryGetValue(parameter, out parameterOverrideFinal))
                parameter = parameterOverrideFinal;
        }

        static string SizeOfStruct(string structName, StructsAndEnums structsAndEnums, Dictionary<string, string> typedefsDictionary = null)
        {
            if (k_SizeMap.TryGetValue(structName, out var size))
                return size.ToString();
            if (typedefsDictionary != null)
                ApplyTypedefs(typedefsDictionary, ref structName);
            if (structName.EndsWith('*'))
                return "ImGuiConstants.PtrSize";
            if (structsAndEnums.structs.TryGetValue(structName, out var fields))
            {
                var sizeText = "0";
                foreach (var field in fields)
                {
                    var fieldType = field.type;
                    if (fieldType.EndsWith("*"))
                    {
                        sizeText += "+ImGuiConstants.PtrSize";
                        continue;
                    } 
                    
                    if (fieldType.StartsWith("const ")) 
                        fieldType = fieldType[6..];
                    if (typedefsDictionary != null)
                        ApplyTypedefs(typedefsDictionary, ref fieldType);
                    if (k_SizeMap.TryGetValue(fieldType, out var fieldSize))
                        sizeText += $"+{fieldSize.ToString()}";
                    else
                        sizeText += $"+{SizeOfStruct(fieldType, structsAndEnums, typedefsDictionary)}";
                }
                return sizeText;
            }
            
            return $"sizeof({structName})";
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
