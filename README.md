# Card Submit Go

## Folder :
### Asset
Contains all sprite resources and visual assets.

### Script
Contains all project source scripts organized by functional domains.

## Folder Script :

### Card
<img width="428" height="361" alt="image" src="https://github.com/user-attachments/assets/703ea1c1-d33d-4b86-b6f7-c628ea15154c" /><br>
Manages card mechanics, hierarchy, and data for the four card types. Handles card suit assignments, card quantities per suit, and random deck generation logic.<br>
<img width="431" height="147" alt="image" src="https://github.com/user-attachments/assets/6febcaa4-8252-4546-85c1-a9e1ce31dc90" /><br>
Controls the card representation on the table and renders incoming card data onto the UI.<br>

### Manager - SceneController
Handles application exit logic.

### Manager - UIManager
Toggles visibility and state for canvas panels across the application.

### Manager - StateManager
Central hub for global game state management (checking current states and handling state transitions).

### Manager - MainMenuManager
Controls main menu interactions and initial setup.

### Manager - GameOverManager
Manages the end-game display and post-game UI interactions.

### Manager - GamePlayManager
Controls core gameplay execution, managing lifecycle events, player health, and score tracking.

### Manager - Gameplay - UIGamePlayManager
Manager that updates in-game UI components (score display, player health, active card instances, and main game panels).

## Game FLow :
1. The StateManager initializes the game in the Main Menu state. MainMenuManager sets up and renders the main menu UI.
2. Triggering Start Game transitions the state to the Gameplay State. Invokes the OnGameStart event: builds and shuffles the deck, enables the gameplay UI panel, and resets player health, score, and card parameters.
3. Triggers the OnPlayerGuess event, enabling the player UI selection interface to prompt input.
4. Upon player selection, the game evaluates and displays the turn outcome. Invokes the OnConclusion event to process win/loss conditions and disable the selection UI.
5. If the player's guess is incorrect, health points are deducted.The game checks if player health reaches 0; if true, the StateManager transitions directly to the GameOver State.
6. Triggers the OnRoundFinish event.If player health remains > 0, OnRoundStart is invoked to begin the next turn.
7. During the card draw phase, the system verifies remaining deck capacity. If no cards remain in the deck, the game immediately transitions to the GameOver State.
