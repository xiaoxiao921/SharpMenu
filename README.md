# SharpMenu made possible by BigBaseV2
A mod menu base in C# for Grand Theft Auto V.
Strictly for educational purposes.

This is **largely** based on [BigBaseV2 from spankerincrease](https://bitbucket.org/gir489/bigbasev2-fix/) and [YimMenu from Yimura](https://github.com/Yimura/YimMenu), please check them out.

Right now, fiber pool, native invoking, hooks and barebone ImGui are the features, this is very wip.

## Structure
SharpHost is C++ library that you inject in the target process, its here for hosting the net6 coreclr runtime.

Until a proper launcher / release is made you'll have to do that and put the following stuff in AppData manually : 

- The first time you'll ever inject it'll create a folder in `%appdata%\SharpHost`, it expect the SharpLoader.dll to sit at `%appdata%\SharpHost\SharpLoader.dll` so you'll have to inject twice when first setting stuff up unless you create the needed folders yourself (and reload the game too).

- When in the correct folder, SharpLoader will be loaded automatically by SharpHost when injected, allowing you to load assemblies that are in `%appdata%\SharpHost\plugins` with F5 and unload them with F6.

- Put SharpMenu.dll in plugins which contains the BigBaseV2 equivalent in csharp.

## Build environment
To set up the build environment : 
- Make sure Net 6 SDK is installed
- You may need to edit the #pragma lib in SharpHost to target your actual host path
- Run the following commands in a terminal:
```dos
git clone <ThisRepoURL> --recursive
cd SharpHost
GenerateProjects.bat
```
It'll generate a .sln for VS, the .csproj that are generated from premake actually contains a lot of useless stuff but it should still work.
