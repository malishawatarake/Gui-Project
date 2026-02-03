# Space Shooter Game

A classic 2D arcade-style space shooter built with **C#** and **Windows Forms**.

Control your spaceship, shoot incoming enemies, avoid their fire, collect points, and survive as long as possible (or until level 10!).

https://github.com/malishawatarake/space-shooter-csharp



## Features

- Arrow key movement (Up / Down / Left / Right)
- Automatic continuous shooting
- Multiple enemy types + boss enemies
- Enemy shooting back
- Collision detection (player ↔ enemy, bullet ↔ enemy, enemy bullet ↔ player)
- Increasing difficulty every 30 points (faster enemies & bullets)
- Score & Level display
- Pause functionality (press any non-arrow key)
- Game Over screen with **Replay** and **Exit** buttons
- Background music + shooting & explosion sound effects
- Simple scrolling starfield background

## Game Controls

| Action               | Key(s)                          |
|----------------------|---------------------------------|
| Move                 | ← ↑ → ↓ (Arrow keys)            |
| Shoot                | Automatic                      |
| Pause / Resume       | Any key (except arrows)         |
| Restart after Game Over | Click **Replay** button      |
| Quit                 | Click **Exit** button or close window |

**Win condition**: Reach **Level 10**  
**Lose condition**: Get hit by an enemy or enemy bullet

## Requirements

- **.NET Framework 4.7.2** or higher
- **Windows** (uses Windows Forms + Windows Media Player COM component)
- Visual Studio (2019 / 2022 recommended)


**Images**

-<img width="64" height="64" alt="munition" src="https://github.com/user-attachments/assets/38b64041-0acc-411a-aad8-4ed39d78b4d4" />
-<img width="90" height="90" alt="E1" src="https://github.com/user-attachments/assets/b7565342-5c51-48e9-a72a-a3a3a55c38ac" />
- <img width="80" height="80" alt="Boss1" src="https://github.com/user-attachments/assets/f9ebc7f6-94d4-4874-b0df-9461ffb43524" />
- Player ship image is embedded in resources
- <img width="96" height="96" alt="player" src="https://github.com/user-attachments/assets/966c6487-7c3d-423f-8005-6507925b1075" />


> **Tip**: You can change all file paths in `Form1.cs` to be relative to the executable folder, e.g.:
>
> ```csharp
> Image munition = Image.FromFile("assets/munition.png");
> gameMedia.URL = "sounds/GameSong.mp3";
> ```
