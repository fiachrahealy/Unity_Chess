using System;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    
    // Set Variables
    
    public Square currentSquare;
    private bool isTaken = false;
    public bool isMoveable;
    public bool isClicked;
    public int heightFromGround;
    public int player;
    public static int[] zNumLayer1 = {12, -16};
    public static int[] zNumLayer2 = {12, -16};
    
    // Move Method

    public void Move(String squareString)
    {
        // Locate Square
        
        Square square = GameManager.GetSquare(squareString);
        
        // Transform Position
        
        transform.position = new Vector3(square.location[0],heightFromGround,square.location[2]);
        
        // If CurrentSquare is not Null

        if (currentSquare != null)
        {
            
            // Set Current Piece to Null
            
            currentSquare.currentPiece = null; 
            
        }
        
        // Set Square and Piece
        
        currentSquare = square;
        square.currentPiece = this;

    }
    
    // Take Piece

    public void TakePiece()
    {
        
        // Remove Piece from List

        GameManager.Pieces[player].Remove(this);
        
        // If Player 1
        
        if (player == 0)
        {
            // Put Piece in 1st Row
            
            if (GameManager.Pieces[0].Count > 8)
            {
                transform.position = new Vector3(20, heightFromGround, zNumLayer1[0]);

                zNumLayer1[0] = zNumLayer1[0] - 4;

            }
            
            // Put Piece in 2nd Row
            
            else
            {
                
                transform.position = new Vector3(24, heightFromGround, zNumLayer2[0]);

                zNumLayer2[0] = zNumLayer2[0] - 4;
            }

        }
        
        // If Player 2
        
        else
        {
            // Put Piece in 1st Row
            
            if (GameManager.Pieces[1].Count > 8)
            {
                transform.position = new Vector3(-24, heightFromGround, zNumLayer1[1]);

                zNumLayer1[1] = zNumLayer1[1] + 4;

            }
            
            // Put Piece in 2nd Row
            
            else
            {
                
                transform.position = new Vector3(-28, heightFromGround, zNumLayer2[1]);

                zNumLayer2[1] = zNumLayer2[1] + 4;
            }
            
        }
        
        

    }

    // Available Moves

    public virtual List<string> AvailableMoves()
    {

        // Return Null List
        
        List<string> moves = new List<string>();

        return moves;


    }
    
    // Show Available Moves

    public void ShowAvailableMoves()
    {
        
        // Loop through Squares
        
        for (int k = 0; k < 8; k++)
        {

            for (int j = 0; j < 8; j++)
            {

                // Set Default Colours

                if (j % 2 == 0 && k % 2 == 0 || j % 2 == 1 && k % 2 == 1)
                {
                    GameManager.Squares[k][j].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                }
                else
                {
                    GameManager.Squares[k][j].GetComponent<Renderer>().material.SetColor("_Color", Color.white);

                }



            }

        }

        // Set List

        List<string> availableMoves = AvailableMoves();
        
        // Loop through Avilable Moves

            for (int i = 0; i < availableMoves.Count; i++)
            {
                
                // If Move is Not Null

                if (availableMoves[i] != null)
                {
                    
                    // Set Colour to Blue

                    GameManager.GetSquare(availableMoves[i]).GetComponent<Renderer>().material
                        .SetColor("_Color", Color.blue);


                    // If has a Current Piece, set Colour to Red
                    
                    if (GameManager.GetSquare(availableMoves[i]).currentPiece != null)
                    {
                        if (GameManager.GetSquare(availableMoves[i]).currentPiece.player == (player + 1) % 2)
                        {

                            GameManager.GetSquare(availableMoves[i]).GetComponent<Renderer>().material
                                .SetColor("_Color", Color.red);

                        }

                    }

                }
            }

    }
    
    // Hover On

    void OnMouseOver()
    {
        // If Moveable
        
        if (isMoveable)
        {
            
            // Change to Grey
            
            GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        }
        else
        {
            // If Selected Piece is Selected

            if (GameManager.selectedPiece != null)
            {
                // Set Colour to Red

                GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                currentSquare.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);

            }
        }
    }
    
    // On Stop Hover 

    void OnMouseExit()
    {
        // If Not Chlciked
        
        if (!isClicked)
        {
            // Change Colour to Default
            
            GetComponent<Renderer>().material.SetColor("_Color", GameManager.coloursPieces[player]);
            currentSquare.GetComponent<Renderer>().material.SetColor("_Color", currentSquare.colour);
        }
    }
    
    // On Click
    
    void OnMouseDown()
    {
        
        // If Is Moveable

        if (isMoveable)
        {
            
            // Loop through Pieces

            for (int i = 0; i < GameManager.Pieces[player].Count; i++)
            {
                // Set Defaults for Pieces
                
                GameManager.Pieces[player][i].GetComponent<Renderer>().material.SetColor("_Color", GameManager.coloursPieces[player]);
                GameManager.Pieces[player][i].isClicked = false;
            }
            
            // Set Clicked Piece

            isClicked = true;
            GameManager.selectedPiece = this;
            
            // Set Colour to Red

            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            
            // If Show Moves turned On
            
            if (GameManager.showMoves)
            {
                // Show Moves
                
                ShowAvailableMoves();
            }

        }
        else
        {
            
            // If No Selected Piece

            if (GameManager.selectedPiece != null)
            {
                
                // Set List
                
                List<string> availableMoves = GameManager.selectedPiece.AvailableMoves();
                
                // If Available Moves contains Current square

                if (availableMoves.Contains(currentSquare.ID))
                {
                    
                    // Take Piece
                    
                    TakePiece();
                    
                    // Set Default Alert
                    
                    GameManager.Alert("");
                    
                    // Move Selected Piece
                    
                    GameManager.selectedPiece.Move(currentSquare.ID);
                    
                    // Change Turn
                    
                    GameManager.ChangeTurn();
                    
                }
                else
                {
                    
                    // Alert Move is Invalid

                    GameManager.Alert("Invalid Move!");

                }

            }
        }
    }
}
