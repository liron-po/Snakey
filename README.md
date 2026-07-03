# 🐍 Arcade Snake Game (Unity 6)

A high-performance, architecture-first 2D/3D Arcade Snake game built using **Unity 6 (6.1+)**. Optimized for cross-platform mobile deployment (Android & iOS) utilizing clean code principles, decoupling patterns, and native OS scaling.

---

## 🚀 Features & Architecture

* **Unity 6 Architecture:** Built using the modern Unity 6 core standards, entirely bypassing deprecated legacy systems.
* **Decoupled State Management:** Centralized `GameManager` using a strict protected Singleton pattern to handle application states, UI flows, and scene management.
* **Waypoints Queue Movement:** Custom smooth-chain movement algorithm for the snake's tail utilizing a historic waypoint queue to prevent frame-rate rubber-banding and corner clipping.
* **Asynchronous Loading:** Non-blocking scene transitions using `SceneManager.LoadSceneAsync` to ensure zero-freeze frame cycles on mobile devices.
* **JSON-Based Persistence:** Top-10 leaderboard system serialized via a custom wrapper array into JSON and stored locally via `PlayerPrefs`.

---

## 🛠️ Tech Stack & Dependencies

* **Engine:** Unity 6 (6.1.x+)
* **Language:** C# 10 / .NET 8 compatible
* **Input System:** Modern **Unity Input System Package** (supporting touch, gestures, and desktop keyboards).
* **Camera Pipeline:** **Cinemachine 3.x** utilizing decoupled world-space tracking.
* **UI System:** UGUI / Canvas with dynamic **Safe Area Filtering** for modern mobile notches and device cutouts.

---

## 📦 Getting Started & Installation

### Prerequisites
* Unity Hub installed.
* **Unity 6 LTS (or version 6.1+)** with Android/iOS Build Support modules enabled.

### Setup Instructions
1. Clone the repository to your local machine:
   ```bash
   git clone [https://github.com/your-username/snake-unity6.git](https://github.com/your-username/snake-unity6.git)
