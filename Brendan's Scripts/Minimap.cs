using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour 
{
	#region Variable Declaration
	//
	public Transform targetMapCentersOn;
	private float minHeight = 42f;
	private float lockPosition = 0f;
	private float rotationX = 90f;
	

	#endregion
	// Use this for initialization
	void Start () 
	{
		#region assign targetMapCentersOn
		if (targetMapCentersOn == null)	
		{
			if(GameObject.FindWithTag("Player"))
			{
				targetMapCentersOn = GameObject.FindWithTag("Player").transform;
			}
			else
			{
				Debug.Log("Player not found, check that gameObjects in scene are tagged.");
			}
		}
		else
		{
			Debug.Log("Player found");
		}  // end if Player exists
		#endregion
		
		//camera settings
		//Normalized View Port Rect X:0.8 Y:0.7 W:0.85 H:0.9
		//this.gameObject.camera.clearFlags = ;//Depth only
		this.gameObject.camera.orthographic = false; // perspective
		this.gameObject.camera.fieldOfView = 40f;
		this.gameObject.camera.nearClipPlane = 0.3f;
		this.gameObject.camera.farClipPlane = 1000f;
		this.gameObject.camera.depth = GameObject.FindGameObjectWithTag("MainCamera").camera.depth +1;
	}
	
	void LateUpdate()
	{
		//follow player
		transform.position = new Vector3(targetMapCentersOn.position.x, minHeight, targetMapCentersOn.position.z);
		
		//lock camera in place
		transform.rotation = Quaternion.Euler(rotationX, lockPosition, lockPosition);

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

