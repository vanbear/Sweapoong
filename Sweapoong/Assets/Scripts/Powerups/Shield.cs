using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
	int possesion;
	int health = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		// if hit by ball
		if (col.gameObject.tag == "Ball")
		{
			// decrease health
			health--;
			// if health drops down to 0 -> destroy
			if (health == 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
