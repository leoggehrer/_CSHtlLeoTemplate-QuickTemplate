//@CodeCopy
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
            defines = new string[]
            {
                "ACCOUNT_OFF",
                "ACCESSRULES_ON",
                "LOGGING_OFF",
                "REVISION_OFF",
                "DEVELOP_OFF",
                "ROWVERSION_ON",
                "GUID_OFF",
                "CREATED_OFF",
                "MODIFIED_OFF",
                "CREATEDBY_OFF",
                "MODIFIEDBY_OFF",
                "IDINT_ON",
                "IDLONG_OFF",
                "IDGUID_OFF",
                "SQLSERVER_ON",
                "SQLITE_OFF"
            };
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        #endregion Class-Constructors

        #region Properties
        private static string? HomePath { get; set; }
        private static string UserPath { get; set; }
        private static string SourcePath { get; set; }
        private static string[] defines { get; set; }
        #endregion Properties

        private static void Main(/*string[] args*/)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            RunApp();
        }

        #region Console methods
        private static readonly bool canBusyPrint = true;
        private static bool runBusyProgress = false;
        private static void RunApp()
        {
            var input = string.Empty;
            var saveForeColor = Console.ForegroundColor;

            PrintBusyProgress();
            while (input!.Equals("x") == false)
            {
                var changedDefines = false;
                var sourceSolutionName = GetSolutionNameByPath(SourcePath);

                // Read defines from the solution
                defines = GetPreprocessorDefinesInProjectFiles(SourcePath, defines);

                runBusyProgress = false;
                Console.Clear();
                Console.ForegroundColor = saveForeColor;
                Console.WriteLine("Template Preprocessor");
                Console.WriteLine("=====================");
                Console.WriteLine();
                PrintDefines(defines);
                Console.WriteLine();
                Console.WriteLine($"Set define-values '{sourceSolutionName}' from: {SourcePath}");
                Console.WriteLine();
                PrintMenu(defines);
                Console.WriteLine();
                Console.Write("Choose: ");

                input = Console.ReadLine()?.ToLower() ?? String.Empty;
                var numbers = input?.Trim()
                    .Split(',').Where(s => Int32.TryParse(s, out int n))
                    .Select(s => Int32.Parse(s))
                    .ToArray();

                for (int n = 0; n < numbers!.Length; n++)
                {
                    var select = numbers[n];

                    if (select == 1)
                    {
                        var solutionPath = GetCurrentSolutionPath();
                        var qtProjects = GetQuickTemplateProjects(solutionPath).Union(new[] { solutionPath }).ToArray();

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
                                defines = GetPreprocessorDefinesInProjectFiles(SourcePath, defines);
                            }
                        }
                        else if (string.IsNullOrEmpty(selectOrPath) == false && Directory.Exists(selectOrPath))
                        {
                            SourcePath = selectOrPath;
                            defines = GetPreprocessorDefinesInProjectFiles(SourcePath, defines);
                        }
                    }
                    else if ((select - 2) >= 0 && (select - 2) < defines.Length)
                    {
                        var defIdx = select - 2;

                        changedDefines = true;
                        SwitchDefine(defines, defIdx);
                    }
                    else if ((select - 2) == defines.Length)
                    {
                        PrintBusyProgress();
                        SetPreprocessorDefinesInProjectFiles(SourcePath, defines);
                        SetPreprocessorDefinesInRazorFiles(SourcePath, defines);
                    }
                }

                if (changedDefines)
                {
                    PrintBusyProgress();
                    SetPreprocessorDefinesInProjectFiles(SourcePath, defines);
                    SetPreprocessorDefinesInRazorFiles(SourcePath, defines);
                }

                changedDefines = false;
                runBusyProgress = false;
                Console.ResetColor();
            }
        }
        private static void SwitchDefine(string[] defines, int idx)
        {
            if (idx >= 0 && idx < defines.Length)
            {
                if (defines[idx].EndsWith("_ON"))
                {
                    if (defines[idx].StartsWith("IDINT_") == false
                        && defines[idx].StartsWith("IDLONG_") == false
                        && defines[idx].StartsWith("IDGUID_") == false
                        && defines[idx].StartsWith("SQLSERVER_") == false
                        && defines[idx].StartsWith("SQLITE_") == false)
                    {
                        defines[idx] = defines[idx].Replace("_ON", "_OFF");
                    }
                }
                else
                {
                    if (defines[idx].StartsWith("IDINT_") == true)
                    {
                        SwitchDefine(defines, "IDINT_", "ON");
                        SwitchDefine(defines, "IDLONG_", "OFF");
                        SwitchDefine(defines, "IDGUID_", "OFF");
                    }
                    else if (defines[idx].StartsWith("IDLONG_") == true)
                    {
                        SwitchDefine(defines, "IDINT_", "OFF");
                        SwitchDefine(defines, "IDLONG_", "ON");
                        SwitchDefine(defines, "IDGUID_", "OFF");
                    }
                    else if (defines[idx].StartsWith("IDGUID_") == true)
                    {
                        SwitchDefine(defines, "IDINT_", "OFF");
                        SwitchDefine(defines, "IDLONG_", "OFF");
                        SwitchDefine(defines, "IDGUID_", "ON");
                    }
                    else if (defines[idx].StartsWith("SQLSERVER_") == true)
                    {
                        SwitchDefine(defines, "SQLITE_", "OFF");
                        SwitchDefine(defines, "SQLSERVER_", "ON");
                    }
                    else if (defines[idx].StartsWith("SQLITE_") == true)
                    {
                        SwitchDefine(defines, "SQLSERVER_", "OFF");
                        SwitchDefine(defines, "SQLITE_", "ON");
                    }
                    else
                    {
                        defines[idx] = defines[idx].Replace("_OFF", "_ON");
                    }
                }
            }
        }
        private static void SwitchDefine(string[] defines, string definePrefix, string definePostfix)
        {
            bool hasSet = false;

            for (int i = 0; i < defines.Length && hasSet == false; i++)
            {
                if (defines[i].StartsWith(definePrefix))
                {
                    hasSet = true;
                    defines[i] = $"{definePrefix}{definePostfix}";
                }
            }
        }
        private static void PrintBusyProgress()
        {
            if (runBusyProgress == false)
            {
                var sign = "\\";

                Console.WriteLine();
                runBusyProgress = true;
                Task.Factory.StartNew(async () =>
                {
                    while (runBusyProgress)
                    {
                        if (canBusyPrint)
                        {
                            if (Console.CursorLeft > 0)
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                            Console.Write($".{sign}");
                            sign = sign == "\\" ? "/" : "\\";
                        }
                        await Task.Delay(250).ConfigureAwait(false);
                    }
                });
            }
        }
        private static void PrintDefines(string[] defines)
        {
            Console.WriteLine("Define-Values:");
            Console.WriteLine("--------------");
            foreach (var define in defines)
            {
                PrintDefine(define);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        private static void PrintDefine(string define)
        {
            var saveColor = Console.ForegroundColor;

            if (define.EndsWith("_ON"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            Console.Write($"{define}");
            Console.ForegroundColor = saveColor;
        }
        private static void PrintMenu(string[] defines)
        {
            var menuIndex = 0;
            var saveColor = Console.ForegroundColor;

            Console.WriteLine($"[{++menuIndex,-2}] Change source path");

            for (int i = 0; i < defines.Length; i++)
            {
                if (defines[i].EndsWith("_ON"))
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"[{++menuIndex,-2}] Set definition ");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{defines[i],-15}");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ==> ");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{defines[i].Replace("_ON", "_OFF")}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"[{++menuIndex,-2}] Set definition ");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{defines[i],-15}");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ==> ");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{defines[i].Replace("_OFF", "_ON")}");
                }
            }
            Console.ForegroundColor = saveColor;
            Console.WriteLine($"[{++menuIndex,-2}] Start assignment process...");
            Console.WriteLine("[x|X] Exit");
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
        #endregion Console methods

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
        private static string[] GetQuickTemplateProjects(string sourcePath)
        {
            var directoryInfo = new DirectoryInfo(sourcePath);
            var parentDirectory = directoryInfo.Parent != null ? directoryInfo.Parent.FullName : SourcePath;
            var qtDirectories = Directory.GetDirectories(parentDirectory, "QT*", SearchOption.AllDirectories)
                                         .Where(d => d.Replace(UserPath, String.Empty).Contains('.') == false)
                                         .ToList();
            return qtDirectories.ToArray();
        }

        private static string[] GetPreprocessorDefinesInProjectFiles(string path, string[] startDefines)
        {
            var result = new List<string>();
            var files = Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories)
                                 .Where(f => f.Contains(".AngularApp") == false);

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

            foreach (var startDefine in startDefines)
            {
                var tmp = result.FirstOrDefault(x => x.RemoveAll("OFF", "ON") == startDefine.RemoveAll("OFF", "ON"));

                if (string.IsNullOrEmpty(tmp))
                {
                    result.Add(startDefine);
                }
            }
            return result.ToArray();
        }
        private static void SetPreprocessorDefinesInProjectFiles(string path, params string[] defineItems)
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
        }
        private static void SetPreprocessorDefinesInRazorFiles(string path, params string[] defineItems)
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
