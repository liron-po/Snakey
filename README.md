# 🐍 Snakey

A Unity 6 snake game project with waypoint-based body movement, PlayerPrefs high scores, and menu-driven gameplay.

---

## 🚀 What’s in this repo

* **Unity 6 project:** built with `ProjectVersion: 6000.1.7f1`.
* **Snake gameplay:** head movement and tail follow using waypoint history.
* **Input System support:** keyboard controls via Unity Input System and legacy keyboard polling.
* **Score persistence:** high scores saved as JSON in `PlayerPrefs` and shown on the main menu.
* **Main menu flow:** play, show high scores, return to menu, and quit game.
* **Food spawning:** randomized safe spawning that avoids the snake body.

---

## 🛠️ Tech stack

* **Unity Engine:** Unity 6.0.1f1 project.
* **Language:** C# 10 / .NET 8 compatible.
* **Packages:**
  * `com.unity.inputsystem` 1.14.0
  * `com.unity.ugui` 2.0.0
  * `com.unity.timeline` 1.8.7
  * `com.unity.cinemachine` 3.1.7
  * `com.unity.test-framework` 1.5.1
  * `com.unity.visualscripting` 1.9.7
* **UI:** TextMesh Pro + UGUI.

---

## 🧩 Core scripts

* `Assets/Snakey/Scripts/GameManager.cs` — score tracking and UI updates.
* `Assets/Snakey/Scripts/SnakeMovement.cs` — directional movement, growth, and collision handling.
* `Assets/Snakey/Scripts/SnakeBody.cs` — waypoint-based tail following and segment growth.
* `Assets/Snakey/Scripts/SnakeInput.cs` — keyboard input mapping and Input System callbacks.
* `Assets/Snakey/Scripts/FoodSpawner.cs` — randomized food spawning.
* `Assets/Snakey/Scripts/MainManuController.cs` — main menu, high score display, and scene loading.
* `Assets/Snakey/Scripts/SaveSystem.cs` — save/load high score persistence.
* `Assets/Snakey/Scripts/SnakeDeathHandler.cs` — game over handling and scene return.

---

## 🚀 Getting started

### Prerequisites
* Unity Hub installed.
* Unity 6.0.1f1 or later.

### Open the project
1. Open Unity Hub.
2. Add the `Snakey` folder as a project.
3. Open the project in Unity.

### Run
* Open the main menu scene and press Play in the editor.

---

## 📁 Notes

* This project currently uses a simple snake movement mechanic on a 3D plane.
* High scores are stored locally via `PlayerPrefs` under the key `SnakeHighScores_V2`.
* The repo includes TextMesh Pro assets and sample scripts.
