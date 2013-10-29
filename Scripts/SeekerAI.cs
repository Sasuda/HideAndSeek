using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeekerAI : MonoBehaviour
{
#region variable declaration
	// Specific Objects in scene to import
	public GameObject thePlayer;
	public HeadsUpDisplay theHeadsUpDisplay;
	GameObject[] otherSeekers;
	GameObject seekerThatFoundPlayer;
	public List<SeekerAI> seekers;
	//Waypoint3 targetWaypoint;
	public Waypoint3 startLocation;
	public Waypoint3 destination;
	Waypoint3 currentLocation;
	Waypoint3 targetWaypoint;
	Waypoint3 tempWaypoint;
	GameObject[] otherWayPoints;
	// seeker characteristics
	float lockPosition = 0.0f;
	float minHeight = 1.75f;
	public float speed = 0.25f;
	float step;
	// waypoint characteristcs
	public float totalDistance;
	float distanceToNextWaypoint;
	Vector3 startPoint;
	Vector3 endpoint;
	private float startTime;
	private float journeyTime;
	public bool arrived;
	public bool moving;
	private bool initialTarget;
	// spotting
	public float playerSpottedRange = 20.0f;
	public float playerEscapedRange = 30.0f;
	private bool isPlayerInSpottedRange;
	public bool isPlayerEscaped = false;
	// Start timer characteristics
	float countdown = 3f;
	public float currentTime = 0f; // keeps time when race has started
	#region Seeker states
	public int currentSeekerState;
	public enum SeekerState
	{
		Wandering,
		FoundPlayer,
		AlertedByOtherSeeker
	}
	#endregion
#endregion

	// Use this for initialization
	void Start ()
	{
		#region variable initialization
		#region initialize and find tagged objects in scene
		#region thePlayer
		if (thePlayer == null)	
		{
			if(GameObject.FindWithTag("Player"))
			{
				thePlayer = GameObject.FindWithTag("Player");
			}
			else
			{
				Debug.Log("Player not found, check that gameObjects in scene are tagged.");
			}
		}
		else
		{
			Debug.Log("Player assigned");
		} // end if Player exists
		#endregion
		#region otherWayPoints
		if (otherWayPoints == null)	
		{
			if(GameObject.FindGameObjectsWithTag("Waypoint") != null)
			{
				otherWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
			}
			else
			{
				Debug.Log("Waypoints not found, check that gameObjects in scene are tagged.");
			}
		}
		else
		{
			Debug.Log("Waypoints assigned");
		} // end if otherWayPoints exist
		#endregion
		#region otherSeekers
		if (otherSeekers == null)	
		{
			if(GameObject.FindGameObjectsWithTag("Seeker") != null)
			{
				otherSeekers = GameObject.FindGameObjectsWithTag("Seeker");
			}
			else
			{
				Debug.Log("otherSeekers not found, check that gameObjects in scene are tagged.");
			}
		}
		else
		{
			Debug.Log("otherSeekers assigned");
		} // end if Player exists
		#endregion
//		if (theHeadsUpDisplay == null && GameObject.FindGameObjectWithTag("HeadsUpDisplay"))	
//		{
//			theHeadsUpDisplay = (HeadsUpDisplay)GameObject.FindGameObjectWithTag("HeadsUpDisplay").GetComponent("HeadsUpDisplay") as HeadsUpDisplay;
//		}
//		else
//		{
//			Debug.Log("HeadsUpDisplay found");
//		} // end if HeadsUpDisplay exists
		#endregion
		// initalize base numbers and constants
		lockPosition = 0.0f;
		journeyTime = 1.0f;
		minHeight = 1.75f;
		playerSpottedRange = 20.0f;
		speed = 1.25f;
		currentSeekerState = (int)SeekerState.Wandering;
		initialTarget = false;
		isPlayerEscaped = false;
				
		// Starting position
		startPoint = transform.position;
		transform.position = startLocation.transform.position;
		currentLocation = startLocation;
		startTime = Time.time;
		
		// Start timer before Game starts
		countdown = 3f;
		currentTime = 0f; 
		#endregion
	}

	#region custom methods
	public void ChangeState(int seekerStateToChangeTo)
	{
			currentSeekerState = seekerStateToChangeTo;	
	}

	public void CheckProximity()
	{
		if (thePlayer != null)
		{
			if (Vector3.Distance(this.gameObject.transform.position, thePlayer.transform.position) > playerSpottedRange)
			{
				isPlayerInSpottedRange = false;
				Debug.Log("Player is In Spotted Range: " + isPlayerInSpottedRange);
				//
			}
			if (Vector3.Distance(this.gameObject.transform.position, thePlayer.transform.position) < playerSpottedRange) //if the player is inside the max range then we can see it
			{
				isPlayerInSpottedRange = true;
				Debug.Log("Player is In Spotted Range: " + isPlayerInSpottedRange);
			}// End if(player is in playerSpottedRange)
			
			if (Vector3.Distance(this.gameObject.transform.position, thePlayer.transform.position) < playerEscapedRange)
			{
				isPlayerEscaped = false;
				Debug.Log("Player in playerEscapedRange: " + isPlayerEscaped);
			}
			if (Vector3.Distance(this.gameObject.transform.position, thePlayer.transform.position) > playerEscapedRange) 			
			{
				isPlayerEscaped = true;
				Debug.Log("Player in playerEscapedRange: " + isPlayerEscaped);
			}// End if(player is in playerEscapedRange)

		}
	}
	#endregion
	
	private void MoveToSeeker()
	{
		Debug.Log("Moving to seeker.");
		// Move to seeker that found the player
    	step = speed * Time.deltaTime;
		foreach(GameObject otherSeeker in otherSeekers)
		{
			SeekerAI otherSeekerScript = (SeekerAI)otherSeeker.GetComponent("SeekerAI");
			if(otherSeekerScript.currentSeekerState == (int)SeekerState.FoundPlayer)
			{
				seekerThatFoundPlayer = otherSeeker;
			}
		}
    	transform.position = Vector3.MoveTowards(currentLocation.transform.position, seekerThatFoundPlayer.transform.position, step);
	}
	
	private void ChasePlayer()
	{
		Debug.Log("Chasing Player.");
		// 
		Waypoint3 closestWaypointToPlayer;
		closestWaypointToPlayer = currentLocation;
		foreach(GameObject possibleNearestWaypoint in otherWayPoints)
		{
			if(Vector3.Distance(thePlayer.transform.position, possibleNearestWaypoint.transform.position) < Vector3.Distance(thePlayer.transform.position, closestWaypointToPlayer.transform.position))
			{
				closestWaypointToPlayer = (Waypoint3)possibleNearestWaypoint.GetComponent("Waypoint3");
			}
		} 
		destination = closestWaypointToPlayer;
		
		if(!moving)
		{
			for(int waypointIndex = 0; waypointIndex < currentLocation.connections.Count; waypointIndex++)
			{
				if(!initialTarget)
				{
					targetWaypoint = currentLocation.connections[waypointIndex];
					distanceToNextWaypoint = Vector3.Distance(targetWaypoint.transform.position, destination.transform.position);
					if(Vector3.Distance(currentLocation.transform.position, destination.transform.position) > distanceToNextWaypoint)
					{
						tempWaypoint = targetWaypoint;
						initialTarget = true;
					}
				}
				else
				{
					if(Vector3.Distance(tempWaypoint.transform.position, destination.transform.position) > distanceToNextWaypoint)
					{
						tempWaypoint = targetWaypoint;
						//initTarget = true;
					}
				}// End if(!initTarget)
			}// end of for loop
			moving = true;
			startTime = Time.time;
		}
		else
		{
			
			float fracComplete = speed*(Time.time - startTime)/journeyTime;
			transform.position = Vector3.Slerp(currentLocation.transform.position, tempWaypoint.transform.position , fracComplete);
			if(fracComplete >= 1.0f)
			{
				moving = false;
				arrived = true;
				initialTarget = false;
				currentLocation = tempWaypoint;
			}
		}//End if(!moving)
			
			
	}

	void Wander()
	{
		int randomLocationIndex;
			randomLocationIndex = (int)Random.Range(0,3);
			//if(!arrived)
			//{
				if(!moving)
				{
					for(int waypointIndex = 0; waypointIndex < currentLocation.connections.Count; waypointIndex++)
					{
						if(!initialTarget)
						{
	 						targetWaypoint = currentLocation.connections[randomLocationIndex];
							distanceToNextWaypoint = Vector3.Distance(targetWaypoint.transform.position, destination.transform.position);
							tempWaypoint = targetWaypoint;
							initialTarget = true;
						}
						else
						{
							if(Vector3.Distance(tempWaypoint.transform.position, destination.transform.position) > distanceToNextWaypoint)
							{
								tempWaypoint = targetWaypoint;
								//initTarget = true;
							}
						}// End if(!initTarget)
					}// end of for loop
					moving = true;
					startTime = Time.time;
				}
				else
				{
					// Move to target
		        	//step = speed * Time.deltaTime;
		        	//transform.position = Vector3.MoveTowards(currentLocation.transform.position, tempWaypoint.transform.position, step);
					
					float fracComplete = speed*(Time.time - startTime)/journeyTime;
					transform.position = Vector3.Slerp(currentLocation.transform.position, tempWaypoint.transform.position , fracComplete);
					if(fracComplete >= 1.0f)
					{
						moving = false;
						arrived = true;
						initialTarget = false;
						currentLocation = tempWaypoint;
					}
				}//End if(!moving)
			//}
			//else
			//{
				
			//} //End if(!arrived)
	}
	
	// Update is called once per frame
	void Update ()
	{
		//
	    transform.rotation = Quaternion.Euler(lockPosition, lockPosition, lockPosition);
		transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
		CheckProximity();
		if(isPlayerInSpottedRange == true)
		{
			currentSeekerState = (int)SeekerState.FoundPlayer;
		}
		else if(isPlayerEscaped == true)
		{
			currentSeekerState = (int)SeekerState.Wandering;
		}// end if in range
		
		Debug.Log("PreseekerState firing, Seeker state = " + currentSeekerState);
		
		#region SeekerState is Wandering
		if(currentSeekerState == (int)SeekerState.Wandering)
		{
			Wander();

		}
#endregion
		
#region FoundPlayer
		else if(currentSeekerState == (int)SeekerState.FoundPlayer)
		{
			if(isPlayerInSpottedRange == true)
			{
				if(otherSeekers != null)
				{
					foreach(GameObject otherSeeker in otherSeekers)
					{
						SeekerAI otherSeekerScript = (SeekerAI)otherSeeker.GetComponent("SeekerAI");
						if(otherSeekerScript.currentSeekerState == (int)SeekerState.Wandering)
						{
							otherSeeker.BroadcastMessage("ChangeState", (int)SeekerState.AlertedByOtherSeeker);
							Debug.Log("otherSeeker.BroadcastMessage(\"ChangeState\", (int)SeekerState.AlertedByOtherSeeker);");
						}
					}
				}
				else
				{
					Debug.Log("other seekers not populating array.");
				}
			}
		
			// Chase the player 
			if(isPlayerEscaped == true) //if player escaped return to wandering
			{
				ChangeState((int)SeekerState.Wandering);
				foreach(GameObject otherSeeker in otherSeekers)
				{
					otherSeeker.BroadcastMessage("ChangeState", (int)SeekerState.Wandering);
				}

			}
			else
			{
				ChasePlayer();
				/*	
				// Move to target
		        step = speed * Time.deltaTime;
		        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			*/

			}

		}
#endregion
#region AlertedByOtherSeeker
		else if(currentSeekerState == (int)SeekerState.AlertedByOtherSeeker)
		{
			Debug.Log("if(currentSeekerState == (int)SeekerState.AlertedByOtherSeeker) firing");
			CheckProximity();
		
			//
			if(isPlayerInSpottedRange == true)
			{
				// alert other seekers
				/*foreach(GameObject otherSeeker in otherSeekers)
				{
					SeekerAI otherSeekerScript = (SeekerAI)otherSeeker.GetComponent("SeekerAI");
					if(otherSeekerScript.currentSeekerState == (int)SeekerState.Wandering)
					{
						otherSeeker.BroadcastMessage("ChangeState(((int)SeekerState.AlertedByOtherSeeker))");
					}
				}*/
				// change state to found player
				currentSeekerState = (int)SeekerState.FoundPlayer;
			}
			else if(isPlayerEscaped == true)
			{
				/*foreach(GameObject otherSeeker in otherSeekers)
				{
					otherSeeker.BroadcastMessage("ChangeState(((int)SeekerState.Wandering))");
				}*/
			}
			else
			{
				// get to the seeker that spotted the player 
				MoveToSeeker();
			}
			
		}// End if (currentSeekerState is SeekerState) wandering is first
#endregion

	}
}
/*
 if(!withinRange)
			{
				// Move to target
		        step = speed * Time.deltaTime;
		        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}


*/