using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint1 : MonoBehaviour 
{
	Transform target;
	GameObject[] otherWP;
	public List<GameObject> connections;
	
	// Use this for initialization
	void Start () 
	{
		BuildPaths();
	}
	
	void BuildPaths () 
	{
		connections = new List<GameObject>();
		
		otherWP = GameObject.FindGameObjectsWithTag("Waypoint");
		
		foreach(GameObject target in otherWP)
		{
			if (target != null && target.transform != this.transform) 
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