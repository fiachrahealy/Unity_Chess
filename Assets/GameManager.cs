using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    // Instance
    
    private static GameManager instance;
    
    // GameObject Prefabs
    
    public GameObject squarePrefab;
    public GameObject borderPrefab;
    public GameObject kingPrefab;
    public GameObject queenPrefab;
    public GameObject bishopPrefab;
    public GameObject rookPrefab;
    public GameObject knightPrefab;
    public GameObject pawnPrefab;
    
    // Pieces for Selecting Colours

    private GameObject player1Piece;
    private GameObject player2Piece;
    
    // Canvases

    public GameObject startScreen;
    public GameObject playerSetupScreen;
    public GameObject pieceSetupScreen;
    public GameObject gameSetupScreen;
    public GameObject inGameMenu;
    public GameObject endGameScreen;

    // Variables for Determining Moves and Settings
    
    public static int onOff = 1;
    private bool GameInMotion;
    public static bool showMoves = true;
    private string[] playerNames = {"Player1", "Player2"};
    public static int currentMove = 0;
    public static Piece selectedPiece;

    // Colour Scheme

    public static Color[] coloursPieces = {Color.white, Color.black};
    public int currentColourPl1 = 0;
    public int currentColourPl2 = 1;
    public Color[] colours = {Color.white, Color.black, Color.red,  Color.blue, Color.green, Color.yellow, Color.magenta, Color.grey};

    // Lists
    
    public static List<List<Square>> Squares = new List<List<Square>>();
    public static List<List<Piece>> Pieces = new List<List<Piece>>();

    // Buttons

    public Button playBtn;
    public Button next1Btn;
    public Button next2Btn;
    public Button playNowBtn;
    public Button rematchBtn;
    public Button quitBtn;
    public Button onOffBtn;
    public Button rematchEndBtn;
    public Button quitEndBtn;
    public Button up1;
    public Button down1;
    public Button up2;
    public Button down2;
    
    // Texts

    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI otherPlayerText;
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI onOffText;
    public TextMeshProUGUI alertText;
    
    // Input Fields
    
    public TMP_InputField ply1Name;
    public TMP_InputField ply2Name;
    
    // Start Method Called on Game Start
    
    void Start()
    {
        SetDefaults();
        
        // Set Canvases
        
        instance = this;
        instance.startScreen.SetActive(true);
        instance.playerSetupScreen.SetActive(false);
        instance.pieceSetupScreen.SetActive(false);
        instance.gameSetupScreen.SetActive(false);
        instance.inGameMenu.SetActive(false);
        instance.endGameScreen.SetActive(false);
        
        // Set Camera
        
        Camera.main.transform.position = new Vector3(0f, 30f, -30f);
        Camera.main.transform.LookAt(new Vector3(0f, 0f, -4f), new Vector3(0f, 0f, 1f));
        
        // Set Button
        
        playBtn.GetComponent<Button>().onClick.AddListener(PlayerSetup);


    }
    
    // Player Setup

    void PlayerSetup()
    {
        
        // Set Canvases

        instance.startScreen.SetActive(false);
        instance.playerSetupScreen.SetActive(true);
        
        // Set Button
        
        next1Btn.GetComponent<Button>().onClick.AddListener(PieceSetup);

    }
    
    // Piece Setup

    void PieceSetup()
    {
        
        // Set Player Names
        
        playerNames[0] = ply1Name.text;
        playerNames[1] = ply2Name.text;
        
        // Destroy Piece GameObjects
        
        Destroy(player1Piece);
        Destroy(player2Piece);
        
        // Set Canvases
        
        instance.playerSetupScreen.SetActive(false);
        instance.pieceSetupScreen.SetActive(true);
        
        // Instantiate Piece GameObjects
        
        player1Piece = Instantiate(instance.kingPrefab);
        player2Piece = Instantiate(instance.kingPrefab);
        
        // Ensure these Pieces cannot Move

        player1Piece.GetComponent<King>().isMoveable = false;
        player2Piece.GetComponent<King>().isMoveable = false;
        
        // Set their colours
        
        player1Piece.GetComponent<Renderer>().material.SetColor("_Color", coloursPieces[0]);
        player2Piece.GetComponent<Renderer>().material.SetColor("_Color", coloursPieces[1]);
        
        // Set their position
        
        player1Piece.transform.position = new Vector3(-5,-3,0);
        player2Piece.transform.position = new Vector3(5,-3,0);
        
        // Set Text Displaying Names

        player1Text.text = playerNames[0];
        player2Text.text = playerNames[1];
        
        // Set Buttons
        
        next2Btn.GetComponent<Button>().onClick.AddListener(GameSetup);
        up1.GetComponent<Button>().onClick.AddListener(ColourPlayer1PieceUp);
        down1.GetComponent<Button>().onClick.AddListener(ColourPlayer1PieceDown);
        up2.GetComponent<Button>().onClick.AddListener(ColourPlayer2PieceUp);
        down2.GetComponent<Button>().onClick.AddListener(ColourPlayer2PieceDown);
        
    }
    
    // 4 Methods for Determining Piece Colours

    void ColourPlayer1PieceUp()
    {
        player1Piece.GetComponent<Renderer>().material.SetColor("_Color", colours[(currentColourPl1 + 1)%8]);
        coloursPieces[0] = colours[(currentColourPl1 + 1) % 8];
        currentColourPl1++;

    }
    
    void ColourPlayer1PieceDown()
    {

        currentColourPl1 = (currentColourPl1+1)%colours.Length;
        player1Piece.GetComponent<Renderer>().material.SetColor("_Color", colours[(currentColourPl1 - 1)%8]);
        coloursPieces[0] = colours[currentColourPl1];

    }
    
    void ColourPlayer2PieceUp()
    {
        
        player2Piece.GetComponent<Renderer>().material.SetColor("_Color", colours[(currentColourPl2 + 1)%8]);
        coloursPieces[1] = colours[(currentColourPl2 + 1) % 8];
        currentColourPl2++;

    }
    
    void ColourPlayer2PieceDown()
    {
        currentColourPl2 = (currentColourPl1-1)%colours.Length;
        player2Piece.GetComponent<Renderer>().material.SetColor("_Color", colours[(currentColourPl2 - 1)%8]);
        coloursPieces[1] = colours[currentColourPl2];

    }
    
    // Game Setup

    void GameSetup()
    {
        
        // Set Canvases

        instance.pieceSetupScreen.SetActive(false);
        instance.gameSetupScreen.SetActive(true);
        
        // Destroy GameObjects from Piece setup
        
        Destroy(player1Piece);
        Destroy(player2Piece);
        
        // Set Buttons
        
        onOffBtn.GetComponent<Button>().onClick.AddListener(HelpOnOff);
        playNowBtn.GetComponent<Button>().onClick.AddListener(PlayGame);

    }
    
    // Method for enabling Show Piece Moves mode

    void HelpOnOff()
    {
        
        // Set On/Off Text
        
        onOff = (onOff+1)%2;

        if (onOff == 0)
        {
            onOffText.text = "OFF";
            
            // ShowMoves = False
            
            showMoves = false;
        }
        else
        {
            onOffText.text = "ON";
            
            // ShowMoves = True
            
            showMoves =  true;
            
        }

    }
    
    // Play Game

    void PlayGame()
    {
        
        // Set all Variables and Objects to Default
        
        SetDefaults();
        
        // Set Canvases
        
        instance.gameSetupScreen.SetActive(false);
        instance.inGameMenu.SetActive(true);
        
        // Set Buttons

        rematchBtn.GetComponent<Button>().onClick.AddListener(PlayGame);
        quitBtn.GetComponent<Button>().onClick.AddListener(Start);
        
        // Enable Game in Motion
        
        GameInMotion = true;
        
        // Set up Board and Pieces
        
        SetUpBoard();
        SetUpPieces();

    }
    
    // Set up Board

    private void SetUpBoard()
    {
        
        // Instantiate Board borders and set position and colour
        
        // Bottom
        
        GameObject borderB = Instantiate(instance.borderPrefab);
        borderB.transform.position = new Vector3(-3, 0f, -19);
        borderB.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        
        // Top
        
        GameObject borderT = Instantiate(instance.borderPrefab);
        borderT.transform.position = new Vector3(-1, 0f, 15);
        borderT.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        
        // Left
        
        GameObject borderL = Instantiate(instance.borderPrefab);
        borderL.transform.position = new Vector3(15, 0f, -3);
        borderL.transform.Rotate(0.0f, 90.0f, 0.0f);
        borderL.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        
        // Right
        
        GameObject borderR = Instantiate(instance.borderPrefab);
        borderR.transform.position = new Vector3(-19, 0f, -1);
        borderR.transform.Rotate(0.0f, 90.0f, 0.0f);
        borderR.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
            
        // Loop through Rows

        for (int i = 0; i < 8; i++)
        {
            
            // New Row List
            
            List<Square> row = new List<Square>();
            
            // Loop through Columns 

            for (int j = 0; j < 8; j++)
            {
                
                // Instantiate Square

                GameObject square = Instantiate(instance.squarePrefab);
                
                // Set Square Position and Location

                square.transform.position = new Vector3((j-4)*4, 0f, ((i-4)*4));
                square.GetComponent<Square>().location = new Vector3((j-4)*4,0,((i-4)*4));
                
                // Set Square ID

                square.GetComponent<Square>().ID = "ABCDEFGH"[j] + (i+1).ToString();
                
                //Determine if Square is Black or White

                if (j%2 == 0 && i%2 == 0 || j%2 == 1 && i%2 == 1)
                {
                    
                    // Set Colour as Black
                    
                    square.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    square.GetComponent<Square>().colour = Color.black;
                }
                else
                {
                    
                    // Set Colour as White
                    
                    square.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    square.GetComponent<Square>().colour = Color.white;
                    
                }
                
                // Add Square to Row

                row.Add((square.GetComponent<Square>()));

            }
            
            // Add Row to 3D List
            
            Squares.Add(row);
            
        }
        
    }
    
    // Set up Pieces

    private void SetUpPieces()
    {
        
        // Create Lists for Pieces
        
        List<Piece> Player1Pieces = new List<Piece>();
        List<Piece> Player2Pieces = new List<Piece>();


        // Player 1 Pieces Instantiation, Move to Default Position, Add to Piece List
        
        GameObject kingA1 = Instantiate(instance.kingPrefab);
        kingA1.GetComponent<King>().heightFromGround = 1;
        kingA1.GetComponent<King>().Move("D1");
        Player1Pieces.Add(kingA1.GetComponent<King>());

        GameObject queenA1 = Instantiate(instance.queenPrefab);
        queenA1.GetComponent<Queen>().heightFromGround = 1;
        queenA1.GetComponent<Queen>().Move("E1");
        Player1Pieces.Add(queenA1.GetComponent<Queen>());

        GameObject rookA1 = Instantiate(instance.rookPrefab);
        rookA1.GetComponent<Rook>().heightFromGround = 1;
        rookA1.GetComponent<Rook>().Move("A1");
        Player1Pieces.Add(rookA1.GetComponent<Rook>());

        GameObject rookA2 = Instantiate(instance.rookPrefab);
        rookA2.GetComponent<Rook>().heightFromGround = 1;
        rookA2.GetComponent<Rook>().Move("H1");
        Player1Pieces.Add(rookA2.GetComponent<Rook>());

        GameObject bishopA1 = Instantiate(instance.bishopPrefab);
        bishopA1.GetComponent<Bishop>().heightFromGround = 1;
        bishopA1.GetComponent<Bishop>().Move("C1");
        Player1Pieces.Add(bishopA1.GetComponent<Bishop>());

        GameObject bishopA2 = Instantiate(instance.bishopPrefab);
        bishopA2.GetComponent<Bishop>().heightFromGround = 1;
        bishopA2.GetComponent<Bishop>().Move("F1");
        Player1Pieces.Add(bishopA2.GetComponent<Bishop>());

        GameObject knightA1 = Instantiate(instance.knightPrefab);
        knightA1.GetComponent<Knight>().heightFromGround = 1;
        knightA1.GetComponent<Knight>().Move("B1");
        Player1Pieces.Add(knightA1.GetComponent<Knight>());

        GameObject knightA2 = Instantiate(instance.knightPrefab);
        knightA2.GetComponent<Knight>().heightFromGround = 1;
        knightA2.GetComponent<Knight>().Move("G1");
        Player1Pieces.Add(knightA2.GetComponent<Knight>());
        
        GameObject pawnA1 = Instantiate(instance.pawnPrefab);
        pawnA1.GetComponent<Pawn>().heightFromGround = 2;
        pawnA1.GetComponent<Pawn>().Move("A2");
        Player1Pieces.Add(pawnA1.GetComponent<Pawn>());
        
        GameObject pawnA2 = Instantiate(instance.pawnPrefab);
        pawnA2.GetComponent<Pawn>().heightFromGround = 2;
        pawnA2.GetComponent<Pawn>().Move("B2");
        Player1Pieces.Add(pawnA2.GetComponent<Pawn>());
        
        GameObject pawnA3 = Instantiate(instance.pawnPrefab);
        pawnA3.GetComponent<Pawn>().heightFromGround = 2;
        pawnA3.GetComponent<Pawn>().Move("C2");
        Player1Pieces.Add(pawnA3.GetComponent<Pawn>());
        
        GameObject pawnA4 = Instantiate(instance.pawnPrefab);
        pawnA4.GetComponent<Pawn>().heightFromGround = 2;
        pawnA4.GetComponent<Pawn>().Move("D2");
        Player1Pieces.Add(pawnA4.GetComponent<Pawn>());
        
        GameObject pawnA5 = Instantiate(instance.pawnPrefab);
        pawnA5.GetComponent<Pawn>().heightFromGround = 2;
        pawnA5.GetComponent<Pawn>().Move("E2");
        Player1Pieces.Add(pawnA5.GetComponent<Pawn>());
        
        GameObject pawnA6 = Instantiate(instance.pawnPrefab);
        pawnA6.GetComponent<Pawn>().heightFromGround = 2;
        pawnA6.GetComponent<Pawn>().Move("F2");
        Player1Pieces.Add(pawnA6.GetComponent<Pawn>());
        
        GameObject pawnA7 = Instantiate(instance.pawnPrefab);
        pawnA7.GetComponent<Pawn>().heightFromGround = 2;
        pawnA7.GetComponent<Pawn>().Move("G2");
        Player1Pieces.Add(pawnA7.GetComponent<Pawn>());
        
        GameObject pawnA8 = Instantiate(instance.pawnPrefab);
        pawnA8.GetComponent<Pawn>().heightFromGround = 2;
        pawnA8.GetComponent<Pawn>().Move("H2");
        Player1Pieces.Add(pawnA8.GetComponent<Pawn>());
       
        // Player 2 Pieces Instantiation, Move to Default Position, Add to Piece List
        
        GameObject kingB1 = Instantiate(instance.kingPrefab);
        kingB1.GetComponent<King>().heightFromGround = 1;
        kingB1.GetComponent<King>().Move("D8");
        Player2Pieces.Add(kingB1.GetComponent<King>());

        GameObject queenB1 = Instantiate(instance.queenPrefab);
        queenB1.GetComponent<Queen>().heightFromGround = 1;
        queenB1.GetComponent<Queen>().Move("E8");
        Player2Pieces.Add(queenB1.GetComponent<Queen>());

        GameObject rookB1 = Instantiate(instance.rookPrefab);
        rookB1.GetComponent<Rook>().heightFromGround = 1;
        rookB1.GetComponent<Rook>().Move("A8");
        Player2Pieces.Add(rookB1.GetComponent<Rook>());

        GameObject rookB2 = Instantiate(instance.rookPrefab);
        rookB2.GetComponent<Rook>().heightFromGround = 1;
        rookB2.GetComponent<Rook>().Move("H8");
        Player2Pieces.Add(rookB2.GetComponent<Rook>());

        GameObject bishopB1 = Instantiate(instance.bishopPrefab);
        bishopB1.GetComponent<Bishop>().heightFromGround = 1;
        bishopB1.GetComponent<Bishop>().Move("C8");
        
        Player2Pieces.Add(bishopB1.GetComponent<Bishop>());

        GameObject bishopB2 = Instantiate(instance.bishopPrefab);
        bishopB2.GetComponent<Bishop>().heightFromGround = 1;
        bishopB2.GetComponent<Bishop>().Move("F8");
        Player2Pieces.Add(bishopB2.GetComponent<Bishop>());

        GameObject knightB1 = Instantiate(instance.knightPrefab);
        knightB1.GetComponent<Knight>().heightFromGround = 1;
        knightB1.GetComponent<Knight>().Move("B8");
        knightB1.transform.Rotate(0.0f, 0.0f, 180.0f);
        Player2Pieces.Add(knightB1.GetComponent<Knight>());

        GameObject knightB2 = Instantiate(instance.knightPrefab);
        knightB2.GetComponent<Knight>().heightFromGround = 1;
        knightB2.GetComponent<Knight>().Move("G8");
        knightB2.transform.Rotate(0.0f, 0.0f, 180.0f);
        Player2Pieces.Add(knightB2.GetComponent<Knight>());
        
        GameObject pawnB1 = Instantiate(instance.pawnPrefab);
        pawnB1.GetComponent<Pawn>().heightFromGround = 2;
        pawnB1.GetComponent<Pawn>().Move("A7");
        Player2Pieces.Add(pawnB1.GetComponent<Pawn>());
        
        GameObject pawnB2 = Instantiate(instance.pawnPrefab);
        pawnB2.GetComponent<Pawn>().heightFromGround = 2;
        pawnB2.GetComponent<Pawn>().Move("B7");
        Player2Pieces.Add(pawnB2.GetComponent<Pawn>());
        
        GameObject pawnB3 = Instantiate(instance.pawnPrefab);
        pawnB3.GetComponent<Pawn>().heightFromGround = 2;
        pawnB3.GetComponent<Pawn>().Move("C7");
        Player2Pieces.Add(pawnB3.GetComponent<Pawn>());
        
        GameObject pawnB4 = Instantiate(instance.pawnPrefab);
        pawnB4.GetComponent<Pawn>().heightFromGround = 2;
        pawnB4.GetComponent<Pawn>().Move("D7");
        Player2Pieces.Add(pawnB4.GetComponent<Pawn>());
        
        GameObject pawnB5 = Instantiate(instance.pawnPrefab);
        pawnB5.GetComponent<Pawn>().heightFromGround = 2;
        pawnB5.GetComponent<Pawn>().Move("E7");
        Player2Pieces.Add(pawnB5.GetComponent<Pawn>());
        
        GameObject pawnB6 = Instantiate(instance.pawnPrefab);
        pawnB6.GetComponent<Pawn>().heightFromGround = 2;
        pawnB6.GetComponent<Pawn>().Move("F7");
        Player2Pieces.Add(pawnB6.GetComponent<Pawn>());
        
        GameObject pawnB7 = Instantiate(instance.pawnPrefab);
        pawnB7.GetComponent<Pawn>().heightFromGround = 2;
        pawnB7.GetComponent<Pawn>().Move("G7");
        Player2Pieces.Add(pawnB7.GetComponent<Pawn>());
        
        GameObject pawnB8 = Instantiate(instance.pawnPrefab);
        pawnB8.GetComponent<Pawn>().heightFromGround = 2;
        pawnB8.GetComponent<Pawn>().Move("H7");
        Player2Pieces.Add(pawnB8.GetComponent<Pawn>());
        
        // Add Lists to 3D List
        
        Pieces.Add(Player1Pieces);
        Pieces.Add(Player2Pieces);
        
        // Loop Player 1 Pieces

        for (int i = 0; i < 16; i++)
        {
            // Set Colour, Player and isMoveable
            
            Pieces[0][i].GetComponent<Renderer>().material.SetColor("_Color", coloursPieces[0]);
            Pieces[0][i].player = 0;
            Pieces[0][i].isMoveable = true;
        }
        
        // Loop Player 2 Pieces
        
        for (int i = 0; i < 16; i++)
        {
            // Set Colour, Player and isMoveable
            
            Pieces[1][i].GetComponent<Renderer>().material.SetColor("_Color", coloursPieces[1]);
            Pieces[1][i].player = 1;
            Pieces[1][i].isMoveable = false;
        }

    }

    // Get Square
    
    public static Square GetSquare(String square)
    {
        
        // Return the Square Object from Square String

        return Squares[Int32.Parse(square[1].ToString())-1]["ABCDEFGH".IndexOf(square[0])];
        
    }
    
    // Update - Called every frame

    void Update()
    {
        
        // If In Motion
        
        if (GameInMotion)
        {
            
            // Down Arrow

            if (Input.GetKey(KeyCode.DownArrow))
            {
                
                // Point Camera Down
                
                Camera.main.transform.RotateAround(new Vector3(0, 0, 0), Vector3.left, 20 * Time.deltaTime);
            }
            
            // Up Arrow
            
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                
                // Point Camera Up
                
                Camera.main.transform.RotateAround(new Vector3(0, 0, 0), Vector3.left, -20 * Time.deltaTime);
            }
            
            // Left Arrow
            
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                
                // Point Camera Left
                
                Camera.main.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, 20 * Time.deltaTime);
            }
            
            // Right Arrow
            
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                
                // Point Camera Right
                
                Camera.main.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, -20 * Time.deltaTime);
            }
            
            // Set Players Text

            currentPlayerText.text = playerNames[currentMove];
            otherPlayerText.text = "vs "+playerNames[(currentMove+1)%2];
            
        }


    }
    
    // Change Turn

    public static void ChangeTurn()
    {
        
        // Change Move

        currentMove = (currentMove + 1) % 2;
        
        // Loop Through Current Players Pieces

        for (int i = 0; i < Pieces[currentMove].Count; i++)
        {
            
            // Set Pieces to Moveable
            
            Pieces[currentMove][i].isMoveable = true;
            
            // Set Piece Colours
            
            Pieces[0][i].GetComponent<Renderer>().material.SetColor("_Color", coloursPieces[currentMove]);
            Pieces[0][i].isClicked = false;
        }
        
        // Loop through Other Player's Pieces

        for (int i = 0; i < Pieces[(currentMove + 1) % 2].Count; i++)
        {
            
            // Set Pieces to Non-Moveable
            
            Pieces[(currentMove + 1) % 2][i].isMoveable = false;
            
            // Set Piece Colours
            
            Pieces[0][i].GetComponent<Renderer>().material.SetColor("_Color", coloursPieces[(currentMove+1)%2]);
            Pieces[0][i].isClicked = false;
        }
        
        // Set Selected Piece to Null

        selectedPiece = null;

        // Check for Check
        
        if (Pieces[currentMove][0].GetComponent<King>().CheckForCheck())
        {
            
            // If in Check, Check for Checkmate
            
            if (Pieces[currentMove][0].GetComponent<King>().CheckForCheckmate())
            {
                
                // End Game if Checkmate

                instance.EndGame();

            }

        }
        
        // Rotate Board

        Camera.main.transform.RotateAround(new Vector3(-2, 0, -2), Vector3.down, 180);
        
        // Loop to Squares

        for (int k = 0; k < 8; k++)
        {

            for (int j = 0; j < 8; j++)
            {

                // Determine Square Colour

                if (j % 2 == 0 && k % 2 == 0 || j % 2 == 1 && k % 2 == 1)
                {
                    // Set Colours
                    
                    Squares[k][j].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                }
                else
                {
                    // Set Colours
                    
                    Squares[k][j].GetComponent<Renderer>().material.SetColor("_Color", Color.white);

                }



            }

        }
    }
    
    // Set Defaults

    void SetDefaults()
    {
        
        // Destroy all Current GameObjects
        
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("destroyOnLoad"))
        {
            Destroy(i);
        }
        
        // Reset Variables

        onOff = 1;
        GameInMotion = false;
        currentMove = 0;
        selectedPiece = null;
        player1Piece = null;
        player2Piece = null;
        currentColourPl1 = 0;
        currentColourPl2 = 1;
        
        // Clear Lists
    
        Squares.Clear();
        Pieces.Clear();

        
    }
    
    // End game

    public void EndGame()
    {
        
        // Set Defaults
        
        SetDefaults();
        
        // Set Canvases
        
        instance.inGameMenu.SetActive(false);
        instance.endGameScreen.SetActive(true);
        
        // Set Buttons
        
        rematchEndBtn.GetComponent<Button>().onClick.AddListener(PlayGame);
        quitEndBtn.GetComponent<Button>().onClick.AddListener(Start);
        
        // Set Winner Text
        
        winnerText.text = playerNames[currentMove]+" Wins!";
    }
    
    // Alert

    public static void Alert(string alert)
    {
        
        // Set Alert Text

        instance.alertText.text = alert;

    }
}
