using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour 
{
	public GameObject ball;
	public Vector2 direction; // vector containing ball launching direction
	public GameObject wallUp;
	public GameObject wallDown;
	public GameObject healthBar1;
	public GameObject healthBar2;
	float startSpeed;

	void createBall()
	{
		ball = Instantiate (Resources.Load ("ball"), new Vector2 (0, 0), Quaternion.identity) as GameObject;

		startSpeed = ball.GetComponent<BallBounce> ().startSpeed;

		ball.gameObject.tag = "Ball";
		ball.name = "ball";
		ball.GetComponent<Rigidbody2D> ().AddForce(direction.normalized*startSpeed,ForceMode2D.Impulse);
	}

	// Use this for initialization
	void Start () 
	{
		wallUp = GameObject.FindGameObjectWithTag("WallDown");
		wallDown = GameObject.FindGameObjectWithTag("WallUp");
		healthBar1 = GameObject.Find ("Health1");
		healthBar2 = GameObject.Find ("Health2");

		// select random direction (up or down) to launch a ball
		if (Random.Range (0, 2) == 1) // down
		{
			direction [0] = Random.Range (-120, 120);
			direction [1] = -180;
			Debug.Log ("Dół "+direction);
		} 
		else //up
		{	
			direction [0] = Random.Range (-120, 120);
			direction [1] = 180;
			Debug.Log ("Góra "+direction);
		}

		createBall ();
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		if(PlayerWallCollision.health1 <= 0 || PlayerWallCollision.health2 <= 0) 
		{
			PlayerWallCollision.health1 = 100;
			PlayerWallCollision.health2 = 100;
			healthBar1.GetComponent<Image> ().fillAmount = 1.0f;
			healthBar2.GetComponent<Image> ().fillAmount = 1.0f;
			Destroy(ball);
			createBall ();

		}

	}
}
