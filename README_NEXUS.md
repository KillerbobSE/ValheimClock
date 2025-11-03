# Valheim Clock - Analog In-Game Clock

## Description
A simple and elegant analog clock that displays the in-game time in Valheim. The clock appears in the top center of the screen when you're playing.

## Features
- **Analog clock display** showing Valheim's day/night cycle
- **Pixel-perfect rendering** (100x100 pixels)
- **Transparent background** for clean integration
- **Only shows in-game** - no clock in menus
- **Configurable** - can be disabled via config file

## Installation

### Requirements
- **BepInEx 5.4.x** or later
- Valheim (latest version)

### Manual Installation
1. Install [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/) if you haven't already
2. Download this mod
3. Extract the ZIP file
4. Copy `ValheimPlugin.dll` and `clock.png` to `BepInEx/plugins/` folder in your Valheim installation
5. Launch the game!

## Configuration
The mod creates a config file at `BepInEx/config/com.killerbob.valheimclock.cfg`

Available settings:
- **Enabled** (true/false) - Enable or disable the clock
- **ClockSize** (100) - Size of the clock in pixels

## Troubleshooting

### Clock doesn't appear
- Make sure both `ValheimPlugin.dll` and `clock.png` are in the `BepInEx/plugins/` folder
- Check the BepInEx log file (`BepInEx/LogOutput.log`) for errors

### Clock is too big/small
- Edit the config file and change `ClockSize` value
- Restart the game

## Credits
Created for the Valheim modding community

## Changelog

### Version 1.0.0
- Initial release
- Analog clock showing in-game time
- Transparent clock face
- Configurable size
