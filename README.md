<div align="center">

# Flappy Luffy

**A 2D one-button endless-flyer game built in Unity, with progression and rewards added**

![Unity](https://img.shields.io/badge/Unity_6000.2-000000?style=flat&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-512BD4?style=flat&logo=dotnet&logoColor=white)
![URP](https://img.shields.io/badge/Universal_Render_Pipeline-222222?style=flat)

</div>

---

## Overview

Flappy Luffy is a take on the classic Flappy Bird loop. You press **Space** to flap, fly through gaps between pipes, and collect coins along the way. Scoring reaches milestone rewards. The project is organized into small, single-purpose C# scripts: game state, spawning, rewards, audio and UI.

## Features

- **Two difficulty levels.** *Easy* and *Hard* each change flap strength, pipe speed, gap size and spawn rate. The choice is saved between sessions.
- **Character selection.** A shop screen lets you preview and pick a character sprite. The selection is saved with `PlayerPrefs`.
- **Coins and rewards:**
  - a **Silver cup** after 10 pipes and a **Gold cup** after 20
  - a 5-second **shield** after collecting 10 coins
- **Win condition.** You win either by passing a set number of pipes (20 by default) or by surviving for a set time.
- **Audio.** Background music plus sound effects for scoring, collecting, hitting and winning, handled by an `AudioManager`.
- **Scenes.** A start menu with level selection, and the main game scene.

## Scripts

| Script | Responsibility |
|---|---|
| `GameManager.cs` | Persistent singleton holding the selected difficulty and its settings |
| `LogicScript.cs` | Score, win/lose state and game-over/win screens |
| `PlayerScript.cs` | Flap input, physics and collisions; applies the difficulty and character settings |
| `PipeSpawner.cs` · `PipeMovement.cs` · `PipeGapController.cs` | Spawning, movement and gap sizing for the pipes |
| `MiddlePipeScript.cs` | Trigger that counts each pipe passed |
| `CoinSpawner.cs` · `Coin.cs` | Coin spawning and collection |
| `RewardSystem.cs` | Cup milestones and the shield power-up |
| `CharacterManager.cs` · `ShopUIManager.cs` | Character roster, selection and shop UI |
| `StartMenuScript.cs` | Start menu and level selection |
| `AudioManager.cs` | Music and sound effects |

## Getting started

1. Install **Unity 6000.2.7f2** through Unity Hub. Other Unity 6 versions may also work.
2. Clone the repository and open the project folder in Unity Hub.
3. Open `Assets/Scenes/StartMenu.unity` and press **Play**.

**Controls:** press `Space` to flap.

## Documentation

- [Game Design Document](GAME_DESIGN_DOCUMENT.md)
- [Setup Guide](SETUP_GUIDE.md): notes on how the scene objects are wired up

## Disclaimer

This is a non-commercial fan project made for learning. The characters and the background artwork belong to their respective owners.

---

**Author:** [Saed Alsafadi](https://github.com/SaedAlsafadi)
