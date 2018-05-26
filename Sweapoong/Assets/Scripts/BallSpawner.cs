using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour 
{
	public GameObject ball;
	public Vector2 direction;
	public GameObject healthbar1;
	public GameObject healthbar2;


	// Use this for initialization
	void Start () 
	{
		healthbar1 = GameObject.FindGameObjectWithTag("WallDown");
		healthbar2 = GameObject.FindGameObjectWithTag("WallUp");
		direction [0] = Random.Range (-180, 180);
		direction [1] = Random.Range (-180, 180);
		ball = Instantiate (Resources.Load ("ball"), new Vector2 (0, 0), Quaternion.identity) as GameObject;
		ball.gameObject.tag = "Ball";
		ball.name = "ball";
		ball.GetComponent<Rigidbody2D> ().AddForce(direction,ForceMode2D.Impulse); 
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		if(healthbar1.GetComponent<PlayerWallCollision>().health1 <= 0 || healthbar1.GetComponent<PlayerWallCollision>().health2 <= 0) 
		{
			Destroy(ball);
			ball = Instantiate (Resources.Load ("ball"), new Vector2 (0, 0), Quaternion.identity) as GameObject;
			ball.gameObject.tag = "Ball";
			ball.name = "ball";

			// initiation impulse doesnt work 
			ball.GetComponent<Rigidbody2D>().AddForce(direction,ForceMode2D.Impulse); 

		}
		if(healthbar2.GetComponent<PlayerWallCollision>().health1 <= 0 || healthbar2.GetComponent<PlayerWallCollision>().health2 <= 0) 
		{
			Destroy(ball);
			ball = Instantiate (Resources.Load ("ball"), new Vector2 (0, 0), Quaternion.identity) as GameObject;
			ball.gameObject.tag = "Ball";
			ball.name = "ball";

			// initiation impulse doesnt work 
			ball.GetComponent<Rigidbody2D>().AddForce(direction,ForceMode2D.Impulse); 

		}
	}
}
