using UnityEngine;
using System.Collections;

public class MoverAI : MonoBehaviour 
{
	#region Declare Variables
	public Waypoint3 startLocation;
	public Waypoint3 destination;
	Waypoint3 currentLocation;
	Waypoint3 targetMove;
	Waypoint3 tempWaypoint;
	
	bool arrived = false;
	bool moving = false;
	bool initTarget = false;
		
	float speed = 1.0f;
	float targetDistance;
	float startTime;
	float journeyTime = 1.0f;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		transform.position = startLocation.transform.position;
		currentLocation = startLocation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!arrived)
		{
			if(!moving)
			{
				for(int i = 0; i < currentLocation.connections.Count; i++)
				{
					if(!initTarget)
					{
 						targetMove = currentLocation.connections[i];
						targetDistance = Vector3.Distance(targetMove.transform.position, destination.transform.position);
						if(Vector3.Distance(currentLocation.transform.position, destination.transform.position) > targetDistance)
						{
							tempWaypoint = targetMove;
							initTarget = true;
						}
					}else
					{
						if(Vector3.Distance(tempWaypoint.transform.position, destination.transform.position) > targetDistance)
						{
							tempWaypoint = targetMove;
							//initTarget = true;
						}
					}// End if(!initTarget)
				}// end of for loop
				moving = true;
				startTime = Time.time;
			}
			else
			{
				float fracComplete = (Time.time - startTime)/journeyTime;
				transform.position = Vector3.Slerp(currentLocation.transform.position, tempWaypoint.transform.position , fracComplete);
				if(fracComplete >= 1.0f)
				{
					moving = false;
					//arrived = false;
					currentLocation = tempWaypoint;
				}
			}//End if(!moving)
		}
		else
		{
			
		} //End if(!arrived)
	
	}
}
