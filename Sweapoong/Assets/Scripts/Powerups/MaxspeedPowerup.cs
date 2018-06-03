using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxspeedPowerup : Powerup {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// check if collided with ball's circleCollider (we don't want to trigger it with ball's expanded box collider)
		if (col.gameObject.tag == "Ball" && col.GetType() == typeof(CircleCollider2D))
		{
			col.gameObject.GetComponent<BallBounce>().setNewSpeed(600);

			// destroy the pickup
			Destroy(gameObject);		
		}
	}

}
