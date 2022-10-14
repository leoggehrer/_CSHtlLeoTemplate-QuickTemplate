﻿//@CodeCopy
//MdStart
using System.Text;

namespace TemplateComparison.ConApp
{
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
            SourcePath = GetCurrentSolutionPath();
            TargetPaths = Array.Empty<string>();
            SourceLabels = new string[] { StaticLiterals.BaseCodeLabel };
            TargetLabels = new string[] { StaticLiterals.CodeCopyLabel };
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        #region Properties
        private static string? HomePath { get; set; }
        private static string UserPath { get; set; }
        private static string SourcePath { get; set; }
        private static string[] TargetPaths { get; set; }
        private static string[] AddTargetPaths { get; set; } = Array.Empty<string>();
        private static string[] SearchPatterns => StaticLiterals.SourceFileExtensions.Split('|');
        private static string[] SourceLabels { get; set; }
        private static string[] TargetLabels { get; set; }
        #endregion Properties

        private static void Main(/*string[] args*/)
        {
            RunApp();
        }

        #region Console methods
        private static readonly bool canBusyPrint = true;
        private static bool runBusyProgress = false;
        private static void RunApp()
        {
            var running = false;
            var saveForeColor = Console.ForegroundColor;

            do
            {
                var input = string.Empty;
                var handled = false;
                var targetPaths = new List<string>();

                PrintBusyProgress();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                BeforeGetTargetPaths(SourcePath, targetPaths, ref handled);
                if (handled == false)
                {
                    var searchPath = GetParentPathFrom(SourcePath, "source");

                    TargetPaths = GetQuickTemplateProjects(searchPath);
                    TargetPaths = TargetPaths.Union(AddTargetPaths).ToArray();
                }
                else
                {
                    TargetPaths = TargetPaths.Union(AddTargetPaths).ToArray();
                }
                runBusyProgress = false;
                PrintHeader(SourcePath, TargetPaths);
                Console.Write($"Balancing [+|1..{TargetPaths.Length}|X...Quit]: ");

                input = Console.ReadLine()?.ToLower();
                Console.ForegroundColor = saveForeColor;
                running = input?.Equals("x") == false;
                if (running)
                {
                    if (input != null && input.Equals("a"))
                    {
                        PrintBusyProgress();
                        foreach (var item in TargetLabels)
                        {
                            BalancingSolutions(SourcePath, SourceLabels, TargetPaths, TargetLabels);
                        }
                    }
                    else if (input != null && input.Equals("+"))
                    {
                        Console.WriteLine();
                        Console.Write("Add path: ");
                        input = Console.ReadLine();

                        if (Directory.Exists(input))
                        {
                            AddTargetPaths = AddTargetPaths.Union(new string[] { input }).ToArray();
                        }
                    }
                    else
                    {
                        var numbers = input?.Trim()
                                            .Split(',').Where(s => Int32.TryParse(s, out int n))
                                            .Select(s => Int32.Parse(s) - 1)
                                            .ToArray();

                        PrintBusyProgress();
                        foreach (var number in numbers ?? Array.Empty<int>())
                        {
                            if (number >= 0 && number < TargetPaths.Length)
                            {
                                BalancingSolutions(SourcePath, SourceLabels, new string[] { TargetPaths[number] }, TargetLabels);
                            }
                        }
                    }
                }
                runBusyProgress = false;
            } while (running);
        }
        private static void PrintBusyProgress()
        {
            Console.WriteLine();
            runBusyProgress = true;
            Task.Factory.StartNew(async () =>
            {
                while (runBusyProgress)
                {
                    if (canBusyPrint)
                    {
                        Console.Write(".");
                    }
                    await Task.Delay(250).ConfigureAwait(false);
                }
            });
        }
        private static void PrintHeader(string sourcePath, string[] targetPaths)
        {
            var index = 0;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Template Comparison");
            Console.WriteLine("===================");
            Console.WriteLine();
            for (int i = 0; i < SourceLabels.Length && i < TargetLabels.Length; i++)
            {
                Console.WriteLine($"{SourceLabels[i]} => {TargetLabels[i]}");
            }
            Console.WriteLine();
            Console.WriteLine($"Source: {sourcePath}");
            Console.WriteLine();
            Console.WriteLine("   Add path:      [ +] ADD...");
            Console.WriteLine();
            foreach (var target in targetPaths)
            {
                Console.WriteLine($"   Balancing for: [{++index,2}] {target}");
            }
            Console.WriteLine("   Balancing for: [ a] ALL...");
            Console.WriteLine();

            if (Directory.Exists(sourcePath) == false)
            {
                Console.WriteLine($"Source-Path '{sourcePath}' not exists");
            }
            foreach (var item in targetPaths)
            {
                if (Directory.Exists(item) == false)
                {
                    Console.WriteLine($"   Target-Path '{item}' not exists");
                }
            }
            Console.WriteLine();
        }
        #endregion Console methods

        #region App methods
        private static void BalancingSolutions(string sourcePath, string[] sourceLabels, IEnumerable<string> targetPaths, string[] targetLabels)
        {
            var sourcePathExists = Directory.Exists(sourcePath);

            if (sourcePathExists)
            {
                var targetPathsExists = new List<string>();

                foreach (var item in targetPaths)
                {
                    if (Directory.Exists(item))
                    {
                        targetPathsExists.Add(item);
                    }
                }
                // Delete all CopyCode files
                foreach (var targetPath in targetPathsExists)
                {
                    foreach (var searchPattern in SearchPatterns)
                    {
                        var targetCodeFiles = GetSourceCodeFiles(targetPath, searchPattern, targetLabels);

                        foreach (var targetCodeFile in targetCodeFiles)
                        {
                            File.Delete(targetCodeFile);
                        }
                    }
                }
                // Copy all BaseCode files
                foreach (var searchPattern in SearchPatterns)
                {
                    var sourceCodeFiles = GetSourceCodeFiles(sourcePath, searchPattern, sourceLabels);

                    foreach (var targetPath in targetPathsExists)
                    {
                        foreach (var sourceCodeFile in sourceCodeFiles)
                        {
                            SynchronizeSourceCodeFile(sourcePath, sourceCodeFile, targetPath, sourceLabels, targetLabels);
                        }
                    }
                }
            }
        }
        private static bool SynchronizeSourceCodeFile(string sourcePath, string sourceFilePath, string targetPath, string[] sourceLabels, string[] targetLabels)
        {
            var result = false;
            var canCopy = true;
            var sourceSolutionName = GetSolutionNameFromPath(sourcePath);
            var sourceProjectName = GetProjectNameFromFilePath(sourceFilePath, sourceSolutionName);
            var sourceSubFilePath = sourceFilePath.Replace(sourcePath, string.Empty)
                                                  .Replace(sourceProjectName, string.Empty)
                                                  .Replace("\\\\", string.Empty)
                                                  .Replace("//", string.Empty);
            var sourceSubFilePath2 = sourceFilePath.Replace(sourcePath, string.Empty)
                                                  .Replace(sourceProjectName, string.Empty)
                                                  .Replace("\\", "#")
                                                  .Replace("//", "#")
                                                  .Split('#', StringSplitOptions.RemoveEmptyEntries);

            var targetSolutionName = GetSolutionNameFromPath(targetPath);
            var targetProjectName = sourceProjectName.Replace(sourceSolutionName, targetSolutionName);
            var targetFilePath = Path.Combine(targetPath, targetProjectName, string.Join("\\", sourceSubFilePath2));
            var targetFileFolder = Path.GetDirectoryName(targetFilePath);
            var targetProjectPath = Path.Combine(targetPath, targetProjectName);

            //System.Diagnostics.Debug.WriteLine(targetProjectPath);

            if (Directory.Exists(targetProjectPath))
            {
                if (targetFileFolder != null && Directory.Exists(targetFileFolder) == false)
                {
                    Directory.CreateDirectory(targetFileFolder);
                }
                if (File.Exists(targetFilePath))
                {
                    var lines = File.ReadAllLines(targetFilePath, Encoding.Default);

                    canCopy = false;
                    if (lines.Any() && targetLabels.Any(l => lines.First().Contains(l)))
                    {
                        canCopy = true;
                    }
                }
                if (canCopy)
                {
                    var cpyLines = new List<string>();
                    var srcLines = File.ReadAllLines(sourceFilePath, Encoding.Default)
                                       .Select(i => i.Replace(sourceSolutionName, targetSolutionName));
                    var srcFirst = srcLines.FirstOrDefault();

                    if (srcFirst != null)
                    {
                        var label = sourceLabels.FirstOrDefault(l => srcFirst.Contains(l));

                        cpyLines.Add(srcFirst.Replace(label ?? string.Empty, StaticLiterals.CodeCopyLabel));
                    }
                    cpyLines.AddRange(File.ReadAllLines(sourceFilePath, Encoding.Default)
                                       .Skip(1)
                                       .Select(i => i.Replace(sourceSolutionName, targetSolutionName)));
                    File.WriteAllLines(targetFilePath, cpyLines.ToArray(), Encoding.UTF8);
                }
            }
            return result;
        }
        #endregion App methods

        #region Helpers
        private static string GetCurrentSolutionPath()
        {
            int endPos = AppContext.BaseDirectory
                                   .IndexOf($"{nameof(TemplateComparison)}", StringComparison.CurrentCultureIgnoreCase);

            return AppContext.BaseDirectory[..endPos];
        }
        private static string GetParentPathFrom(string path, string parentFolder)
        {
            var directoryInfo = new DirectoryInfo(path);

            while (directoryInfo != null && directoryInfo.Name.Equals(parentFolder, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                directoryInfo = directoryInfo.Parent;
            }
            return directoryInfo != null ? directoryInfo.FullName : path;
        }
        private static string[] GetQuickTemplateProjects(string searchPath)
        {
            var qtDirectories = Directory.GetDirectories(searchPath, "QT*", SearchOption.AllDirectories)
                                         .Where(d => d.Replace(UserPath, String.Empty).Contains('.') == false)
                                         .ToList();
            return qtDirectories.ToArray();
        }
        private static IEnumerable<string> GetSourceCodeFiles(string path, string searchPattern, string[] labels)
        {
            var result = new List<string>();
            var files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories)
                                 .Where(f => StaticLiterals.GenerationIgnoreFolders.Any(e => f.Contains(e) == false))
                                 .OrderBy(i => i);

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file, Encoding.Default);

                if (lines.Any() && labels.Any(l => lines.First().Contains(l)))
                {
                    result.Add(file);
                }
                //System.Diagnostics.Debug.WriteLine($"{file}");
            }
            return result;
        }
        private static string GetSolutionNameFromPath(string path)
        {
            var result = string.Empty;
            var data = path.Split("\\", StringSplitOptions.RemoveEmptyEntries);

            if (data.Any())
            {
                result = data.Last();
            }
            return result;
        }
        private static string GetProjectNameFromFilePath(string filePath, string solutionName)
        {
            var result = string.Empty;
            var data = filePath.Split("\\", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < data.Length && result == string.Empty; i++)
            {
                for (int j = 0; j < StaticLiterals.SolutionProjects.Length; j++)
                {
                    if (data[i].Equals(StaticLiterals.SolutionProjects[j]))
                    {
                        result = data[i];
                    }
                }
                for (int j = 0; j < StaticLiterals.SolutionToolProjects.Length; j++)
                {
                    if (data[i].Equals(StaticLiterals.SolutionToolProjects[j]))
                    {
                        result = data[i];
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    for (int j = 0; j < StaticLiterals.ProjectExtensions.Length; j++)
                    {
                        if (data[i].Equals($"{solutionName}{StaticLiterals.ProjectExtensions[j]}"))
                        {
                            result = data[i];
                        }
                    }
                }
            }
            return result;
        }
        #endregion Helpers

        #region Partial methods
        static partial void BeforeGetTargetPaths(string sourcePath, List<string> targetPaths, ref bool handled);
        static partial void AfterGetTargetPaths(string sourcePath, List<string> targetPaths);
        #endregion Partial methods
    }
}
//MdEnd
