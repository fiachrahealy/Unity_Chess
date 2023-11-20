using System.Collections.Generic;

public class Pawn : Piece
{

    public override List<string> AvailableMoves()
    {
        
        // Set List and hasMoved Bool

        List<string> moves = new List<string>();
        bool hasNotMoved = currentSquare.ID[1].Equals('2') && player == 0 || currentSquare.ID[1].Equals('7') && player == 1;

        // If Player is Player 1
        
        if (player == 0)
        {
            
            // Add Square to North

            moves.Add(currentSquare.getSquareToThe("N"));
            
            // If Not Moved Yet

            if (hasNotMoved)
            {
                // Add Piece to North
                
                moves.Add(GameManager.GetSquare(moves[0]).getSquareToThe("N"));
            }

        }
        
        // If Player 2
        
        else
        {
            
            // Add Piece to South

            moves.Add(currentSquare.getSquareToThe("S"));
            
            // If Piece has Not Moved

            if (hasNotMoved)
            {
                // Add Move to South
                
                moves.Add(GameManager.GetSquare(moves[0]).getSquareToThe("S"));
            }

        }
        
        // Loop through Pieces

        for (int i = 0; i < GameManager.Pieces[player].Count; i++)
        {
            
            // Loop through Moves
            
            for (int j = 0; j < moves.Count; j++)
            {

                if (GameManager.Pieces[player][i].currentSquare.ID.Equals(moves[j]))
                {
                    
                    // If Piece Blocking Clear Moves

                    if (j == 0)
                    {
                        moves.Clear();
                    }
                    else
                    {
                        moves.Remove(moves[j]);
                    }
                }

            }

        }
        
        // Loop through Opponent's Pieces

        for (int i = 0; i < GameManager.Pieces[(player + 1) % 2].Count; i++)
        {
            // Loop through Moves
            
            for (int j = 0; j < moves.Count; j++)
            {
                
                // if Piece Blocking

                if (GameManager.Pieces[(player + 1) % 2][i].currentSquare.ID.Equals(moves[j]))
                {
                    
                    // Clear Available Moves

                    if (j == 0)
                    {
                        moves.Clear();
                    }
                    else
                    {
                        moves.Remove(moves[j]);
                    }
                }

            }

        }
        
        // If Player 1

        if (player == 0)
        {
            // Check if Opponent is in NE position
            
            if (currentSquare.getSquareToThe("NE") != null)
            {

                if (GameManager.GetSquare(currentSquare.getSquareToThe("NE")).currentPiece != null)
                {

                    if (GameManager.GetSquare(currentSquare.getSquareToThe("NE")).currentPiece.player ==
                        (player + 1) % 2)
                    {

                        moves.Add(currentSquare.getSquareToThe("NE"));

                    }


                }
            }
            
            // Check if Opponent in NW

            if (currentSquare.getSquareToThe("NW") != null)
            {

                if (GameManager.GetSquare(currentSquare.getSquareToThe("NW")).currentPiece != null)
                {

                    if (GameManager.GetSquare(currentSquare.getSquareToThe("NW")).currentPiece.player ==
                        (player + 1) % 2)
                    {

                        moves.Add(currentSquare.getSquareToThe("NW"));

                    }


                }

            }

        }
        
        // If Player 2
        
        else
        {
            
            // Check Opponent in SE

            if (currentSquare.getSquareToThe("SE") != null)
            {

                if (GameManager.GetSquare(currentSquare.getSquareToThe("SE")).currentPiece != null)
                {

                    if (GameManager.GetSquare(currentSquare.getSquareToThe("SE")).currentPiece.player ==
                        (player + 1) % 2)
                    {

                        moves.Add(currentSquare.getSquareToThe("SE"));

                    }


                }

            }


        }
        
        // Check Opponent in SW

        if (currentSquare.getSquareToThe("SW") != null)
        {

            if (GameManager.GetSquare(currentSquare.getSquareToThe("SW")).currentPiece != null)
            {

                if (GameManager.GetSquare(currentSquare.getSquareToThe("SW")).currentPiece.player == (player + 1) % 2)
                {

                    moves.Add(currentSquare.getSquareToThe("SW"));

                }


            }

        }

        // Return Moves

        return moves;


}

}


