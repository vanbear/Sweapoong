using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup {

	// Use this for initialization
	void Start () {
		Debug.Log("Shield p-up created");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// check if collided with ball's circleCollider (we don't want to trigger it with ball's expanded box collider)
		if (col.gameObject.tag == "Ball" && col.GetType() == typeof(CircleCollider2D))
		{
			// get current ball's possesion
			int pos = col.gameObject.GetComponent<BallBounce>().possesion;
			// check if the ball is actually in anybody's possesion
			if (pos != 0)
			{
				/////////////////
				// TODO:
				// check if there is no shield already in given player's possesion
				// if not:
					// create shield for given player
				// else:
					// increase his shield's health up to 3
				/////////////////
				
				

				// destroy the pickup
				Debug.Log(pos);
				Destroy(gameObject);
			}
			// nothing happens when the ball is ownerless		
		}
	}
}
