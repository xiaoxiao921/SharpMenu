# SharpMenu made possible by BigBaseV2
A mod menu base for Grand Theft Auto V.
Strictly for educational purposes.

This is largely based on BigBaseV2.

Right now, fiber pool, native invoking, and hooks are the features, this is very wip.

SharpHost is a cpp dll that you inject in the target process, its here for hosting the net6 coreclr runtime,

the first time you'll ever inject it'll create a folder in `%appdata%\SharpHost`, it expect the SharpLoader.dll to sit at `%appdata%\SharpHost\SharpLoader.dll` so you'll have to inject twice (and reload the game too)

Until a proper launcher / release is made you'll have to do that and put stuff in AppData manually

Once that done, SharpLoader will be loaded automatically by SharpHost when injected, allowing you to load assemblies that are in ``%appdata%\SharpHost\plugins` with F5 and unload them with F6

You put SharpMenu.dll in plugins which contains the BigBaseV2 equivalent in csharp.

To set up the build environment, run the following commands in a terminal:
```dos
git clone URL --recursive
cd SharpHost
GenerateProjects.bat
```
It'll generate a .sln for VS, you'll have to add the c# projects manually until I tackle a script that does it automatically (Add Existing Project, select the .csproj files)


