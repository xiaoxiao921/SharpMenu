# SharpMenu made possible by BigBaseV2
A mod menu base in C# for Grand Theft Auto V.
Strictly for educational purposes.

This is largely based on BigBaseV2.

Right now, fiber pool, native invoking, and hooks are the features, this is very wip.

## Structure
SharpHost is C++ library that you inject in the target process, its here for hosting the net6 coreclr runtime.

Until a proper launcher / release is made you'll have to do that and put the following stuff in AppData manually : 

- The first time you'll ever inject it'll create a folder in `%appdata%\SharpHost`, it expect the SharpLoader.dll to sit at `%appdata%\SharpHost\SharpLoader.dll` so you'll have to inject twice when first setting stuff up unless you create the needed folders yourself (and reload the game too).

- When in the correct folder, SharpLoader will be loaded automatically by SharpHost when injected, allowing you to load assemblies that are in `%appdata%\SharpHost\plugins` with F5 and unload them with F6.

- Put SharpMenu.dll in plugins which contains the BigBaseV2 equivalent in csharp.

## Build environment
To set up the build environment, run the following commands in a terminal:
```dos
git clone <ThisRepoURL> --recursive
cd SharpHost
GenerateProjects.bat
```
It'll generate a .sln for VS, but : you'll have to add the c# projects manually until a script that does it is done (Add Existing Project, select the .csproj files)


