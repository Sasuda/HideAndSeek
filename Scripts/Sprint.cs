using UnityEngine;
using System.Collections;

public class Sprint : MonoBehaviour 
{

	public float walkSpeed = 7; // regular speed
    public float crchSpeed = 3; // crouching speed
    public float runSpeed = 9; // run speed
	
	//
	public float runTimer = 4f;
	public float recoverTimer = 4.0f;
	bool isRecoverTimerActive;
	bool isRunning;
 
    private CharacterMotor chMotor;
    private Transform tr;
    private float dist; // distance to ground
 
    // Use this for initialization
    void Start () 
    {
		recoverTimer = 4.0f;
		isRunning = false;
		isRecoverTimerActive = false;
       chMotor =  GetComponent<CharacterMotor>();
        tr = transform;
        CharacterController ch = GetComponent<CharacterController>();
        dist = ch.height/2; // calculate distance to ground
    }
 	
	
	
    // Update is called once per frame
    void FixedUpdate ()
    {
       float vScale = 1.0f;
        float speed = walkSpeed;
		if(runTimer > 0.00f && recoverTimer == 4.0f && isRecoverTimerActive == false)
		{
	        if ((Input.GetKey("left shift") || Input.GetKey("right shift")) && chMotor.grounded)
	        {
				isRunning = true;
	            speed = runSpeed;  
				runTimer -= Time.deltaTime;// decrease time left
			}
        }
		else if(runTimer < 4.0f && isRunning == false)
		{
			runTimer += Time.deltaTime;
		}
		else if(runTimer <= 0)
		{
			isRecoverTimerActive = true;
		}
		
		if(isRecoverTimerActive == true)
		{
			if(recoverTimer > 0.0f)
			{
				recoverTimer -= Time.deltaTime;
				Debug.Log("recover timer is greater than 0");
			}
			else
			{
				isRunning = false;
				isRecoverTimerActive = false;
				runTimer = 4f;
				Debug.Log ("isRunning = false; isRecoverTimerActive = false;");
			}
		}
		else
		{
			recoverTimer = 4.0f;
			Debug.Log ("recovertimer reset");
		}
 
        if (Input.GetKey("c"))
        { // press C to crouch
            vScale = 0.5f;
            speed = crchSpeed; // slow down when crouching
        }
 
        chMotor.movement.maxForwardSpeed = speed; // set max speed
        float ultScale = tr.localScale.y; // crouch/stand up smoothly 
 
       Vector3 tmpScale = tr.localScale;
       Vector3 tmpPosition = tr.position;
 
       tmpScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5 * Time.deltaTime);
       tr.localScale = tmpScale;
 
       tmpPosition.y += dist * (tr.localScale.y - ultScale); // fix vertical position       
       tr.position = tmpPosition;
    }
}
/*
 * using UnityEngine;
using System.Collections;
 
public class RunAndCrouch : MonoBehaviour 
{
    public float walkSpeed = 7; // regular speed
    public float crchSpeed = 3; // crouching speed
    public float runSpeed = 20; // run speed
 
    private CharacterMotor chMotor;
    private Transform tr;
    private float dist; // distance to ground
 
    // Use this for initialization
    void Start () 
    {
       chMotor =  GetComponent<CharacterMotor>();
        tr = transform;
        CharacterController ch = GetComponent<CharacterController>();
        dist = ch.height/2; // calculate distance to ground
    }
 
    // Update is called once per frame
    void FixedUpdate ()
    {
       float vScale = 1.0f;
        float speed = walkSpeed;
 
        if ((Input.GetKey("left shift") || Input.GetKey("right shift")) && chMotor.grounded)
        {
            speed = runSpeed;       
        }
 
        if (Input.GetKey("c"))
        { // press C to crouch
            vScale = 0.5f;
            speed = crchSpeed; // slow down when crouching
        }
 
        chMotor.movement.maxForwardSpeed = speed; // set max speed
        float ultScale = tr.localScale.y; // crouch/stand up smoothly 
 
       Vector3 tmpScale = tr.localScale;
       Vector3 tmpPosition = tr.position;
 
       tmpScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5 * Time.deltaTime);
       tr.localScale = tmpScale;
 
       tmpPosition.y += dist * (tr.localScale.y - ultScale); // fix vertical position       
       tr.position = tmpPosition;
    }
}
*/