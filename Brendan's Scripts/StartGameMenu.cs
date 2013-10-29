using UnityEngine;
using System.Collections;

public class StartGameMenu : MonoBehaviour 
{
	#region Declare variables
	//Types of menus
	bool menuActive = false;
	public bool retryMenu = false;
	public bool startMenuActive;
	//Menu size
    public float menuWidth = 140.0f;
    public float menuHeight = 100.0f;
    public float buttonWidth = 100.0f;
    public float buttonHeight = 60.0f;
    public float horizontalSpacing = 70.0f;
    public float verticalSpacing = 20.0f;
	public float gameStart = 0f;
	//game object management
	//GameObject theAllAITanks;
	//public GameObject[] AITanks;
	//public int tankNumber = 1;
	//string AITankName;
	//Spawner theSpawner;
	//TankController thePlayerTank;
	//public int numberOfAITanks = 1;
	//int currentNumberOfPlayers;
	
	public HeadsUpDisplay theHeadsUpDisplay;
	//int idNumber;
	//public Transform playerPrefab;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		if (theHeadsUpDisplay == null && GameObject.FindGameObjectWithTag("HeadsUpDisplay"))	
		{
			theHeadsUpDisplay = (HeadsUpDisplay)GameObject.FindGameObjectWithTag("HeadsUpDisplay").GetComponent("HeadsUpDisplay") as HeadsUpDisplay;
		}
		else
		{
			Debug.Log("HeadsUpDisplay found");
		} // end if HeadsUpDisplay exists
		//if (theSpawner == null && GameObject.FindGameObjectWithTag("Spawner"))	
		//{
		//	theSpawner = (Spawner)GameObject.FindGameObjectWithTag("Spawner").GetComponent("Spawner");
		//}
		//else
		//{
		//	Debug.Log("Spawner found");
		//} // end if Spawner exists
		//if (thePlayerTank == null && GameObject.FindGameObjectWithTag("Player"))	
		//{
		//	thePlayerTank = (TankController)GameObject.FindGameObjectWithTag("Player").GetComponent("TankController");
		//}
		//else
		//{
		//	Debug.Log("Spawner Not found, check tags");
		//} // end if PlayerTank exists
		//theAllAITanks = GameObject.FindGameObjectWithTag("AllAITanks");
		//if (AITanks == null)	
		//{
		//	AITanks = GameObject.FindGameObjectsWithTag("EnemyTank");
		//}
		//else
		//{
		//	Debug.Log("TankFound");
		//} // end if PlayerTank exists
		//idNumber = 1;
		//numberOfAITanks = 1;
		 /*if(Application.loadedLevelName == "Multiplayer Coin Collection")
		{*/
		menuActive = true;
		retryMenu = false;
		startMenuActive = true;
		/*}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		menuWidth = Screen.width -50f;
		menuHeight = Screen.height -50f;
		 if (Time.time == gameStart)
        {
            menuActive = true;
			theHeadsUpDisplay.headsUpDisplayActive = false;
        }
		//AITanks = GameObject.FindGameObjectsWithTag("EnemyTank");
		
	}
	
	void OnGUI()
    {
		
        if (menuActive)
        {
			//Freeze mouse movement
			GameObject.Find("First Person Controller").GetComponent<MouseLook>().enabled = false;
			GameObject.Find("Flashlight").GetComponent<Flashlight>().enabled = false;
			//Hide the HUD
			theHeadsUpDisplay.headsUpDisplayActive = false;
			
			//Start menu
			if(startMenuActive)
			{
	            GUI.Box(new Rect((Screen.width - menuWidth) / 2.0f, (Screen.height - menuHeight) / 2.0f, menuWidth, menuHeight), "Click start when ready to begin.");
				Time.timeScale = 0.0f;
				 //if (GUI.Button(new Rect((Screen.width - buttonWidth)*(1.0f/3.0f), (Screen.height - menuHeight + verticalSpacing*(19.0f/2.0f)) / 2 + 30, buttonWidth, buttonHeight), "<< Less"))
	            //{
					//if(numberOfAITanks > 1)
					//{
					//	numberOfAITanks = numberOfAITanks -1;
					//}
				//}
				//GUI.Label(new Rect((Screen.width)/2, (Screen.height - menuHeight + verticalSpacing*(25.0f/2.0f)) / 2 + 30, buttonWidth, buttonHeight), numberOfAITanks.ToString());
				//if (GUI.Button(new Rect(((Screen.width - buttonWidth)*(2.0f/3.0f)), (Screen.height - menuHeight + verticalSpacing*(19.0f/2.0f)) / 2 + 30, buttonWidth, buttonHeight), "More >>"))
	            //{
				//	if(numberOfAITanks < 10)
				//	{
				//		numberOfAITanks = numberOfAITanks +1;
				//	}
				//}
	            if (GUI.Button(new Rect((Screen.width - buttonWidth) / 2.0f, (Screen.height - menuHeight + verticalSpacing) / 2 + 30, buttonWidth, buttonHeight), "Start"))
	            {
					//allow mouse movement
					GameObject.Find("First Person Controller").GetComponent<MouseLook>().enabled = true;
					GameObject.Find("Flashlight").GetComponent<Flashlight>().enabled = true;
					
					//hide the menu start time
					Time.timeScale = 1;
	                menuActive = false;
					
					theHeadsUpDisplay.headsUpDisplayActive = true;
					// Randomize starting location
					Vector3 spawnPosition;
					//spawnPosition.x = 20*UnityEngine.Random.value;
					//spawnPosition.y = 4;
					//spawnPosition.z = 20*UnityEngine.Random.value;
					/*
					Network.Instantiate(playerPrefab, spawnPosition, transform.rotation, 0);
					theHeadsUpDisplay.AddPlayer(idNumber);
					idNumber++;
					//GameObject.Find("First Person Controller").BroadcastMessage("ResumeWeapons");
					*/
	            }
			}
			//Retry menu
			else if(retryMenu)
			{
				Time.timeScale = 0.0f;
				// Menu
				GUI.Box(new Rect((Screen.width - menuWidth) / 2.0f, (Screen.height - menuHeight) / 2.0f, menuWidth, menuHeight), "Click Retry when ready to begin.");
				 if (GUI.Button(new Rect((Screen.width - buttonWidth)*(1.0f/3.0f), (Screen.height - menuHeight + verticalSpacing*(19.0f/2.0f)) / 2 + 30, buttonWidth, buttonHeight), "<< Less"))
	            {
					//if(numberOfAITanks > 1)
					//{
					//	numberOfAITanks = numberOfAITanks -1;
					//}
				}
				//GUI.Label(new Rect((Screen.width)/2, (Screen.height - menuHeight + verticalSpacing*(25.0f/2.0f)) / 2 + 30, buttonWidth, buttonHeight), numberOfAITanks.ToString());
				if (GUI.Button(new Rect(((Screen.width - buttonWidth)*(2.0f/3.0f)), (Screen.height - menuHeight + verticalSpacing*(19.0f/2.0f)) / 2 + 30, buttonWidth, buttonHeight), "More >>"))
	            {
					//if(numberOfAITanks < 10)
					//{
					//	numberOfAITanks = numberOfAITanks +1;
					//}
				}
	            if (GUI.Button(new Rect((Screen.width - buttonWidth) / 2.0f, (Screen.height - menuHeight + verticalSpacing) / 2 + 30, buttonWidth, buttonHeight), "Retry"))
	            {
					//allow mouse movement
					GameObject.Find("First Person Controller").GetComponent<MouseLook>().enabled = true;
					GameObject.Find("Flashlight").GetComponent<Flashlight>().enabled = true;
					
					//hide the menu start time
					Time.timeScale = 1;
	                menuActive = false;
					
					theHeadsUpDisplay.headsUpDisplayActive = true;
					//theHeadsUpDisplay.ResetScore();
					// Respwawn the player
					//theSpawner.SpawnPlayerTank();
					//thePlayerTank.health = 3;
					//Tell the AI player is respawning
					//theAllAITanks.BroadcastMessage("FindNewPlayerSpawn");
					//theHeadsUpDisplay.BroadcastMessage("FindNewPlayerSpawn");
					//foreach(GameObject AITank in AITanks)
					//{
					//	AITank.BroadcastMessage("FindNewPlayerSpawn");
					//}
					/*
					Network.Instantiate(playerPrefab, spawnPosition, transform.rotation, 0);
					theHeadsUpDisplay.AddPlayer(idNumber);
					idNumber++;
					//GameObject.Find("First Person Controller").BroadcastMessage("ResumeWeapons");
					*/
	            }
				//GUI.Label(new Rect((Screen.width - 40f)/2, (Screen.height - menuHeight + verticalSpacing*(50.0f/2.0f)) / 2 + 30, buttonWidth, buttonHeight), "Score: " + theHeadsUpDisplay.playerScore);
			}
			else
			{
				GUI.Box(new Rect((Screen.width - menuWidth) / 2.0f, (Screen.height - menuHeight) / 2.0f, menuWidth, menuHeight), "Click unpause when ready to play.");
				Time.timeScale = 0.0f;
				if (GUI.Button(new Rect((Screen.width - buttonWidth) / 2.0f, (Screen.height - menuHeight + verticalSpacing) / 2 + 30, buttonWidth, buttonHeight), "Unpause"))
	            {					
					/*GameObject.Find("First Person Controller").GetComponent<MouseLook>().enabled = true;
					GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = true;
					*/
					theHeadsUpDisplay.headsUpDisplayActive = true;
					Time.timeScale = 1;
	                menuActive = false;
					/*
					Network.Instantiate(playerPrefab, spawnPosition, transform.rotation, 0);
					theHeadsUpDisplay.AddPlayer(idNumber);
					idNumber++;
					//GameObject.Find("First Person Controller").BroadcastMessage("ResumeWeapons");
					*/
	            }
			}// end of if type of menu active
        }// End if menuActive 
    }
	
	public void ActivateMenu(string menuName)
	{
		menuActive = true;
		if(menuName.Equals("Retry Menu"))
		{
			retryMenu = true;
			startMenuActive = false;
			theHeadsUpDisplay.headsUpDisplayActive = false;
		}
		else if(menuName.Equals("Start Menu"))
		{
			retryMenu = false;
			startMenuActive = true;
			theHeadsUpDisplay.headsUpDisplayActive = false;
		}
	}
	/*void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log("Server destroying player");
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
	}
	*/
}
