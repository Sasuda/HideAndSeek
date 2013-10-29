using UnityEngine;
using System.Collections;

public class SquareSelect : MonoBehaviour 
{
	RaycastHit hit; // Raycast hit variable used to get the tag of whatever was clicked on with the mouse cursor.
	
	int playerTurn; // Variable that keeps track of which player's turn it currently is.
	int playerWon; // Variable that stores which player won the game.
	
	// When a player wins the winning squares will flash yellow.
	float flashTime = 0.5f; // The time interval for the flashing.
	float timeStamp;
	bool flashOn = false;
	
	public GameObject playerGUI; // Link to the 3D GUI that shows which player's turn it is or which player won.
	
	public GameObject[] playerSquares; // An array that stores the nine gameobjects that the mouse can click on.
	Square[] playerSelected; // An array of the corresponding Square scripts.
	
	int[] winningSquares; // An array of the 3 winning squares.
	
	// Use this for initialization
	void Start () 
	{
		winningSquares = new int[3]; // There can only be 3 winning squares.
		timeStamp = Time.time; // Initialize the timeStamp.
		
		SetupSquares(); 
		InitializeGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Input.GetMouseButtonDown(0) && playerWon == 0)
        {
			// If the raycast hit anything
			if (Physics.Raycast(transform.position, ray.direction, out hit, 1000))
            {
                // If the raycast hit a Square
				if (hit.collider.tag == "Square")
                {
					// If player 1 just selected a free square.
					if (playerTurn == 1 && hit.transform.GetComponent<Square>().GetPlayer() == 0)
					{
						hit.transform.GetComponent<Square>().SetPlayer(1);
						playerTurn = 2;
						playerGUI.GetComponent<TextMesh>().text = "Player 2 Turn";
						playerGUI.GetComponent<TextMesh>().color = Color.green;
					}
					// If player 2 just selected a free square.
					else if (playerTurn == 2 && hit.transform.GetComponent<Square>().GetPlayer() == 0)
					{
						hit.transform.GetComponent<Square>().SetPlayer(2);
						playerTurn = 1;
						playerGUI.GetComponent<TextMesh>().text = "Player 1 Turn";
						playerGUI.GetComponent<TextMesh>().color = Color.red;
					}
				}
				
				CheckForWin();
			}
		}
		else if (playerWon > 0)
		{
			ShowWinningSquares();
		}
	}
	
	void OnGUI()
	{
		if (playerWon > 0)
		{
			if (GUI.Button(new Rect(150, 200, 100, 30), "Play Again"))
			{
				InitializeGame();
			}
		}
	}
	
	void InitializeGame()
	{
		playerWon = 0;
		playerTurn = 1;
		flashOn = false;
		playerGUI.GetComponent<TextMesh>().text = "Player 1 Turn";
		playerGUI.GetComponent<TextMesh>().color = Color.red;
		
		for (int i=0; i < playerSelected.Length; i++)
		{
			playerSelected[i].Reset();
		}
	}
	
	void SetupSquares()
	{
		// Store the Square script for each of the gameobjects.
		playerSelected = new Square[9];
		for (int i=0; i < playerSelected.Length; i++)
		{
			playerSelected[i] = playerSquares[i].GetComponent<Square>();
		}
	}
	
	void CheckForWin()
	{
		#region Check the top row
		if (playerSelected[0].GetPlayer() == 1 && 
			playerSelected[1].GetPlayer() == 1 && 
			playerSelected[2].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 0;
			winningSquares[1] = 1;
			winningSquares[2] = 2;

		}
		else if (playerSelected[0].GetPlayer() == 2 && 
			playerSelected[1].GetPlayer() == 2 && 
			playerSelected[2].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 0;
			winningSquares[1] = 1;
			winningSquares[2] = 2;
		}
		#endregion
		#region Check the middle row
		else if (playerSelected[3].GetPlayer() == 1 && 
			playerSelected[4].GetPlayer() == 1 && 
			playerSelected[5].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 3;
			winningSquares[1] = 4;
			winningSquares[2] = 5;

		}
		else if (playerSelected[3].GetPlayer() == 2 && 
			playerSelected[4].GetPlayer() == 2 && 
			playerSelected[5].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 3;
			winningSquares[1] = 4;
			winningSquares[2] = 5;
		}
		#endregion
		#region Check the bottom row
		else if (playerSelected[6].GetPlayer() == 1 && 
			playerSelected[7].GetPlayer() == 1 && 
			playerSelected[8].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 6;
			winningSquares[1] = 7;
			winningSquares[2] = 8;
		}
		else if (playerSelected[6].GetPlayer() == 2 && 
			playerSelected[7].GetPlayer() == 2 && 
			playerSelected[8].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 6;
			winningSquares[1] = 7;
			winningSquares[2] = 8;
		}
		#endregion
		#region Check the left column
		else if (playerSelected[0].GetPlayer() == 1 && 
			playerSelected[3].GetPlayer() == 1 && 
			playerSelected[6].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 0;
			winningSquares[1] = 3;
			winningSquares[2] = 6;
		}
		else if (playerSelected[0].GetPlayer() == 2 && 
			playerSelected[3].GetPlayer() == 2 && 
			playerSelected[6].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 0;
			winningSquares[1] = 3;
			winningSquares[2] = 6;
		}
		#endregion
		#region Check the middle column
		else if (playerSelected[1].GetPlayer() == 1 && 
			playerSelected[4].GetPlayer() == 1 && 
			playerSelected[7].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 1;
			winningSquares[1] = 4;
			winningSquares[2] = 7;
		}
		else if (playerSelected[1].GetPlayer() == 2 && 
			playerSelected[4].GetPlayer() == 2 && 
			playerSelected[7].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 1;
			winningSquares[1] = 4;
			winningSquares[2] = 7;
		}
		#endregion
		#region Check the left column
		else if (playerSelected[2].GetPlayer() == 1 && 
			playerSelected[5].GetPlayer() == 1 && 
			playerSelected[8].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 2;
			winningSquares[1] = 5;
			winningSquares[2] = 8;
		}
		else if (playerSelected[2].GetPlayer() == 2 && 
			playerSelected[5].GetPlayer() == 2 && 
			playerSelected[8].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 2;
			winningSquares[1] = 5;
			winningSquares[2] = 8;
		}
		#endregion
		#region Check the first diagonal
		else if (playerSelected[0].GetPlayer() == 1 && 
			playerSelected[4].GetPlayer() == 1 && 
			playerSelected[8].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 0;
			winningSquares[1] = 4;
			winningSquares[2] = 8;
		}
		else if (playerSelected[0].GetPlayer() == 2 && 
			playerSelected[4].GetPlayer() == 2 && 
			playerSelected[8].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 0;
			winningSquares[1] = 4;
			winningSquares[2] = 8;
		}
		#endregion
		#region Check the second diagonal
		else if (playerSelected[2].GetPlayer() == 1 && 
			playerSelected[4].GetPlayer() == 1 && 
			playerSelected[6].GetPlayer() == 1)
		{
			playerWon = 1;
			winningSquares[0] = 2;
			winningSquares[1] = 4;
			winningSquares[2] = 6;
		}
		else if (playerSelected[2].GetPlayer() == 2 && 
			playerSelected[4].GetPlayer() == 2 && 
			playerSelected[6].GetPlayer() == 2)
		{
			playerWon = 2;
			winningSquares[0] = 2;
			winningSquares[1] = 4;
			winningSquares[2] = 6;
		}
		#endregion
		#region Check for draw
		// If all the squares are selected with no winner.
		if (playerWon == 0 &&
			playerSelected[0].GetPlayer() > 0 && 
			playerSelected[1].GetPlayer() > 0 &&
			playerSelected[2].GetPlayer() > 0 &&
			playerSelected[3].GetPlayer() > 0 &&
			playerSelected[4].GetPlayer() > 0 &&
			playerSelected[5].GetPlayer() > 0 &&
			playerSelected[6].GetPlayer() > 0 &&
			playerSelected[7].GetPlayer() > 0 &&
			playerSelected[8].GetPlayer() > 0)
		{
			playerWon = 3;
		}
		#endregion
		#region Display winning player
		if (playerWon == 1)
		{
			playerGUI.GetComponent<TextMesh>().text = "Player 1 Wins";
			playerGUI.GetComponent<TextMesh>().color = Color.red;
		}
		else if (playerWon == 2)
		{
			playerGUI.GetComponent<TextMesh>().text = "Player 2 Wins";
			playerGUI.GetComponent<TextMesh>().color = Color.green;
		}
		else if (playerWon == 3)
		{
			playerGUI.GetComponent<TextMesh>().text = "Draw Game";
			playerGUI.GetComponent<TextMesh>().color = Color.cyan;
		}
		#endregion
	}
	
	void ShowWinningSquares()
	{
		if (Time.time > timeStamp + flashTime && playerWon != 3)
		{
			if (flashOn)
			{
				if (playerWon == 1)
				{
					playerSelected[winningSquares[0]].GetComponent<MeshRenderer>().material.color = Color.red;
					playerSelected[winningSquares[1]].GetComponent<MeshRenderer>().material.color = Color.red;
					playerSelected[winningSquares[2]].GetComponent<MeshRenderer>().material.color = Color.red;
					timeStamp = Time.time;
					flashOn = false;
				}
				else
				{
					playerSelected[winningSquares[0]].GetComponent<MeshRenderer>().material.color = Color.green;
					playerSelected[winningSquares[1]].GetComponent<MeshRenderer>().material.color = Color.green;
					playerSelected[winningSquares[2]].GetComponent<MeshRenderer>().material.color = Color.green;
					timeStamp = Time.time;
					flashOn = false;
				}
			}
			else
			{
				playerSelected[winningSquares[0]].GetComponent<MeshRenderer>().material.color = Color.yellow;
				playerSelected[winningSquares[1]].GetComponent<MeshRenderer>().material.color = Color.yellow;
				playerSelected[winningSquares[2]].GetComponent<MeshRenderer>().material.color = Color.yellow;
				timeStamp = Time.time;
				flashOn = true;			
			}
		}
	}
}
