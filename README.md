StrideZero – Endless Survival Game

Desert Runner is a 3D endless runner game developed in Unity. The game is set in a desert environment where the player must avoid obstacles, complete missions, and survive as long as possible.
This project was built as a learning-focused development exercise to understand game architecture, scene flow, object pooling, mission systems, and performance optimization in Unity.

**Game Overview**

The player continuously moves forward in a desert terrain filled with obstacles. The objective is to avoid collisions, survive for longer durations, and complete dynamic missions. The project focuses on clean scene transitions, optimized spawning systems, and structured game logic.

**Core Features**

- Endless forward player movement
- Obstacle system using Object Pooling 
- Desert-themed environment
- Mission system including:
- Distance-based mission
- Coin collection mission
- Safety mission 
- Distance-based scoring system
- Game Over redirection to Main Menu
- Android build support

**Obstacles**

The obstacle system includes:

- Skull
- Drum Barrel
- Cactus

All obstacles are managed using an Object Pooling system to ensure better performance and memory efficiency during continuous spawning.

**Scene Structure**

1. SplashScene (Main Menu + Entry Point)

- Initial logo screen
- Play interaction
- Scene transition to Game Scene

2. Game Scene

- Player Controller
- Obstacle Pooling System
- Mission Manager
- Score System
- Game Over Logic

The main menu logic is integrated within the SplashScene to keep the project structure simple and efficient.

**Game Flow**

**SplashScene → Game Scene → Game Over → Redirect back to SplashScene **

**Engine & Version**

- Engine: Unity 6.3 LTS (6000.3.8f1)
- Platform: Android
- Graphics API: DX11

**Scoring System**

The game currently uses a distance-based scoring system.
Coin-based scoring is not implemented in the final logic.

**Learning Objectives Behind This Project**

- Implementing object pooling for optimized obstacle spawning
- Designing a mission-based system
- Managing scene transitions and UI logic
- Handling Android build configuration
- Structuring a scalable endless runner architecture

**LinkedIn:** https://www.linkedin.com/in/neav-panjwani/
