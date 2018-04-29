using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCollision : MonoBehaviour 
{
	
	public int player;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ball")
		{
			Debug.Log ("Player" +player.ToString()+ "wall was hit");

		}
	}
}
