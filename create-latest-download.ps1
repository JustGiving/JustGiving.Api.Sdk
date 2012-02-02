cd DotNet\src\
cmd /c create-package.bat
cd ..\..

xcopy DotNet\src\justgiving-sdk.*.nupkg * /Y
xcopy DotNet\src\justgiving-sdk.*.nupkg justgiving-sdk.*.zip /Y

Add-Type -TypeDefinition @"
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class DownloadBuilder
{
    public static void Package(string path)
    {
        string packageZip = GenerateFileName(path);
        ExecuteCommandSync("Tools\\7za.exe d -r " + packageZip + " _rels");
        ExecuteCommandSync("Tools\\7za.exe d -r " + packageZip + " package");
        ExecuteCommandSync("Tools\\7za.exe d -r " + packageZip + " [Content_Types].xml");
        ExecuteCommandSync("Tools\\7za.exe d -r " + packageZip + " justgiving-sdk.nuspec");
        ExecuteCommandSync("Tools\\7za.exe a -r " + packageZip + " PHP");
        
        Console.WriteLine(packageZip);
    }
    
    public static string GenerateFileName(string path)
    {
        string fileName = string.Empty;
        foreach( string file in Directory.GetFiles(path))
        {
            if (file.EndsWith(".nupkg"))
            {
                fileName = file.Replace(".nupkg", ".zip");
            }
        }
        return fileName;   
    }
    
    public static void ExecuteCommandSync(string command)
    {
          System.Diagnostics.ProcessStartInfo procStartInfo =
          new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
          procStartInfo.RedirectStandardOutput = true;
          procStartInfo.UseShellExecute = false;
          procStartInfo.CreateNoWindow = true;
          System.Diagnostics.Process proc = new System.Diagnostics.Process();
          proc.StartInfo = procStartInfo;
          proc.Start();
          string result = proc.StandardOutput.ReadToEnd();
          Console.WriteLine(result);          
    }
}
"@

[DownloadBuilder]::Package((Get-Location -PSProvider FileSystem).ProviderPath);

exit