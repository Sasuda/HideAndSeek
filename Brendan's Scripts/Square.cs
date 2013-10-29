using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour 
{
	int player;
	
	// Use this for initialization
	void Start () 
	{
		int player = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void SetPlayer(int selected)
	{
		player = selected;
		
		if (player == 1)
		{
			gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
		}
		else if (player == 2)
		{
			gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		}
	}
	
	public int GetPlayer()
	{
		return player;
	}
	
	public void Reset()
	{
		player = 0;
		gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
	}
}
