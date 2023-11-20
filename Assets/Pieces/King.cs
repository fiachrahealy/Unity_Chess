using System;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    
    // Set List
    
    public List<Piece> checkPieces;

    public override List<string> AvailableMoves()
    {
        
        // Set List

        List<string> moves = new List<string>();
        
        // Add each Square around King to Moves

        moves.Add(currentSquare.getSquareToThe("N"));
        moves.Add(currentSquare.getSquareToThe("S"));
        moves.Add(currentSquare.getSquareToThe("E"));
        moves.Add(currentSquare.getSquareToThe("W"));
        moves.Add(currentSquare.getSquareToThe("NE"));
        moves.Add(currentSquare.getSquareToThe("NW"));
        moves.Add(currentSquare.getSquareToThe("SE"));
        moves.Add(currentSquare.getSquareToThe("SW"));
        
        // Loop through Own Pieces

        for (int i = 0; i < GameManager.Pieces[player].Count; i++)
        {
            // Loop through Moves
            
            for (int j = 0; j < moves.Count; j++)
            { 
                // If Own Piece is on Square in Moves

                if (GameManager.Pieces[player][i].currentSquare.ID.Equals(moves[j]))
                {
                    
                    // Remove Square from Moves
                    
                    moves.Remove(moves[j]);
                }
            }

        }
        
        // Return Moves

        return moves;

    }
    
    // Check for Check

    public bool CheckForCheck()
    {
        // Empty Check Pieces List
        
        checkPieces.Clear();
        
        // Loop through Opponent's Pieces

        for (int i = 0; i < GameManager.Pieces[(player + 1) % 2].Count; i++)
        {
            // Find Pieces's Available Moves
            
            List<String> availableMoves = GameManager.Pieces[(player + 1) % 2][i].AvailableMoves();
            
            // Loop through Available Moves

            for (int j = 0; j < availableMoves.Count; j++)
            {
                
                // If Move is Not Null

                if (availableMoves[j] != null)
                {
                    
                    // If Move Equals King's Current Square

                    if (availableMoves[j].Equals(currentSquare.ID))
                    {
                        
                        // Add Piece to CheckPieces
                        
                        checkPieces.Add(GameManager.Pieces[(player + 1) % 2][i]);
                        
                        // Break Loop
                        
                        break;
                    }

                }

            }

        }
        
        // If CheckPieces has a Piece

        if (checkPieces.Count > 0)
        {
            
            // Alert for Check
            
            GameManager.Alert("King in Check!");
            
            // Return True
            
            return true;
        }
        
        // Return False
        
        return false;

    }

    // Check for Checkmate
    public bool CheckForCheckmate()
    {
        // If One Piece in CheckPieces
        
        if (checkPieces.Count == 1)
        {
            
            // Loop through Player's Pieces 

            for (int i = 0; i < GameManager.Pieces[player].Count; i++)
            {
                // Check this Piece's Available Moves
                
                List<String> availableMoves = GameManager.Pieces[player][i].AvailableMoves();
                
                // Loop through Available Moves

                for (int j = 0; j < availableMoves.Count; j++)
                {
                    
                    // If Move is Not Null

                    if (availableMoves[j] != null)
                    {
                        
                        // If This Piece can take Piece currently Checking King

                        if (availableMoves[j].Equals(checkPieces[0].currentSquare.ID))
                        {
                            // Return False
                            
                            return false;
                        }

                    }
                }

            }

        }
        
        // Set List

        List<String> KingAvailableMoves = AvailableMoves();
        
        // Loop through King's Available Moves

        for (int k = 0; k < KingAvailableMoves.Count; k++)
        {
            // Loop through Opponent's Pieces

            for (int i = 0; i < GameManager.Pieces[(player + 1) % 2].Count; i++)
            {
                
                // Get Available Moves for Opponent's Piece
                
                List<String> availableMoves = GameManager.Pieces[(player + 1) % 2][i].AvailableMoves();
                
                // Loop through Available Moves

                for (int j = 0; j < availableMoves.Count; j++)
                {
                    
                    // If Opponent's Move is Not Null

                    if (availableMoves[j] != null)
                    {
                        
                        // If King's Move is Not Null

                        if (KingAvailableMoves[k] != null)
                        {
                            
                            // If Move Equals King's Possible Move

                            if (availableMoves[j].Equals(KingAvailableMoves[k]))
                            {
                                // Remove Move from King's Available Moves
                                
                                KingAvailableMoves.Remove(KingAvailableMoves[k]);
                            }

                        }

                    }

                }
            }

        }
        
        // If King has Available Moves

        if (KingAvailableMoves.Count != 0)
        {
            
            // Return False

            return false;
        }
        
        // Return True for Checkmate

        return true;
    }
    
}
    

