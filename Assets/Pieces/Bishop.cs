using System.Collections.Generic;

public class Bishop : Piece
{
    // Available Moves
    public override List<string> AvailableMoves()
    {

        // Set List and Directions
        
        List<string> moves = new List<string>();
        string[] directions = {"NW", "NE", "SW", "SE"};
        
        // Loop for Directions

        for (int i = 0; i < directions.Length; i++)
        {
            
            // Temp Square
            
            Square squareTemp = currentSquare;
            
            // While There is Still a Square to the Direction
            
            while(squareTemp.getSquareToThe(directions[i]) != null){
                
                // If This Square has a Piece

                if (GameManager.GetSquare(squareTemp.getSquareToThe(directions[i])).currentPiece != null)
                {
                    
                    // If The Piece is The Opponents
                    
                    if (GameManager.GetSquare(squareTemp.getSquareToThe(directions[i])).currentPiece.player != player)
                    {
                        
                        // Add the Piece to Moves

                        moves.Add(squareTemp.getSquareToThe(directions[i]));

                    }
                    
                    // Break the Loop

                    break;

                }
                
                // Add Square to Moves
                
                moves.Add(squareTemp.getSquareToThe(directions[i]));
                
                // Set Temp Square to Next Square in Direction

                squareTemp = GameManager.GetSquare(squareTemp.getSquareToThe(directions[i]));
            
            }
        }
        
        // Return Moves

        return moves;

    }
}
