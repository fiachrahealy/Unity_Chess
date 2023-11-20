using System.Collections.Generic;

public class Knight : Piece
{

    // Available Moves

    public override List<string> AvailableMoves()
    {
        
        // Set List and Directions

        List<string> moves = new List<string>();
        string[,] directions = {{"E","E","N"}, {"E","E","S"}, {"N","N","E"}, {"N","N","W"}, {"S","S","W"}, {"S","S","E"}, {"W","W","N"}, {"W","W","S"}};

        // Loop for Directions

        for (int i = 0; i < 8; i++)
        {

            // If Square in First Direction

            if (currentSquare.getSquareToThe(directions[i,0]) != null)
            {

                // If Square in Second Direction

                if (GameManager.GetSquare(currentSquare.getSquareToThe(directions[i, 0]))
                    .getSquareToThe(directions[i, 1]) != null)
                {

                    // If Square in Third Direction

                    if (GameManager
                        .GetSquare(GameManager.GetSquare(currentSquare.getSquareToThe(directions[i, 0])).getSquareToThe(directions[i, 1]))
                        .getSquareToThe(directions[i, 2]) != null)
                    {

                        // Add Square to Moves
                        
                        moves.Add(GameManager
                            .GetSquare(GameManager.GetSquare(currentSquare.getSquareToThe(directions[i, 0])).getSquareToThe(directions[i, 1]))
                            .getSquareToThe(directions[i, 2]));
                    
                    }
                    
                }
                
            }
            
        }

        // Loop for Pieces

        for (int i = 0; i < GameManager.Pieces[player].Count; i++)
        {

            // Loop for Moves

            for (int j = 0; j < moves.Count; j++)
            {

                // If Piece in Square in Moves
          
                if(GameManager.Pieces[player][i].currentSquare.ID.Equals(moves[j]))
                {

                    // Remove Move

                    moves.Remove(moves[j]);
                }
            }

        }

        // Return Moves

        return moves;

    }
}
