using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
	// Use this for initialization

	// lifespan
	public float lifespan = 3;
	public int possesion;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// decrease timeleft
		lifespan -= Time.deltaTime;

		// change opacity
		Color c = this.gameObject.GetComponent<SpriteRenderer>().color;
		c.a = lifespan;
		this.gameObject.GetComponent<SpriteRenderer>().color = c;

		// destroy 
		if ( lifespan < 0 )
		{
			Destroy(gameObject);
		}
	}

}
