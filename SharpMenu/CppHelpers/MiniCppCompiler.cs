using SharpMenu.Extensions;
using System.ComponentModel;
using System.Diagnostics;

namespace SharpMenu.CppHelpers
{
    internal static class MiniCppCompiler
    {
        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        private static string GetCppFunctionName(string cppSource)
        {
            var end = cppSource.IndexOf('(', cppSource.IndexOf('(', 0) + 1);

            for (int i = end; i >= 0; i--)
            {
                if (cppSource[i] == ' ')
                {
                    var start = i + 1;
                    return cppSource.Substring(start, end - start);
                }
            }

            return null;
        }

        public static unsafe void* GetFunctionPointer(byte[] functionBytes)
        {
            fixed (byte* functionPtr = functionBytes)
            {
                var functionIntPtr = (IntPtr)functionPtr;

                // Mark memory as EXECUTE_READWRITE to prevent DEP exceptions
                if (!VirtualProtectEx(Process.GetCurrentProcess().Handle, functionIntPtr,
                    (UIntPtr)functionBytes.Length, 0x40 /* EXECUTE_READWRITE */, out uint _))
                {
                    throw new Win32Exception();
                }

                return (void*)functionIntPtr;
            }
        }

        public static byte[] Compile(string cppSource)
        {
            var cppFunctionName = GetCppFunctionName(cppSource);
            Log.Info("cppFunctionName : " + cppFunctionName);

            var cppFunctionNameCOMDAT = cppFunctionName + ", COMDAT";

            var tempFolder = Path.GetTempPath();
            tempFolder = Path.Combine(tempFolder, "cpp_in_csharp");
            Directory.CreateDirectory(tempFolder);
            var vcVarsFilePath = Path.Combine(tempFolder, "vcvars.txt");

            Log.Info("temp folder : " + tempFolder);

            var cppFilePath = Path.Combine(tempFolder, "x64_asm_output.cpp");
            File.WriteAllText(cppFilePath, cppSource);

            var cppCompileScriptPath = Path.Combine(tempFolder, "cpp_compiler.ps1");
            var cppCompileScriptText =
                "cmd.exe /c \"call `\"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\VC\\Auxiliary\\Build\\vcvars64.bat`\" && set > " + vcVarsFilePath + "\"\n" +
                "Get-Content \"" + vcVarsFilePath + "\" | Foreach-Object {\n" +
                "   if ($_ -match \"^(.*?)=(.*)$\") {\n" +
                    "   Set-Content \"env:\\$($matches[1])\" $matches[2]\n" +
                "   }\n" +
                "}\n" +
                "cl /FAcsu /O2 " + "\"" + cppFilePath + "\"";
            File.WriteAllText(cppCompileScriptPath, cppCompileScriptText);

            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = @"C:\windows\system32\windowspowershell\v1.0\powershell.exe";
            process.StartInfo.Arguments = "\"&'" + cppCompileScriptPath + "'\"";
            process.StartInfo.WorkingDirectory = tempFolder;

            process.Start();

            string clCompilerOutput = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (!(clCompilerOutput.Contains(".exe") && clCompilerOutput.Contains(".obj")))
            {
                Log.Info(clCompilerOutput);
            }

            var startText = cppFunctionNameCOMDAT;
            var endText = "ENDP";
            var endText2 = cppFunctionName;

            var startOfAsmBytesText = "00000";

            var asmFileOutput = Path.Combine(tempFolder, "x64_asm_output.cod");

            bool soonStartOfAsm = false;
            bool startOfAsm = false;

            List<byte> asmBytes = new();
            List<string> formatedText = new();
            foreach (var line in File.ReadAllLines(asmFileOutput))
            {
                if (line.Contains(startText))
                {
                    soonStartOfAsm = true;
                    continue;
                }

                if (soonStartOfAsm && line.Contains(startOfAsmBytesText))
                {
                    startOfAsm = true;
                }

                if (startOfAsm)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith(';'))
                    {
                        continue;
                    }

                    if (line.Contains(endText) && line.Contains(endText2))
                    {
                        break;
                    }

                    var asmLineSplit = line.Split('\t', 3);
                    var currentLineAsmBytes = asmLineSplit[1].ParseAsmBytes();
                    var currentLineAsmText = asmLineSplit[2];

                    var formatedLine = string.Format("{0,-16} {1,-16}", asmLineSplit[1].Replace("\t", ""), currentLineAsmText.Replace("\t", ""));
                    Log.Info(formatedLine);
                    formatedText.Add(formatedLine);

                    asmBytes.AddRange(currentLineAsmBytes);
                }
            }

            Directory.Delete(tempFolder, true);

            return asmBytes.ToArray();
        }
    }
}
