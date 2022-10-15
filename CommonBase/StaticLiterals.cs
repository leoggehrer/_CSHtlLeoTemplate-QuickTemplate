//@CodeCopy
//MdStart
namespace CommonBase
{
    public static partial class StaticLiterals
    {
        static StaticLiterals()
        {
            BeforeClassInitialize();
            SolutionProjects = new string[]
            {
                "CommonBase",
                "TemplateCodeGenerator.Logic",
            };
            ProjectExtensions = new string[]
            {
                ".ConApp",
                ".CodeGenApp",
                ".Logic",
                ".Logic.UnitTest",
                ".WebApi",
                ".AspMvc",
                ".AngularApp",
                ".WpfApp",
            };
            SolutionToolProjects = new[]
            {
                "TemplateCodeGenerator.ConApp",
                "TemplateCodeGenerator.Logic",
                "TemplateComparison.ConApp",
                "TemplateCopier.ConApp",
                "TemplatePreprocessor.ConApp",
            };
            GenerationIgnoreFolders = new string[] { "node_module" };
            AfterClassInitialize();
        }
        static partial void BeforeClassInitialize();
        static partial void AfterClassInitialize();

        public static string SolutionFileExtension => ".sln";
        public static string ProjectFileExtension => ".csproj";

        public static string[] SolutionProjects { get; private set; }
        public static string[] ProjectExtensions { get; private set; }
        public static string[] SolutionToolProjects { get; private set; }

        public static string[] GenerationIgnoreFolders { get; private set; }
        public static string GeneratedCodeLabel => "@GeneratedCode";
        public static string GeneratedAndCustomizedCodeLabel => "@GeneratedAndCustomizedCode";
        public static string IgnoreLabel => "@Ignore";
        public static string BaseCodeLabel => "@BaseCode";
        public static string CodeCopyLabel => "@CodeCopy";
        public static string CSharpFileExtension => ".cs";
        public static string TypeScriptFileExtension => ".ts";
        public static string SourceFileExtensions => "*.css|*.cs|*.ts|*.cshtml|*.razor|*.razor.cs|*.template";
    }
}
//MdEnd
