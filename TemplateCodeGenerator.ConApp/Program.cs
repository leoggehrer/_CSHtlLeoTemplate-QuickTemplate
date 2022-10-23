//@CodeCopy
//MdStart
namespace TemplateCodeGenerator.ConApp
{
    using System.Diagnostics;
    using TemplateCodeGenerator.Logic;
    using TemplateCodeGenerator.Logic.Generation;
    internal partial class Program
    {
        static Program()
        {
            ClassConstructing();
            HomePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                        Environment.OSVersion.Platform == PlatformID.MacOSX)
                       ? Environment.GetEnvironmentVariable("HOME")
                       : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            UserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            SolutionPath = GetCurrentSolutionPath();
            SourcePath = Directory.Exists(Path.Combine(UserPath, "source")) ? Path.Combine(UserPath, "source") : SolutionPath;
            TargetPaths = Array.Empty<string>();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        #region Properties
        private static string? HomePath { get; set; }
        private static string UserPath { get; set; }
        private static string SolutionPath { get; set; }
        private static string SourcePath { get; set; }
        private static string[] TargetPaths { get; set; }
        private static string[] SearchPatterns => StaticLiterals.SourceFileExtensions.Split('|');
        #endregion Properties

        static void Main(/*string[] args*/)
        {
            RunApp();
        }
        #region Console methods
        private static void RunApp()
        {
            var toGroupFile = false;
            var input = string.Empty;
            var saveForeColor = Console.ForegroundColor;

            while (input.Equals("x") == false)
            {
                var menuIndex = 0;
                var maxWaiting = 10 * 60 * 1000;    // 10 minutes
                var sourceSolutionName = GetSolutionNameByPath(SolutionPath);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Template Code Generator");
                Console.WriteLine("=======================");
                Console.WriteLine();
                Console.WriteLine($"Code generation for: {sourceSolutionName}");
                Console.WriteLine($"From file path:  {SolutionPath}");
                Console.WriteLine($"Generation into: {(toGroupFile ? "Group files" : "Single files")}");
                Console.WriteLine();
                Console.WriteLine($"[{++menuIndex}] Change source path");
                Console.WriteLine($"[{++menuIndex}] Compile solution...");
                Console.WriteLine($"[{++menuIndex}] Compile logic project...");
                Console.WriteLine($"[{++menuIndex}] Change group file flag");
                Console.WriteLine($"[{++menuIndex}] Delete generation files...");
                Console.WriteLine($"[{++menuIndex}] Start code generation...");
                Console.WriteLine("[x|X] Exit");
                Console.WriteLine();
                Console.Write("Choose: ");

                input = Console.ReadLine()?.ToLower() ?? String.Empty;
                Console.ForegroundColor = saveForeColor;
                if (Int32.TryParse(input, out var select))
                {
                    var solutionProperties = SolutionProperties.Create(SolutionPath);

                    if (select == 1)
                    {
                        var solutionPath = GetCurrentSolutionPath();
                        var qtProjects = GetQuickTemplateProjects(SourcePath).Union(new[] { solutionPath }).ToArray();

                        for (int i = 0; i < qtProjects.Length; i++)
                        {
                            if (i == 0)
                                Console.WriteLine();

                            Console.WriteLine($"Change path to: [{i + 1, 2}] {qtProjects[i]}");
                        }
                        Console.WriteLine();
                        Console.Write("Select or enter source path: ");
                        var selectOrPath = Console.ReadLine();

                        if (Int32.TryParse(selectOrPath, out int number))
                        {
                            if ((number - 1) >= 0 && (number - 1) < qtProjects.Length)
                            {
                                SolutionPath = qtProjects[number - 1];
                            }
                        }
                        else if (Directory.Exists(selectOrPath))
                        {
                            SolutionPath = selectOrPath;
                        }
                    }
                    if (select == 2)
                    {
                        var counter = 0;
                        var startCompilePath = Path.Combine(Path.GetTempPath(), solutionProperties.SolutionName);
                        var compilePath = startCompilePath;
                        bool deleteError;

                        do
                        {
                            deleteError = false;
                            if (Directory.Exists(compilePath))
                            {
                                try
                                {
                                    Directory.Delete(compilePath, true);
                                }
                                catch
                                {
                                    deleteError = true;
                                    compilePath = $"{startCompilePath}{++counter}";
                                }
                            }
                        } while (deleteError != false);

                        var arguments = $"build \"{solutionProperties.SolutionFilePath}\" -c Release -o {compilePath}";
                        Console.WriteLine(arguments);
                        Debug.WriteLine($"dotnet.exe {arguments}");

                        var csprojStartInfo = new ProcessStartInfo("dotnet.exe")
                        {
                            Arguments = arguments,
                            //WorkingDirectory = projectPath,
                            UseShellExecute = false
                        };
                        Process.Start(csprojStartInfo)?.WaitForExit(maxWaiting);
                        solutionProperties.CompilePath = compilePath;
                        if (select == 2)
                        {
                            Console.Write("Press any key ");
                            Console.ReadKey();
                        }
                    }
                    if (select == 3 || select == 6)
                    {
                        var counter = 0;
                        var startCompilePath = Path.Combine(Path.GetTempPath(), solutionProperties.SolutionName);
                        var compilePath = startCompilePath;
                        bool deleteError;

                        do
                        {
                            deleteError = false;
                            if (Directory.Exists(compilePath))
                            {
                                try
                                {
                                    Directory.Delete(compilePath, true);
                                }
                                catch
                                {
                                    deleteError = true;
                                    compilePath = $"{startCompilePath}{++counter}";
                                }
                            }
                        } while (deleteError != false);

                        var arguments = $"build \"{solutionProperties.LogicCSProjectFilePath}\" -c Release -o {compilePath}";
                        Console.WriteLine(arguments);
                        Debug.WriteLine($"dotnet.exe {arguments}");

                        var csprojStartInfo = new ProcessStartInfo("dotnet.exe")
                        {
                            Arguments = arguments,
                            //WorkingDirectory = projectPath,
                            UseShellExecute = false
                        };
                        Process.Start(csprojStartInfo)?.WaitForExit(maxWaiting);
                        solutionProperties.CompilePath = compilePath;
                        if (select == 2)
                        {
                            Console.Write("Press any key ");
                            Console.ReadKey();
                        }
                    }
                    if (select == 4)
                    {
                        toGroupFile = !toGroupFile;
                    }
                    if (select == 5)
                    {
                        Generator.DeleteGeneratedFiles(SolutionPath);
                    }
                    if (select == 6)
                    {
                        var generatedItems = Generator.Generate(solutionProperties);

                        Generator.DeleteGeneratedFiles(SolutionPath);
                        Logic.Writer.WriteToGroupFile = toGroupFile;
                        Logic.Writer.WriteAll(SolutionPath, solutionProperties, generatedItems);
                    }
                    Thread.Sleep(700);
                }
            }
        }
        #endregion Console methods

        #region Helpers
        private static string GetCurrentSolutionPath()
        {
            int endPos = AppContext.BaseDirectory
                                   .IndexOf($"{nameof(TemplateCodeGenerator)}", StringComparison.CurrentCultureIgnoreCase);

            return AppContext.BaseDirectory[..endPos];
        }
        private static string GetSolutionNameByPath(string solutionPath)
        {
            return solutionPath.Split(new char[] { '\\', '/' })
                               .Where(e => string.IsNullOrEmpty(e) == false)
                               .Last();
        }
        private static string[] GetQuickTemplateProjects(string sourcePath)
        {
            var qtDirectories = Directory.GetDirectories(sourcePath, "QT*", SearchOption.AllDirectories)
                                         .Where(d => d.Replace(UserPath, string.Empty).Contains('.') == false)
                                         .ToList();
            return qtDirectories.ToArray();
        }
        private static string[] GetQuickTemplateProjectsFromParent(string sourcePath)
        {
            var directoryInfo = new DirectoryInfo(sourcePath);
            var parentDirectory = directoryInfo.Parent != null ? directoryInfo.Parent.FullName : sourcePath;
            var qtDirectories = Directory.GetDirectories(parentDirectory, "QT*", SearchOption.AllDirectories)
                                         .Where(d => d.Replace(UserPath, String.Empty).Contains('.') == false)
                                         .ToList();
            return qtDirectories.ToArray();
        }
        #endregion Helpers

        #region Partial methods
        static partial void BeforeGetTargetPaths(string sourcePath, List<string> targetPaths, ref bool handled);
        static partial void AfterGetTargetPaths(string sourcePath, List<string> targetPaths);
        #endregion Partial methods

        private static void TestForecolor()
        {
            var savecolor = Console.ForegroundColor;
            var first = Console.ForegroundColor.FirstEnum();
            var run = first;

            do
            {
                Console.ForegroundColor = savecolor;
                Console.Write($"Color - {run,-14}: ");
                Console.ForegroundColor = run;
                Console.WriteLine("Hallo, das ist ein color Test!");
                run = run.NextEnum();
            } while (first != run);
        }
    }
}
//MdEnd
