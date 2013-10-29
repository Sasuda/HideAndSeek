using UnityEngine;
using System.Collections;

public class AITankController : MonoBehaviour 
{
	#region Variables
	//constants
	//tagged variables to find
	public GameObject playerTank;
	public HeadsUpDisplay theHeadsUpDisplay;
	public Transform target;
	//dynamic variables
    public float speed;
	public float rotationSpeed;
	public int health = 1;
	
	//
	public float distanceToPlayerToStop;
	Vector3 thisTankPosition;
	bool withinRange;
	Vector3 targetDir;
	Vector3 newDir;
	float rotationStep;
	float step;
	#endregion
	
	void Start()
	{
		//Initialize variables
		#region initialize and find tagged objects in scene
		if (target == null && GameObject.FindWithTag("Player"))	
		{
			target = GameObject.FindWithTag("Player").transform;
		}
		else
		{
			Debug.Log("target: Player found");
		} // end if target exists
		if (playerTank == null && GameObject.FindWithTag("Player"))	
		{
			playerTank = GameObject.FindWithTag("Player");
		}
		else
		{
			Debug.Log("Player tank found");
		} // end if Player exists
		if (theHeadsUpDisplay == null && GameObject.FindGameObjectWithTag("HeadsUpDisplay"))	
		{
			theHeadsUpDisplay = (HeadsUpDisplay)GameObject.FindGameObjectWithTag("HeadsUpDisplay").GetComponent("HeadsUpDisplay") as HeadsUpDisplay;
		}
		else
		{
			Debug.Log("HeadsUpDisplay found");
		} // end if HeadsUpDisplay exists
		#endregion
		health = 1;
		distanceToPlayerToStop = 8.0f;
		withinRange = false;

	}
	
    void Update() 
	{
		thisTankPosition = this.transform.position;
		CheckProximity();
		if(target != null)
		{
			// Rotate Toward target
			targetDir = target.position - transform.position;
	        rotationStep = rotationSpeed * Time.deltaTime;
	        newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0.0F);
	        Debug.DrawRay(transform.position, newDir, Color.red);
	        transform.rotation = Quaternion.LookRotation(newDir);
			
			if(!withinRange)
			{
				// Move to target
		        step = speed * Time.deltaTime;
		        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}
		}
	}
	
	void FixedUpdate()
	{
		//ApplyDamage();
		if(health == 0)
		{
			theHeadsUpDisplay.IncreasePlayerScore();
			Destroy(this.gameObject);
		}
	}
	
	public void ApplyDamage()
	{
		health = health-1;
	}
	void CheckProximity()	
	{
		if (playerTank != null)
		{
			if (Vector3.Distance(thisTankPosition, playerTank.transform.position) > distanceToPlayerToStop)
			{
				withinRange= false;
			}
			if (Vector3.Distance(thisTankPosition, playerTank.transform.position) < distanceToPlayerToStop) //if the player is inside the max range then we can see it
			{
				withinRange= true;
			}	
		}
	}
	public void FindNewPlayerSpawn()
	{
		Debug.Log("FindNewPlayerSpawn() is activated");
		if (target == null && GameObject.FindWithTag("Player"))	
		{
			target = GameObject.FindWithTag("Player").transform;
		}
		if (playerTank == null && GameObject.FindWithTag("Player"))	
		{
			playerTank = GameObject.FindWithTag("Player");
		}
	}
}
