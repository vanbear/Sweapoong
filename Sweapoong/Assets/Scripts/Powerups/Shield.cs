using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
	// Use this for initialization

	// lifespan
	float timeLeft = 2;
	public int possesion;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// decrease timeleft
		timeLeft -= Time.deltaTime;

		// change opacity
		Color c = this.gameObject.GetComponent<SpriteRenderer>().color;
		c.a = timeLeft;
		this.gameObject.GetComponent<SpriteRenderer>().color = c;

		// destroy 
		if ( timeLeft < 0 )
		{
			Destroy(gameObject);
		}
	}

}
