﻿//@CodeCopy
//MdStart
namespace TemplatePreprocessor.ConApp
{
    using CommonBase.Extensions;
    using System.Text;
    internal partial class Program
    {
        #region Class-Constructors
        static Program()
        {
            ClassConstructing();
            HomePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                        Environment.OSVersion.Platform == PlatformID.MacOSX)
                       ? Environment.GetEnvironmentVariable("HOME")
                       : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            UserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            SourcePath = GetCurrentSolutionPath();
            Defines = new string[]
            {
                "ACCOUNT_OFF",
                "LOGGING_OFF",
                "REVISION_OFF",
                "DEVELOP_OFF",
            };
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        #endregion Class-Constructors
        private static string? HomePath { get; set; }
        private static string UserPath { get; set; }
        private static string SourcePath { get; set; }
        private static string[] Defines { get; set; }

        private static void Main(/*string[] args*/)
        {
            RunApp();
        }

        private static void RunApp()
        {
            var input = string.Empty;
            var saveForeColor = Console.ForegroundColor;

            while (input.Equals("x") == false)
            {
                var menuIndex = 0;
                var sourceSolutionName = GetSolutionNameByPath(SourcePath);
                
                // Read defines from the solution
                Defines = GetPreprocessorDefinesInProjectFiles(SourcePath);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Template Preprocessor");
                Console.WriteLine("=====================");
                Console.WriteLine();
                Console.WriteLine($"Definition-Values: {string.Join(" ", Defines)}");
                Console.WriteLine();
                Console.WriteLine($"Set definition-values in '{sourceSolutionName}'");
                Console.WriteLine($"                         '{SourcePath}'");
                Console.WriteLine();
                Console.WriteLine($"[{++menuIndex}] Change source path");

                for (int i = 0; i < Defines.Length; i++)
                {
                    Console.WriteLine($"[{++menuIndex}] Set definition {(Defines[i].EndsWith("_ON") ? Defines[i].Replace("_ON", "_OFF") : Defines[i].Replace("_OFF", "_ON"))}");
                }

                Console.WriteLine($"[{++menuIndex}] Start assignment process...");
                Console.WriteLine("[x|X] Exit");
                Console.WriteLine();
                Console.Write("Choose: ");

                input = Console.ReadLine()?.ToLower() ?? String.Empty;
                Console.ForegroundColor = saveForeColor;
                if (Int32.TryParse(input, out var select))
                {
                    if (select == 1)
                    {
                        var solutionPath = GetCurrentSolutionPath();
                        var searchPath = GetParentPathFrom(solutionPath, "source");
                        var qtProjects = GetQuickTemplateProjects(searchPath).Union(new[] { solutionPath }).ToArray();

                        for (int i = 0; i < qtProjects.Length; i++)
                        {
                            if (i == 0)
                                Console.WriteLine();

                            Console.WriteLine($"Change path to: [{i + 1}] {qtProjects[i]}");
                        }
                        Console.WriteLine();
                        Console.Write("Select or enter source path: ");
                        var selectOrPath = Console.ReadLine();

                        if (Int32.TryParse(selectOrPath, out int number))
                        {
                            if ((number - 1) >= 0 && (number - 1) < qtProjects.Length)
                            {
                                SourcePath = qtProjects[number - 1];
                            }
                        }
                        else if (string.IsNullOrEmpty(selectOrPath) == false)
                        {
                            SourcePath = selectOrPath;
                        }
                    }
                    else if ((select - 2) >= 0 && (select - 2) < Defines.Length)
                    {
                        if (Defines[select - 2].EndsWith("_ON"))
                        {
                            Defines[select - 2] = Defines[select - 2].Replace("_ON", "_OFF");
                        }
                        else
                        {
                            Defines[select - 2] = Defines[select - 2].Replace("_OFF", "_ON");
                        }
                        SetPreprocessorDefinesInProjectFiles(SourcePath, Defines);
                        EditPreprocessorDefinesInRazorFiles(SourcePath, Defines);
                    }
                    else if ((select - 2) == Defines.Length)
                    {
                        SetPreprocessorDefinesInProjectFiles(SourcePath, Defines);
                        EditPreprocessorDefinesInRazorFiles(SourcePath, Defines);
                    }
                }
                Console.ResetColor();
            }
        }

        private static void PrintSolutionDirectives(string path, params string[] excludeDirectives)
        {
            var files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var idx = 0;
                var lines = File.ReadAllLines(file, Encoding.Default);

                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("#if ") && excludeDirectives.Any(e => line.Contains(e)) == false)
                    {
                        var message = $"{line} in line {idx} of the {file} file";

                        //Console.WriteLine(message);
                        //Debug.WriteLine(message);
                    }
                    idx++;
                }
            }
        }

        private static string GetCurrentSolutionPath()
        {
            int endPos = AppContext.BaseDirectory
                                   .IndexOf($"{nameof(TemplatePreprocessor)}", StringComparison.CurrentCultureIgnoreCase);
            var result = AppContext.BaseDirectory[..endPos];

            while (result.EndsWith("/"))
            {
                result = result[0..^1];
            }
            while (result.EndsWith("\\"))
            {
                result = result[0..^1];
            }
            return result;
        }
        private static string GetCurrentSolutionName()
        {
            var solutionPath = GetCurrentSolutionPath();

            return GetSolutionNameByFile(solutionPath);
        }
        private static string GetSolutionNameByPath(string solutionPath)
        {
            return solutionPath.Split(new char[] { '\\', '/' })
                               .Where(e => string.IsNullOrEmpty(e) == false)
                               .Last();
        }
        private static string GetSolutionNameByFile(string solutionPath)
        {
            var fileInfo = new DirectoryInfo(solutionPath).GetFiles()
                                                          .SingleOrDefault(f => f.Extension.Equals(".sln", StringComparison.CurrentCultureIgnoreCase));

            return fileInfo != null ? Path.GetFileNameWithoutExtension(fileInfo.Name) : string.Empty;
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

        private static string[] GetPreprocessorDefinesInProjectFiles(string path)
        {
            var result = new List<string>();
            var files = Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file, Encoding.Default);

                foreach (var line in lines)
                {
                    var defines = line.ExtractBetween("<DefineConstants>", "</DefineConstants>");

                    if (defines.HasContent())
                    {
                        defines.Split(";", StringSplitOptions.RemoveEmptyEntries)
                               .ToList()
                               .ForEach(e =>
                               {
                                   var tmp = result.FirstOrDefault(x => x.RemoveAll("OFF", "ON") == e.RemoveAll("OFF", "ON"));

                                   if (string.IsNullOrEmpty(tmp))
                                   {
                                       result.Add(e);
                                   }
                               });
                    }
                }
            }
            return result.ToArray();
        }
        private static int SetPreprocessorDefinesInProjectFiles(string path, params string[] defineItems)
        {
            var files = Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories);
            var directives = string.Join(";", defineItems);

            foreach (var file in files)
            {
                var hasChanged = false;
                var result = new List<string>();
                var lines = File.ReadAllLines(file, Encoding.Default);

                foreach (var line in lines)
                {
                    if (line.Contains("<DefineConstants>", "</DefineConstants>"))
                    {
                        hasChanged = true;
                        result.Add(line.ReplaceBetween("<DefineConstants>", "</DefineConstants>", directives));
                    }
                    else
                    {
                        result.Add(line);
                    }
                }
                if (hasChanged == false && directives.Length > 0)
                {
                    var insertIdx = result.FindIndex(e => e.Contains("</PropertyGroup>"));

                    insertIdx = insertIdx < 0 ? result.Count - 2 : insertIdx;
                    hasChanged = true;

                    result.InsertRange(insertIdx + 1, new string[]
                        {
                            string.Empty,
                            "  <PropertyGroup>",
                            $"    <DefineConstants>{directives}</DefineConstants>",
                            "  </PropertyGroup>",
                        });
                }
                if (hasChanged)
                {
                    File.WriteAllLines(file, result.ToArray(), Encoding.Default);
                }
            }
            return files.Length;
        }
        private static void EditPreprocessorDefinesInRazorFiles(string path, params string[] defineItems)
        {
            foreach (var directive in defineItems)
            {
                var analyzeDirective = directive.ToUpper();

                if (analyzeDirective.EndsWith("_OFF", StringComparison.CurrentCultureIgnoreCase))
                {
                    SetPreprocessorDefinesCommentsInRazorFiles(path, directive.Replace("_OFF", "_ON"));
                    RemovePreprocessorDefinesCommentsInRazorFiles(path, directive);
                }
                else if (analyzeDirective.EndsWith("_ON", StringComparison.CurrentCultureIgnoreCase))
                {
                    SetPreprocessorDefinesCommentsInRazorFiles(path, directive.Replace("_ON", "_OFF"));
                    RemovePreprocessorDefinesCommentsInRazorFiles(path, directive);
                }
            }
        }
        private static void SetPreprocessorDefinesCommentsInRazorFiles(string path, params string[] defineItems)
        {
            var files = Directory.GetFiles(path, "*.cshtml", SearchOption.AllDirectories);

            foreach (var directive in defineItems)
            {
                foreach (var file in files)
                {
                    var startIndex = 0;
                    var hasChanged = false;
                    var result = string.Empty;
                    var text = File.ReadAllLines(file, Encoding.Default)
                                   .Select(l =>
                                   {
                                       if (l.Contains($"@*#if {directive}*@") || l.Contains("@*#endif*@"))
                                       {
                                           l = l.Trim();
                                       }
                                       return l;
                                   }).ToText();

                    foreach (var tag in text.GetAllTags($"@*#if {directive}*@", "@*#endif*@"))
                    {
                        if (tag.StartTagIndex > startIndex)
                        {
                            result += text.Partialstring(startIndex, tag.StartTagIndex - 1);
                            result += tag.StartTag;
                            if (tag.InnerText.Trim().StartsWith("@*"))
                            {
                                result += tag.InnerText;
                            }
                            else
                            {
                                hasChanged = true;
                                result += /*Environment.NewLine + */"@*";
                                result += tag.InnerText;
                                result += "*@";// + Environment.NewLine;
                            }
                            result += tag.EndTag;
                            startIndex += tag.EndTagIndex + tag.EndTag.Length;
                        }
                    }
                    if (hasChanged && startIndex < text.Length)
                    {
                        result += text.Partialstring(startIndex, text.Length);
                    }
                    if (hasChanged)
                    {
                        File.WriteAllText(file, result, Encoding.Default);
                    }
                }
            }
        }
        private static void RemovePreprocessorDefinesCommentsInRazorFiles(string path, params string[] defineItems)
        {
            var files = Directory.GetFiles(path, "*.cshtml", SearchOption.AllDirectories);

            foreach (var directive in defineItems)
            {
                foreach (var file in files)
                {
                    var startIndex = 0;
                    var hasChanged = false;
                    var result = string.Empty;
                    var text = File.ReadAllText(file, Encoding.Default);

                    foreach (var tag in text.GetAllTags($"@*#if {directive}*@", "@*#endif*@"))
                    {
                        if (tag.StartTagIndex > startIndex)
                        {
                            result += text.Partialstring(startIndex, tag.StartTagIndex - 1);
                            result += tag.StartTag;
                            var innerText = tag.InnerText.Trim(Environment.NewLine.ToCharArray());
                            if (innerText.Trim().StartsWith("@*") && innerText.Trim().EndsWith("*@"))
                            {
                                hasChanged = true;
                                result += innerText.Partialstring(2, innerText.Length - 5);
                                result += Environment.NewLine;
                            }
                            else
                            {
                                result += tag.InnerText;
                            }
                            startIndex += tag.EndTagIndex + tag.EndTag.Length;
                            result += tag.EndTag;
                        }
                    }
                    if (hasChanged && startIndex < text.Length)
                    {
                        result += text.Partialstring(startIndex, text.Length);
                    }
                    if (hasChanged)
                    {
                        File.WriteAllText(file, result, Encoding.Default);
                    }
                }
            }
        }
        private static void ReplacePreprocessorDefinesInRazorFiles(string path, params string[] defineItems)
        {
            var files = Directory.GetFiles(path, "*.cshtml", SearchOption.AllDirectories);

            foreach (var directive in defineItems)
            {
                var labels = new[] { $"#if {directive}", "#endif" };

                foreach (var file in files)
                {
                    var hasChanged = false;
                    var result = new List<string>();
                    var lines = File.ReadAllLines(file, Encoding.Default);

                    foreach (var line in lines)
                    {
                        var targetLine = line;

                        foreach (var label in labels)
                        {
                            if (line.StartsWith(label))
                            {
                                hasChanged = true;
                                targetLine = line.Replace(label, $"@*{label}*@");
                            }
                        }
                        result.Add(targetLine);
                    }
                    if (hasChanged)
                    {
                        File.WriteAllLines(file, result, Encoding.Default);
                    }
                }
            }
        }
    }
}
//MdEnd
