﻿//@CodeCopy
//MdStart
namespace TemplateCodeGenerator.Logic.Generation
{
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using TemplateCodeGenerator.Logic.Contracts;
    internal sealed partial class AngularGenerator : ClassGenerator
    {
        public bool GenerateEnums { get; set; }
        public bool GenerateModels { get; set; }
        public bool GenerateServices { get; set; }

        #region AngularApp-Definitions
        public static string CodeExtension => "ts";
        public static string EnumsSubFolder => Path.Combine("src", "app", "core", "enums", "gen");
        public static string ModelsSubFolder => Path.Combine("src", "app", "core", "models", "gen");
        public static string ServicesSubFolder => Path.Combine("src", "app", "core", "services", "http", "gen");

        public static string SourceNameSpace => "src";
        public static string ContractsNameSpace => $"{SourceNameSpace}.contracts";
        public static string CreateContractsNameSpace(Type type)
        {
            return $"{ContractsNameSpace}.{CreateSubNamespaceFromType(type)}".ToLower();
        }
        public static string CreateTypeScriptFullName(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{CreateContractsNameSpace(type)}.{(type.IsInterface ? ItemProperties.CreateEntityName(type) : type.Name)}";
        }
        #endregion AngularApp-Definitions

        public AngularGenerator(ISolutionProperties solutionProperties) : base(solutionProperties)
        {
            GenerateEnums = QuerySetting<bool>(Common.ItemType.TypeScriptEnum, "All", StaticLiterals.Generate, "True");
            GenerateModels = QuerySetting<bool>(Common.ItemType.TypeScriptModel, "All", StaticLiterals.Generate, "True");
            GenerateServices = QuerySetting<bool>(Common.ItemType.TypeScriptService, "All", StaticLiterals.Generate, "True");
        }
        private bool CanCreate(Type type)
        {
            bool create = EntityProject.IsAccountOrLoggingOrRevisionEntity(type) ? false : true;

            CanCreateModel(type, ref create);
            return create;
        }
        partial void CanCreateModel(Type type, ref bool create);

        public IEnumerable<IGeneratedItem> GenerateAll()
        {
            var result = new List<IGeneratedItem>();

            result.AddRange(CreateEnums());
            result.AddRange(CreateModels());
            result.AddRange(CreateServices());
            return result;
        }
        public IEnumerable<IGeneratedItem> CreateEnums()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            foreach (var type in entityProject.EnumTypes)
            {
                if (CanCreate(type) && QuerySetting<bool>(Common.ItemType.TypeScriptEnum, type, StaticLiterals.Generate, GenerateEnums.ToString()))
                {
                    result.Add(CreateEnumFromType(type));
                }
            }
            return result;
        }
        public IGeneratedItem CreateEnumFromType(Type type)
        {
            var subPath = ConvertFileItem(CreateSubPathFromType(type));
            var fileName = $"{ConvertFileItem(type.Name)}.{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.Angular, Common.ItemType.TypeScriptEnum)
            {
                FullName = CreateTypeScriptFullName(type),
                FileExtension = CodeExtension,
                SubFilePath = Path.Combine(EnumsSubFolder, subPath, fileName),
            };

            StartCreateEnum(type, result.Source);
            result.Add($"export enum {type.Name}" + " {");

            foreach (var item in Enum.GetNames(type))
            {
                var value = Enum.Parse(type, item);

                result.Add($"{item} = {(int)value},");
            }

            result.Add("}");
            result.AddRange(result.Source.Eject().Distinct());
            result.FormatCSharpCode();
            FinishCreateEnum(type, result.Source);
            return result;
        }
        partial void StartCreateEnum(Type type, List<string> lines);
        partial void FinishCreateEnum(Type type, List<string> lines);

        public IEnumerable<IGeneratedItem> CreateModels()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            foreach (var type in entityProject.EntityTypes)
            {
                if (CanCreate(type) && QuerySetting<bool>(Common.ItemType.TypeScriptModel, type, StaticLiterals.Generate, GenerateModels.ToString()))
                {
                    result.Add(CreateModelFromType(type, entityProject.EntityTypes));
                }
            }
            return result;
        }
        public IGeneratedItem CreateModelFromType(Type type, IEnumerable<Type> types)
        {
            var subPath = ConvertFileItem(CreateSubPathFromType(type));
            var projectPath = Path.Combine(SolutionProperties.SolutionPath, SolutionProperties.AngularAppProjectName);
            var entityName = ItemProperties.CreateEntityName(type);
            var fileName = $"{ConvertFileItem(entityName)}.{CodeExtension}";
            var typeProperties = type.GetAllPropertyInfos();
            var declarationTypeName = string.Empty;
            var result = new Models.GeneratedItem(Common.UnitType.Angular, Common.ItemType.TypeScriptModel)
            {
                FullName = CreateTypeScriptFullName(type),
                FileExtension = CodeExtension,
                SubFilePath = Path.Combine(ModelsSubFolder, subPath, fileName),
            };

            StartCreateModel(type, result.Source);
            result.Add($"export interface {entityName}" + " {");

            foreach (var item in typeProperties)
            {
                if (declarationTypeName.Equals(item.DeclaringType!.Name) == false)
                {
                    declarationTypeName = item.DeclaringType.Name;
                    result.Add($"/** {declarationTypeName} **/");
                }

                result.AddRange(CreateTypeScriptProperty(item));
            }
            result.AddRange(CreateModelToModelFromModels(type, types));
            result.Add("}");

            result.Source.Insert(result.Source.Count - 1, StaticLiterals.AngularCustomCodeBeginLabel);
            result.Source.InsertRange(result.Source.Count - 1, ReadCustomModelCode(projectPath, result));
            result.Source.Insert(result.Source.Count - 1, StaticLiterals.AngularCustomCodeEndLabel);

            var imports = new List<string>();

            imports.AddRange(CreateTypeImports(type, types));
            imports.AddRange(CreateModelToModelImports(type, types));
            imports.Add(StaticLiterals.AngularCustomImportBeginLabel);
            imports.AddRange(ReadCustomModelImports(projectPath, result));
            imports.Add(StaticLiterals.AngularCustomImportEndLabel);

            InsertTypeImports(imports, result.Source);
            FinishCreateModel(type, result.Source);
            return result;
        }
        partial void StartCreateModel(Type type, List<string> lines);
        partial void FinishCreateModel(Type type, List<string> lines);

        private IEnumerable<IGeneratedItem> CreateServices()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            foreach (var type in entityProject.EntityTypes)
            {
                if (CanCreate(type) && QuerySetting<bool>(Common.ItemType.TypeScriptService, type, StaticLiterals.Generate, GenerateServices.ToString()))
                {
                    result.Add(CreateServiceFromType(type, Common.UnitType.Angular, Common.ItemType.TypeScriptService));
                }
            }
            return result;
        }
        private IGeneratedItem CreateServiceFromType(Type type, Common.UnitType unitType, Common.ItemType itemType)
        {
            var subPath = ConvertFileItem(CreateSubPathFromType(type));
            var projectPath = Path.Combine(SolutionProperties.SolutionPath, SolutionProperties.AngularAppProjectName);
            var entityName = ItemProperties.CreateEntityName(type);
            var fileName = $"{ConvertFileItem($"{entityName}Service")}.{CodeExtension}";
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = CreateTypeScriptFullName(type),
                FileExtension = CodeExtension,
                SubFilePath = Path.Combine(ServicesSubFolder, subPath, fileName),
            };

            StartCreateService(type, result.Source);
            result.Add("import { HttpClient } from '@angular/common/http';");
            result.Add("import { Injectable } from '@angular/core';");
            result.Add("import { ApiBaseService } from '@app-core/services/api-base.service';");
            result.Add("import { environment } from '@environment/environment';");
            result.Add(CreateImport("@app-core-models", entityName, subPath));

            result.Add(StaticLiterals.AngularCustomImportBeginLabel);
            result.AddRange(ReadCustomModelImports(projectPath, result));
            result.Add(StaticLiterals.AngularCustomImportEndLabel);

            result.Add("@Injectable({");
            result.Add("  providedIn: 'root',");
            result.Add("})");
            result.Add($"export class {entityName}Service extends ApiBaseService<{entityName}>" + " {");
            result.Add("  constructor(public override http: HttpClient) {");
            result.Add($"    super(http, environment.API_BASE_URL + '/{entityName.CreatePluralWord().ToLower()}');");
            result.Add("  }");
            result.Add("}");

            result.Source.Insert(result.Source.Count - 1, StaticLiterals.AngularCustomCodeBeginLabel);
            result.Source.InsertRange(result.Source.Count - 1, ReadCustomModelCode(projectPath, result));
            result.Source.Insert(result.Source.Count - 1, StaticLiterals.AngularCustomCodeEndLabel);
            FinishCreateService(type, result.Source);
            return result;
        }
        partial void StartCreateService(Type type, List<string> lines);
        partial void FinishCreateService(Type type, List<string> lines);

        private T QuerySetting<T>(Common.ItemType itemType, Type type, string valueName, string defaultValue)
        {
            T result;

            try
            {
                result = (T)Convert.ChangeType(QueryGenerationSettingValue(Common.UnitType.Angular, itemType, CreateEntitiesSubTypeFromType(type), valueName, defaultValue), typeof(T));
            }
            catch (Exception ex)
            {
                result = (T)Convert.ChangeType(defaultValue, typeof(T));
                System.Diagnostics.Debug.WriteLine($"Error in {System.Reflection.MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
            }
            return result;
        }
        private T QuerySetting<T>(Common.ItemType itemType, string itemName, string valueName, string defaultValue)
        {
            T result;

            try
            {
                result = (T)Convert.ChangeType(QueryGenerationSettingValue(Common.UnitType.Angular, itemType, itemName, valueName, defaultValue), typeof(T));
            }
            catch (Exception ex)
            {
                result = (T)Convert.ChangeType(defaultValue, typeof(T));
                System.Diagnostics.Debug.WriteLine($"Error in {System.Reflection.MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
            }
            return result;
        }

        #region Helpers
        public static IEnumerable<string> ReadCustomModelImports(string sourcePath, Models.GeneratedItem generatedItem)
        {
            var result = new List<string>();
            var filePath = Path.Combine(sourcePath, generatedItem.SubFilePath);

            if (File.Exists(filePath))
            {
                var source = File.ReadAllText(filePath, Encoding.Default);

                foreach (var item in source.GetAllTags(new string[] { $"{StaticLiterals.AngularCustomImportBeginLabel}{Environment.NewLine}", $"{StaticLiterals.AngularCustomImportEndLabel}" })
                               .OrderBy(e => e.StartTagIndex))
                {
                    if (item.InnerText.HasContent())
                    {
                        result.AddRange(item.InnerText.ToLines().Where(l => l.HasContent()));
                    }
                }
            }
            return result;
        }
        public static IEnumerable<string> ReadCustomModelCode(string sourcePath, Models.GeneratedItem generatedItem)
        {
            var result = new List<string>();
            var filePath = Path.Combine(sourcePath, generatedItem.SubFilePath);

            if (File.Exists(filePath))
            {
                var source = File.ReadAllText(filePath, Encoding.Default);

                foreach (var item in source.GetAllTags(new string[] { $"{StaticLiterals.AngularCustomCodeBeginLabel}{Environment.NewLine}", $"{StaticLiterals.AngularCustomCodeEndLabel}" })
                               .OrderBy(e => e.StartTagIndex))
                {
                    if (item.InnerText.HasContent())
                    {
                        result.AddRange(item.InnerText.ToLines().Where(l => l.HasContent()));
                    }
                }
            }
            return result;
        }
        public static string ConvertFileItem(string fileItem)
        {
            var result = new StringBuilder();

            foreach (var item in fileItem)
            {
                if (result.Length == 0)
                {
                    result.Append(Char.ToLower(item));
                }
                else if (Char.IsUpper(item))
                {
                    if (result[^1] != '/' && result[^1] != '\\')
                    {
                        result.Append('-');
                    }
                    result.Append(Char.ToLower(item));
                }
                else
                {
                    result.Append(Char.ToLower(item));
                }
            }
            return result.ToString();
        }
        public static string CreateImport(string alias, string typeName, string subPath)
        {
            return "import { " + typeName + " } from " + $"'{alias}/gen/{ConvertFileItem(subPath)}/{ConvertFileItem(typeName)}';";
        }
        public static void InsertTypeImports(IEnumerable<string> imports, List<string> lines)
        {
            foreach (var item in imports.Reverse())
            {
                lines.Insert(0, item);
            }
        }

        public static IEnumerable<string> CreateTypeImports(Type type, IEnumerable<Type> types)
        {
            var result = new List<string>();
            var typeProperties = type.GetAllPropertyInfos();
            var entityName = ItemProperties.CreateEntityName(type);

            foreach (var propertyInfo in typeProperties)
            {
                if (propertyInfo.PropertyType.IsEnum)
                {
                    var typeName = $"{propertyInfo.PropertyType.Name}";

                    if (typeName.Equals(entityName) == false)
                    {
                        var subPath = GeneratorObject.CreateSubPathFromType(propertyInfo.PropertyType).ToLower();

                        result.Add(CreateImport("@app-core-enums", typeName, subPath));
                    }
                }
                else if (propertyInfo.PropertyType.IsGenericType)
                {
                    var subType = propertyInfo.PropertyType.GetGenericArguments().First();
                    var modelType = types.FirstOrDefault(e => e.FullName == subType.FullName);

                    if (modelType != null && modelType.IsClass)
                    {
                        var modelName = ItemProperties.CreateEntityName(modelType);

                        if (modelName.Equals(entityName) == false)
                        {
                            var subPath = GeneratorObject.CreateSubPathFromType(modelType).ToLower();

                            result.Add(CreateImport("@app-core-models", modelName, subPath));
                        }
                    }
                }
                else if (propertyInfo.PropertyType.IsClass)
                {
                    var modelType = types.FirstOrDefault(e => e.FullName == propertyInfo.PropertyType.FullName);

                    if (modelType != null && modelType.IsClass)
                    {
                        var modelName = ItemProperties.CreateEntityName(modelType);

                        if (modelName.Equals(entityName) == false)
                        {
                            var subPath = GeneratorObject.CreateSubPathFromType(modelType).ToLower();

                            result.Add(CreateImport("@app-core-models", modelName, subPath));
                        }
                    }
                }
            }
            return result.Distinct();
        }
        public static IEnumerable<string> CreateTypeScriptProperty(PropertyInfo propertyInfo)
        {
            var result = new List<string>();

            if (propertyInfo.PropertyType.IsEnum)
            {
                var enumName = $"{propertyInfo.PropertyType.Name}";

                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {enumName};");
            }
            else if (propertyInfo.PropertyType == typeof(DateTime)
                     || propertyInfo.PropertyType == typeof(DateTime?))
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: Date;");
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: string;");
            }
            else if (propertyInfo.PropertyType == typeof(bool))
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: boolean;");
            }
            else if (propertyInfo.PropertyType.IsNumericType())
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: number;");
            }
            else if (propertyInfo.PropertyType.IsGenericType)
            {
                Type subType = propertyInfo.PropertyType.GetGenericArguments().First();

                if (subType.IsInterface)
                {
                    result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {subType.Name[1..]}[];");
                }
                else if (subType == typeof(Guid))
                {
                    result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: string[];");
                }
                else
                {
                    result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {subType.Name}[];");
                }
            }
            else if (propertyInfo.PropertyType.IsInterface)
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {propertyInfo.PropertyType.Name[1..]};");
            }
            return result;
        }
        private static IEnumerable<string> CreateModelToModelImports(Type type, IEnumerable<Type> types)
        {
            var result = new List<string>();
            var typeName = ItemProperties.CreateEntityName(type);

            foreach (var other in types)
            {
                var otherName = ItemProperties.CreateEntityName(other);

                foreach (var pi in other.GetProperties())
                {
                    if (pi.Name.Equals($"{typeName}Id"))
                    {
                        var refTypeName = ItemProperties.CreateEntityName(other);
                        var subPath = GeneratorObject.CreateSubPathFromType(other).ToLower();

                        result.Add(CreateImport("@app-core-models", refTypeName, subPath));
                    }
                }
            }
            foreach (var pi in type.GetProperties())
            {
                foreach (var other in types)
                {
                    var otherName = ItemProperties.CreateEntityName(other);

                    if (pi.Name.Equals($"{otherName}Id"))
                    {
                        var refTypeName = ItemProperties.CreateEntityName(other);
                        var subPath = GeneratorObject.CreateSubPathFromType(other).ToLower();

                        result.Add(CreateImport("@app-core-models", refTypeName, subPath));
                    }
                    else if (pi.Name.StartsWith($"{otherName}Id_"))
                    {
                        var data = pi.Name.Split("_");

                        if (data.Length == 2)
                        {
                            var refTypeName = ItemProperties.CreateEntityName(other);
                            var subPath = GeneratorObject.CreateSubPathFromType(other).ToLower();

                            result.Add(CreateImport("@app-core-models", refTypeName, subPath));
                        }
                    }
                }
            }
            return result.Distinct();
        }
        private static IEnumerable<string> CreateModelToModelFromModels(Type type, IEnumerable<Type> types)
        {
            var result = new List<string>();
            var typeName = ItemProperties.CreateEntityName(type);

            foreach (var other in types)
            {
                var otherName = ItemProperties.CreateEntityName(other);

                foreach (var pi in other.GetProperties())
                {
                    if (pi.Name.Equals($"{typeName}Id"))
                    {
                        var name = $"{other.Name[1..]}s";

                        result.Add($"{char.ToLower(name[0])}{name[1..]}: {other.Name[1..]}[];");
                    }
                }
            }
            foreach (var pi in type.GetProperties())
            {
                foreach (var other in types)
                {
                    var otherName = ItemProperties.CreateEntityName(other);

                    if (pi.Name.Equals($"{otherName}Id"))
                    {
                        var name = $"{other.Name[1..]}";

                        result.Add($"{char.ToLower(name[0])}{name[1..]}: {other.Name[1..]};");
                    }
                    else if (pi.Name.StartsWith($"{otherName}Id_"))
                    {
                        var data = pi.Name.Split("_");

                        if (data.Length == 2)
                        {
                            var name = $"{other.Name[1..]}_{data[1]}";

                            result.Add($"{char.ToLower(name[0])}{name[1..]}: {other.Name[1..]};");
                        }
                    }
                }
            }
            return result;
        }
        #endregion Helpers
    }
}
//MdEnd
