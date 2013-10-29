using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint2 : MonoBehaviour 
{
	Transform target;
	GameObject[] otherWayoints;
	public List<GameObject> connections;
	
	int maxConnections = 5;
	
	// Use this for initialization
	void Start () 
	{
		BuildPaths();
	}
	
	void BuildPaths () 
	{
		connections = new List<GameObject>();
		
		otherWayoints = GameObject.FindGameObjectsWithTag("Waypoint");
		
		foreach(GameObject target in otherWayoints)
		{
			if (target != null && target.transform != this.transform && connections.Count < maxConnections) 
			{
	            if (Vector3.Distance(transform.position, target.transform.position) <= 20.0f)
				{
					Debug.DrawLine(transform.position, target.transform.position, Color.red, 200, false);
					connections.Add(target);
				}
	        }
		}
	}
	
	void OnDrawGizmos() 
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
	}
}