﻿using UnityEngine;
using System.Collections;


/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class Flashlight : MonoBehaviour 
{
	public GameObject objectTheFlashlightIsOn;
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 0, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -30F;
	public float maximumY = 60F;

	float rotationY = 0F;

	void Start ()
	{
		#region assign objectTheFlashlightIsOn
		if (objectTheFlashlightIsOn == null)	
		{
			if( this.gameObject.transform.parent.gameObject != null)
			{
			objectTheFlashlightIsOn = this.gameObject.transform.parent.gameObject;
			}
			else if(GameObject.FindWithTag("Player"))
			{
				objectTheFlashlightIsOn = GameObject.FindWithTag("Player");
				Debug.Log("Player found, placing flashlight");
			}
			else
			{
				Debug.Log("Player not found, check that gameObjects in scene are tagged.");
			}
		}
		else
		{
			Debug.Log("Flashlight is on: " + objectTheFlashlightIsOn);
		} // end if Player exists
		#endregion
		
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
		//put the flastlight in the same position the player is
		this.transform.position = objectTheFlashlightIsOn.transform.position;
		
		// Flashlight settings
		this.light.type = LightType.Spot;
		this.light.range = 100f;
		this.light.spotAngle= 36f;
		this.light.intensity= 0.1f;
		// script specific
		sensitivityX = 1F;
	 	sensitivityY = 7F;
		minimumX = 0F;
		maximumX = 0F;
		minimumY = -40F;
		maximumY = 60F;
		axes = RotationAxes.MouseY;

	}
	
	void Update ()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			//float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
	//		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}