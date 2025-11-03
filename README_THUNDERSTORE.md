# Valheim Clock

A simple and elegant analog clock that displays Valheim's in-game time.

## Features

- **Analog clock display** showing the day/night cycle
- **Pixel-perfect rendering** (100x100 pixels)
- **Transparent background** for clean integration
- **Only shows in-game** - no clock in menus
- **Fully configurable** via BepInEx config

## Installation

### With Mod Manager (Recommended)
1. Install [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or Thunderstore Mod Manager
2. Click "Install with Mod Manager" button
3. Launch the game!

### Manual Installation
1. Install [BepInExPack Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod
3. Extract the ZIP file
4. Copy the `ValheimClock` folder to `BepInEx/plugins/`
5. Launch the game!

## Configuration

After first launch, a config file will be created at:
`BepInEx/config/com.yourname.valheimwatch.cfg`

**Available settings:**
- `Enabled` - Enable/disable the clock (default: true)
- `ClockSize` - Size in pixels (default: 100)

## Troubleshooting

**Clock doesn't appear:**
- Make sure BepInEx is installed correctly
- Check BepInEx console (F5 in-game) for errors
- Verify both DLL and PNG files are in the plugins folder

**Clock is the wrong size:**
- Edit the config file and change `ClockSize`
- Restart the game for changes to take effect

## Changelog

### 1.0.0
- Initial release
- Analog clock showing in-game time
- Configurable size and position
- Transparent clock face with custom hands
