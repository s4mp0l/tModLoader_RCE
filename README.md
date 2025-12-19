# tModLoader RCE

## So what's the problem?

tModLoader (tML), the official modding framework for Terraria (free Steam DLC), enables seamless multiplayer modding by allowing hosts to require specific mods, which clients automatically detect and prompt to download/install upon joining. Mods are C# assemblies loaded with full user privileges into the game's .NET 8 process, no sandboxing, signing, or restrictions enforce execution safety.

This is not a new problem... Exploitation began around 2016 with RATs and info-stealers via modded servers. In 2022, a partial "sandbox" was introduced to limit some APIs, but it is easily bypassed.

For example, Minecraft is safer because mod loaders like Forge and Fabric require exact mod matches on client and server, no automatic downloads from the host. Clients must manually install the same mods from trusted sources (e.g., CurseForge).

## Proof Of Concept

The folder `tModLoader_RCE` is the mod that has the PoC which executes commands on the client's machine.

Basically, this simple mod overrides the `Load()` method, which runs automatically when the mod is enabled and loaded.

It detects the client's OS using `RuntimeInformation.IsOSPlatform` and executes the corresponding payload depending on the operating system. As a proof of concept, it simply writes a .txt file in the user's directory.

Here is a step-by-step guide of how to setup the environment.

As a simple test in single player you can use the following stepts:

1. Install tModLoader from Steam.
2. Launch tModLoader > Workshop > Develop Mods > Create Mod.
3. Replace the generated mod_name.cs with the provided code.
4. In Develop Mods > Select your mod > Build + Reload.
5. Go to Mods menu > Enable your mod > Reload.
6. Then you should create a world and test if the file was created.

If you want to use a dedicated server you can use the following stepts:

1. Download dedicated server files from [terraria.org](https://terraria.org/).
2. Create the server following the [wiki](https://terraria.fandom.com/wiki/Guide:Setting_up_a_Terraria_server)
3. Enable your built mod > place .tmod in server Mods folder > edit enabled.json.
4. Share IP:Port and convince clients to join your malicious server.

We have different ways of delivering our payload. Check the following section.

## Delivery Techniques

- P2P: Host enables malicious mod > Client joins > Prompted: "Download from host?".
- Steam Workshop: Upload malicious mod > Victims subscribe manually and enable it.

## How to Avoid This Vulnerability

1. Play with friends privately.
2. Disable Auto-Downloads from tML settings. It blocks P2P but Steam Workshop is still a valid delivery method.

## Limitations

It's a 1-Click Social Engineering exploit: Prompt requires "Yes" to download or manual subscribe/enable from Workshop.