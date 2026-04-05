**StrideZero – Endless Survival Game**

StrideZero is a 3D endless runner game built using Unity. The game is set in a desert environment where the player avoids obstacles, completes missions, and survives as long as possible. This project was created as a learning-focused build to understand how real game systems work including movement logic, object pooling, missions, and performance optimization.

**Game Overview**

In this game, the player does not actually move forward. Instead, the environment and obstacles move towards the player to create the illusion of motion.
**This approach:**

- Keeps movement smooth
- Simplifies control logic
- Makes it easier to build an infinite game loop

The goal is to survive longer, avoid obstacles, and complete missions during the run.

**Core Features** 

- Endless runner gameplay
- Environment-based movement system
- Obstacle system using object pooling
- Desert-themed environment
- Mission system:
    - Distance-based
    - Coin collection
    - Survival-based
- Distance-based scoring system
- Game Over -> returns to main menu
- Android build support

**Obstacles**
**The game includes:**
- Skull
- Drum Barrel
- Cactus
All obstacles are managed using object pooling, meaning they are reused instead of created/destroyed repeatedly. This improves performance and keeps gameplay smooth.

**Scene Structure** 

1. SplashScene (Main Menu)
- Logo screen
- Play button
- Entry point of the game

2. Game Scene
- Player controller
- Obstacle spawning system
- Mission manager
- Score tracking
- Game over handling

**Game Flow**
SplashScene -> Game Scene -> Game Over -> SplashScene

**Scoring System**
The game uses a distance-based scoring system, where the score increases as long as the player survives.

**Tech Stack**

- Engine: Unity 6 LTS (6000.3.x)
- Language: C#
- Platform: Android

**What This Project Focuses On**

- Building modular game systems
- Using object pooling for performance
- Designing a mission-based gameplay loop
- Managing scene transitions and UI
- Creating a scalable endless runner structure

**Setup Instructions**

- Run the Project
    - Install Unity Hub
    - Install Unity version 6000.3.x (LTS)
    - Clone or download this repository
    - Open the project using Unity Hub
    - Open the SplashScene
    - Click Play

- Build for Android
    - Install Android Build Support in Unity
    - Go to File -> Build Settings
    - Select Android
    - Click Switch Platform
    - Connect your device or build APK
