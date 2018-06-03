using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup {

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
			// get current ball's possesion
			int pos = col.gameObject.GetComponent<BallBounce>().possesion;
			// check if the ball is actually in anybody's possesion
			if (pos != 0)
			{
				if (pos==1)
				{
					GameObject s = Instantiate (Resources.Load ("shield"), new Vector2 (0, -4.4f), Quaternion.identity) as GameObject;
					s.GetComponent<Shield>().possesion = pos;
				}
				else if (pos==2)
				{
					GameObject s = Instantiate (Resources.Load ("shield"), new Vector2 (0, 4.4f), Quaternion.identity) as GameObject;
					s.GetComponent<Shield>().possesion = pos;
				}

				// destroy the pickup
				Destroy(gameObject);
			}
			// nothing happens when the ball is ownerless		
		}
	}
}
