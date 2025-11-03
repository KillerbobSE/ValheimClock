# ValheimClock

A simple and elegant analog clock mod for Valheim that displays the in-game time.

![Valheim Clock](clock.png)

## Features

- 🕐 **Analog clock display** showing Valheim's day/night cycle
- 🎯 **Pixel-perfect rendering** (100x100 pixels)
- ✨ **Transparent background** for clean integration
- 🎮 **Only shows in-game** - no clock in menus
- ⚙️ **Fully configurable** via BepInEx config

## Installation

### With Mod Manager (Recommended)
1. Install [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or Thunderstore Mod Manager
2. Search for "ValheimClock"
3. Click "Install"
4. Launch the game!

### Manual Installation
1. Install [BepInExPack Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download the latest release
3. Extract the ZIP file
4. Copy `ValheimPlugin.dll` and `clock.png` to `BepInEx/plugins/`
5. Launch the game!

## Configuration

After first launch, a config file will be created at:
`BepInEx/config/com.killerbob.valheimclock.cfg`

**Available settings:**
- `Enabled` (true/false) - Enable/disable the clock
- `ClockSize` (100) - Size in pixels

## Troubleshooting

**Clock doesn't appear:**
- Make sure both DLL and PNG files are in the plugins folder
- Check BepInEx console (F5 in-game) for errors
- Verify BepInEx is installed correctly

**Clock is the wrong size:**
- Edit the config file and change `ClockSize`
- Restart the game for changes to take effect

## Development

Built with:
- BepInEx 5.4.x
- .NET Framework 4.8
- Unity UI system

## Contributing

Found a bug or have a suggestion? Open an issue on GitHub!

## License

MIT License - feel free to modify and redistribute

## Changelog

### 1.0.0 (2025-11-03)
- Initial release
- Analog clock showing in-game time
- Configurable size and position
- Transparent clock face with custom hands
- Pixel-perfect rendering

## Credits

Created by Killerbob for the Valheim modding community

## Support

If you enjoy this mod, consider endorsing it on Thunderstore or Nexus Mods!

