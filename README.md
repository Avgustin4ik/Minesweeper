# Minesweeper
ECS Minesweeper test task
## How to play
- Click on the cell to open it
- Right click on the cell to mark it as a mine
- Open all cells without mines and mark all mines to win
- Settings - Game Settings Service : ScriptableObject
## main features
- ECS, LeoECS Lite for logic
- MVVV, UniRx for UI
- [UniGame](https://github.com/UnioGame) as a main framework for the project

There is a separate features for logic and ui.
Logic implemented in Field Feature and Game Rules Feature.
UI - in Field View Feature and HUD Feature.
Date stores in Game Settings Service, witch is injected in the world

### Way to improve:
I've put the logic of recursive opening cells in the ECS System. The problem is thats takes a couple frames to open big amount of cells. It's better to do store neighbor data in service in optimized collection, but I liked the way withc cells are opened one by one. That's why I've decided to leave it as is. 

### Screenshots

![Movie_001](Minesweeper.gif)