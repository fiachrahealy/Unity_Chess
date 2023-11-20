using System;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    // Set Variables
    
    public string ID;
    public Vector3 location;
    public Piece currentPiece;
    public Color colour;

    // Get Square to Position 
    public string getSquareToThe(string cardinalDirection)
    {
        
        // Set Alphabet
        
        String alphabet = "ABCDEFGH";
        
        // If North
        
        if (cardinalDirection.Equals("N"))
        {
            if (Int32.Parse(ID[1].ToString()) + 1 <= 8)
            {
                return ID[0].ToString() + (Int32.Parse(ID[1].ToString()) + 1);
            }

            return null;

        }
        
        // If South
        
        if (cardinalDirection.Equals("S"))
        {
            if (Int32.Parse(ID[1].ToString()) - 1 > 0)
            {
                return ID[0].ToString() + (Int32.Parse(ID[1].ToString()) - 1);
            }

            return null;
        }
        
        // If East
        
        if (cardinalDirection.Equals("E"))
        {

            if (alphabet.IndexOf(ID[0]) + 2 <= alphabet.Length)
            {
                return alphabet[alphabet.IndexOf(ID[0]) + 1] + ID[1].ToString();
            }

            return null;


        }
        
        // If West
        
        if (cardinalDirection.Equals("W"))
        {
            if (alphabet.IndexOf(ID[0]) - 1 >= 0)
            {
                return alphabet[alphabet.IndexOf(ID[0]) - 1] + ID[1].ToString();
            }

            return null;
        }
        
        // If North East
        
        if (cardinalDirection.Equals("NE"))
        {
            
            if (Int32.Parse(ID[1].ToString()) + 1 <= 8 && alphabet.IndexOf(ID[0]) + 2 <= alphabet.Length)
            {
                return alphabet[alphabet.IndexOf(ID[0]) + 1].ToString()  + (Int32.Parse(ID[1].ToString()) + 1);
            }

            return null;
            
        }
        
        // If North West
        
        if (cardinalDirection.Equals("NW"))
        {
            
            if (Int32.Parse(ID[1].ToString()) + 1 <= 8 && alphabet.IndexOf(ID[0]) - 1 >= 0)
            {
                return alphabet[alphabet.IndexOf(ID[0]) - 1].ToString()  + (Int32.Parse(ID[1].ToString()) + 1);
            }

            return null;
            
        }
        
        // If South East
        
        if (cardinalDirection.Equals("SE"))
        {
            
            if (Int32.Parse(ID[1].ToString()) - 1 > 0 && alphabet.IndexOf(ID[0]) + 2 <= alphabet.Length)
            {
                return alphabet[alphabet.IndexOf(ID[0]) + 1].ToString()  + (Int32.Parse(ID[1].ToString()) - 1);
            }

            return null;
            
        }
        
        // If South West
        
        if (cardinalDirection.Equals("SW"))
        {
            
            if (Int32.Parse(ID[1].ToString()) - 1 > 0 && alphabet.IndexOf(ID[0]) - 1 >= 0)
            {
                return alphabet[alphabet.IndexOf(ID[0]) - 1].ToString()  + (Int32.Parse(ID[1].ToString()) - 1);
            }

            return null;
            
        }

        return null;
    }
    
    // On Hover

    void OnMouseOver()
    {
        // If Selected Piece, Change Colour to Grey
        
        if (GameManager.selectedPiece != null && currentPiece == null)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        }
    }
    
    // On Stop Hover

    void OnMouseExit()
    {
        
        // If Selected Piece
        
        if (GameManager.selectedPiece != null)
        {
            
            // Set to Default Colour
            
            GetComponent<Renderer>().material.SetColor("_Color",colour);


            // Show Available Moves Again
            
            if (GameManager.showMoves)
            {

                GameManager.selectedPiece.ShowAvailableMoves();

            }
        }
    }
    
    // On Click
    
    void OnMouseDown()
    {

        // If Selected Piece
        
        if (GameManager.selectedPiece != null)
        {
            
            // Set List
            
            List<string> availableMoves = GameManager.selectedPiece.AvailableMoves();
            
            // If In Available Moves

            if (availableMoves.Contains(ID))
            {
                
                // Move Piece and Change Turn
                
                GameManager.Alert("");

                GameManager.selectedPiece.Move(ID);

                GameManager.ChangeTurn();


            }
            else
            {
                // Alert Invalid Move
                
                GameManager.Alert("Invalid Move!");
            }

        }



    }


}
