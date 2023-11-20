# Unity Chess

Unity Chess is a two-player chess game set in a 3D space setting.

The traditional rules of chess apply to the game. The game is played on a square board made of 64 smaller squares, with eight squares on each side. Each player moves one piece per turn, and the goal of the game is to try and checkmate the king of the opponent. Checkmate is the process of applying a threat to the opposing king that no move can stop; such a move ends the game.

The game is customisable, users have the ability to change the colours of the pieces at the beginning of the game. The game is also beginner-friendly with a built-in move helper mode that shows the player all of the legal moves that can be made with the piece they have selected. This mode can be turned on or off in the game setup.

## Technologies Used

- **Unity:** Unity is used as the project's game engine.

## Prerequisites

### Software Installations

Before running the project, the following software must be installed:

- [Unity](https://unity.com/)

## Source Code Index

As the project's complex file structure contains a large number of files (needed to build game assests), a source code index is provided below:

| **File**                                                | **Description**                                     |
|---------------------------------------------------------|-----------------------------------------------------|
| [**Assets/GameManager.cs**](Assets/GameManager.cs)      | The project's game manager.                         |
| [**Assets/Piece.cs**](Assets/Piece.cs)                  | The piece class, inherited by all game pieces.      |
| [**Assets/Pieces/King.cs**](Assets/Pieces/King.cs)      | The king class.                                     |
| [**Assets/Pieces/Queen.cs**](Assets/Pieces/Queen.cs)    | The queen class.                                    |
| [**Assets/Pieces/Knight.cs**](Assets/Pieces/Knight.cs)  | The knight class.                                   |
| [**Assets/Pieces/Bishop.cs**](Assets/Pieces/Bishop.cs)  | The bishop class.                                   |
| [**Assets/Pieces/Rook.cs**](Assets/Pieces/Rook.cs)      | The rook class.                                     |
| [**Assets/Pieces/Pawn.cs**](Assets/Pieces/Pawn.cs)      | The pawn class.                                     |
| [**Assets/Board/Square.cs**](Assets/Board/Square.cs)    | The sqaure class, used to build the board.          |

## Getting Started

1. Clone the repository:

    ```bash
    git clone https://github.com/fiachrahealy/Unity_Chess.git
    ```

2. Launch the project in Unity:

    ```bash
    /Path/to/Unity/ -projectPath Unity_Chess
    ```

3. Import the main scene:

    Navigate to the **File** menu in the top left hand corner of Unity.<br>
    Select **Open Scene**, open the **MainScene** file.


4. Build and Run the project:

    Navigate to the **File** menu in the top left hand corner of Unity.<br>
    Select **Build and Run**.

## Features

### Setup and Customistation

When players launch the game for the first time, they are welcomed by the landing page. Clicking on the "Play" button takes them to the Player Setup screen, allowing both players to input their names. Subsequently, selecting "Next" leads to the Piece Setup screen, where the players can choose their piece colors. Another click on "Next" guides them to the Game Setup screen, where they can toggle the "Show Possible Moves" helper, which shows the current player all of the legal moves that can be made with the piece they have selected. To commence the game, the players simply click "Play Now," and the board and pieces will be displayed, enabling Player 1 to make their initial move.

Players also have the ability to adjust the board's orientation by tilting it up and down or rotating it clockwise and anticlockwise, utilising the respective arrow keys on their keyboard. Should players desire a rematch with the same participants and piece colours, they can opt for the "Rematch" button. Conversely, the "Quit" button offers a swift return to the landing page.

### Gameplay

When a piece is either selected or hovered over, it will assume a grey hue. If the "Available Moves Helper" mode is enabled, legal moves for the selected piece will be displayed in blue (or red if the square is occupied by the opponent's piece). When the turn shifts, the board undergoes a 180° rotation, rendering Player 1's pieces immovable, thus allowing Player 2 to make their moves. In the event a piece is in imminent danger of being captured, it will change to a red color. The captured pieces are neatly stacked alongside the board.

If a player attempts an illegal move, an error message will promptly appear to notify them of the move's invalidity. In the event a player's king is under threat, a message will surface, alerting the player to their king's precarious situation and limiting their moves to those that protect the king. If there are no feasible moves available to extricate the king from check, a checkmate scenario is declared, and the player responsible for putting the opponent in checkmate emerges as the victor of the game.

## Authors

- [Fiachra Healy](https://www.linkedin.com/in/fiachrahealy/)

## Acknowledgments

The following is a list of 3rd party assests used in the production of Unity Chess:

- [Chess Pieces](https://assetstore.unity.com/packages/3d/props/chess-pieces-board-70092)