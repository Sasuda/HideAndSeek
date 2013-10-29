using UnityEngine;
using System.Collections;

public class HeadsUpDisplay : MonoBehaviour 
{
	#region variables
	public bool headsUpDisplayActive;
	//public TankController thePlayerTank;
	public StartGameMenu theStartMenu;
	// Start timer characteristics
	public float countdown = 3f;
	public float currentTime = 0f; // keeps time when race has started
	public float minutes;
	public float seconds;
	public float fraction;
	
	//int numberOfAITanks;
	//public int playerScore;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		// Start timer before Game starts
		countdown = 3f;
		currentTime = 0f;
		
		//
		currentTime.ToString();

	 	headsUpDisplayActive = true;
		//playerScore = 0;
		
		//if (thePlayerTank == null && GameObject.FindWithTag("Player"))	
		//{
		//	thePlayerTank = (TankController)GameObject.FindWithTag("Player").GetComponent("TankController");
		//}
		if (theStartMenu == null && GameObject.FindGameObjectWithTag("StartMenu"))	
		{
			theStartMenu = GameObject.FindGameObjectWithTag("StartMenu").GetComponent("StartGameMenu") as StartGameMenu;
			//numberOfAITanks = theStartMenu.numberOfAITanks;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//
		if(countdown <= 0.00f)
		{
			currentTime += Time.deltaTime; // start the clock
		}
		if(countdown >= 0.00f)
		{
			countdown -= Time.deltaTime;
		}
			
		/*if two seekers spot the player
		 * {
		 * timer stops
		 * timer float is frecorded int a seperate string
		 * retry screen pops up with time
		 * }
		*/
		
		//numberOfAITanks = theStartMenu.numberOfAITanks;
	}
	
	//
	void OnGUI()
	{
		if (headsUpDisplayActive)
		{
			//string tempPlayerString = theOwner.ToString();
			//int playerNumber = Convert.ToInt32(tempPlayerString);
			//initilizing timer
			minutes = Mathf.Floor(currentTime / 60);
			seconds = Mathf.Floor(currentTime % 60);
			fraction = currentTime * 10;
			fraction = fraction % 10;
			//HUD Box
			GUI.Box(new Rect(10, 30, 150f, 20), "");
			GUI.Label(new Rect(12, 30, 110, 20), "Timer: " + minutes + ":" + seconds + ":" + fraction.ToString("F2"));
			
			//GUI.Label(new Rect(10, 120, 200, 20), "Health = " + thePlayerTank.health);
			//GUI.Label(new Rect(10, 140, 200, 20), "Number Of Tanks = " + numberOfAITanks);
			//GUI.Label(new Rect(10, 160, 200, 20), "Enemies Destroyed = " + playerScore);
			
			// display the countdown to start
		if(countdown >= 0.0f)
    	{ 
			string countdownMessage = countdown.ToString();
			countdownMessage  = countdownMessage.Substring(0,4);
			GUI.Box (new Rect((Screen.width -280) / 2, (Screen.height - 40) / 2, 280, 40), "Game starts in : "+ countdownMessage);
		}
/*
			GUI.Label(new Rect(10, 120, 200, 20), "Player 1 Score = " + playerScores[0]);
			if (numberOfPlayers > 0)
			{
				GUI.Label(new Rect(10, 140, 200, 20), "Player 2 Score = " + playerScores[1]);
			}
			if (numberOfPlayers > 1)
			{
				GUI.Label(new Rect(10, 160, 200, 20), "Player 3 Score = " + playerScores[2]);
			}
			if (numberOfPlayers > 2)
			{
				GUI.Label(new Rect(10, 180, 200, 20), "Player 4 Score = " + playerScores[3]);
			}
			/**/			
		}
	}
	//public void FindNewPlayerSpawn()
	//{
	//	if (thePlayerTank == null && GameObject.FindWithTag("Player"))	
	//	{
	//		thePlayerTank = (TankController)GameObject.FindWithTag("Player").GetComponent("TankController");
	//	}
	//}
	//public void IncreasePlayerScore()
	//{
	//	playerScore += 1;
	//}
	//public void ResetScore()
	//{
	//	playerScore = 0;
	//}
}
/*
public class ScoreBoard : MonoBehaviour 
{
	public NetworkPlayer theOwner;
	public bool scoreGUIActive;
	public int numberOfPlayers;
	public int[] playerID;
	public int[] playerScores;
	
	// Use this for initialization
	void Start () 
	{
		scoreGUIActive = false;
		numberOfPlayers =0;
		playerScores = new int[8];
		//numberOfPlayers = Lobby.
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Activate()
    {
    	scoreGUIActive = true;
		playerScores[0] = 0;
 		playerID[numberOfPlayers] = 1;
    }
	
	public void AddPlayer(int id)
	{
		numberOfPlayers++;
		playerScores[numberOfPlayers] = 0;
		playerID[numberOfPlayers] = id;
	}
	
	public void RemovePlayer()
	{
		numberOfPlayers--;
	}
	
	public void IncrementScore(int id)
	{
		for(int i = 0; i <= numberOfPlayers; i++)
		{
			if(playerID[i] == id)
			{
				playerScores[i] = playerScores[i] + 1;
			}
		}
	}
	
    void OnGUI()
	{
		if (scoreGUIActive)
		{
			string tempPlayerString = theOwner.ToString();
			int playerNumber = Convert.ToInt32(tempPlayerString);
			
			GUI.Box(new Rect(10, 100, 200, 200), "Score Board");
			
			GUI.Label(new Rect(10, 120, 200, 20), "Player 1 Score = " + playerScores[0]);
			if (numberOfPlayers > 0)
			{
				GUI.Label(new Rect(10, 140, 200, 20), "Player 2 Score = " + playerScores[1]);
			}
			if (numberOfPlayers > 1)
			{
				GUI.Label(new Rect(10, 160, 200, 20), "Player 3 Score = " + playerScores[2]);
			}
			if (numberOfPlayers > 2)
			{
				GUI.Label(new Rect(10, 180, 200, 20), "Player 4 Score = " + playerScores[3]);
			}			
		}
	}
}/**/