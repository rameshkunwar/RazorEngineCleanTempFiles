### RazorEngine temporary files deletion using proxy domain.

From the RazorEngine [Github page](https://github.com/Antaris/RazorEngine),

> RazorEngine tries hard to delete the temporary files it creates, but this is not always possible. This is especially true if you run RazorEngine from the default AppDomain. RazorEngine will warn you in this situation by writing to the stderr.

The given solution works for console application. It unloads the proxy Appdomain when exiting the main. This approach won't work if the program is run as a Windows services.

MarshalByRefObject is used to access to objects across application domain boundaries.

### Installation

1. Install all Nuget packages by right-clicking the Solution (on Visual Studio) and selecting Restore Nuget Packages.
2. Installing Service:

   - Go to the root folder of the project, _RazorEngineMailerParserService_
   - Go to /bin/release (remember to compile using Release compiler directive, though debug will be fine for development)
   - Go to the address bar and copy the path, remember to include executable name, _RazorEngineMailerParserService.exe_
   - Open command prompt as an administratory, go to `C:\Windows\Microsoft.NET\Framework\v4.0.30319>`
   - Type following `C:\Windows\Microsoft.NET\Framework\v4.0.30319>InstallUtil.exe D:\Workspace\RazorEngine\RazorEngine\RazorEngineMailerParserService\bin\Release\RazorEngineMailerParserService.exe`
   - Press Windows key + r to open _Run_ and type _services.msc_
   - Find the service and run it.

### The console application **GenerateEmail** can be run just by pressing F5 (make sure it's the startup project).
