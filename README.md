# unity-freecam-melonloader
Very simple Unity il2cpp freecam melonloader mod with XInput controller and keyboard support that should work on most il2cpp Unity games.

![Preview](media/preview.gif)


# Installation
**⚠️ Backup game files before installing**
1. Download and install [MelonLoader](https://github.com/LavaGang/MelonLoader)
2. Download the Unity freecam from the [releases](https://github.com/oiyl/unity-freecam-melonloader/releases/) page
3. Place the `Keyboard/Mods/unity_freecam.dll` in your MelonLoader installation directory's `Mods` folder (Alternatively use `Controller` folder for controllers)
4. For controller support, place `Controller/UserLib`s in your MelonLoader installation directory `UserLibs` folder, and place `XInputInterface.dll` in your MelonLoader installation directory


# Development
**⚠️ Run MelonLoader at least once to make sure the assemblies are downloaded**
1. Copy "MelonLoader.dll" and "Il2CppInterop.Runtime.dll" from your `MelonLoader\net6\` path into the projects `libs\` folder

2. Copy the library content from `MelonLoader\Il2CppAssemblies\` into the projects `libs\` directory

3. Add references to all of them (Visual Studio should automatically include everything)

# Controls
`F5` to toggle freecam

Keyboard / Mouse: W, A, S, D, Ctrl, Space

Controller: Joysticks

# Supported platforms
* As far as MelonLoader supports it 
* XInput functionality only works on Windows 
* MelonLoader v0.6.2 OpenBeta ( x64, Il2cpp, net6, Unity Version 2019.4.11f1 )
* Literally made this just for Digimon World: Next Order

# Notes
* Should lock input yourself if needed, Will Be Done One Day™
* If using a controller on Steam, disable SteamInput as this disables XInput and the XInput wrapper will not work
* Only works with an XInput controller. switching out the library for another (Such as SDL2 for DirectInput) should be easy.

# Support
If you encounter any issues or have suggestions for improvement, please open an issue on GitHub.

# Acknowledgements
* The MelonLoader community
* Speps for their  [XInput C# wrapper](https://github.com/speps/XInputDotNet)  
* Sinai for their [UnityExplorer mod](https://github.com/sinai-dev/UnityExplorer) and general inspiration for this

# License
The MIT License, available in the [LICENSE](https://github.com/oiyl/unity-freecam-melonloader/blob/master/LICENSE) file
