using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xxtea;

namespace Example {
    class MainClass {
        public static void Main (string[] args) {

#if DEBUG
            var workspace = @"D:\yunding\assets";
#else
            var commandLineArgs = Environment.GetCommandLineArgs();
            var workspace = Path.GetDirectoryName(commandLineArgs[0]);
#endif

            var files = Directory.GetFiles(workspace, "*.png", SearchOption.AllDirectories);

            foreach(var path in files)
            {
                var bytes = File.ReadAllBytes(path);

                bytes = bytes.Skip("YD0888ResPackage".Length).ToArray();

                bytes = XXTEA.Decrypt(bytes, "G9w0BAQefAa0888M");

                if (bytes == null)
                    continue;

                File.WriteAllBytes(path, bytes);
            }
        }
    }
}
