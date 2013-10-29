using UnityEngine;
using System.Collections;

public class CubeFollower : MonoBehaviour 
{
	#region varible declaration
	public GameObject CubeToFollow;
	public float totalDistance;
	public float distanceToNextPoint;
	 public float speed = 1.0f;
	
	Vector3 startPoint;
	Vector3 endpoint;
	private float startTime;
	Transform targetWaypoint;
	GameObject[] otherWayPoints;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		otherWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
		startPoint = transform.position;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void MoveToWayPoint()
	{
		// delete this next line
		endpoint = startPoint;
		
		distanceToNextPoint = Vector3.Distance(startPoint, endpoint);
		float distCovered = (Time.time - startTime) * speed;
///        float fracJourney = distCovered / journeyLength;
//		 transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
		// targetWaypoint
	}
	
	public void FindNextWaypoint()
	{
		
	}
}
