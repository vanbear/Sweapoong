using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour 
{
	// get game objects
	public GameObject ball;
	public GameObject wallUp;
	public GameObject wallDown;
	public GameObject healthBar1;
	public GameObject healthBar2;
	float startSpeed; // ball speed at the start of the round

	// countdown
	bool gameIsRunning = false;

	// Use this for initialization
	void Start () 
	{
		wallUp = GameObject.FindGameObjectWithTag("WallDown");
		wallDown = GameObject.FindGameObjectWithTag("WallUp");
		healthBar1 = GameObject.Find ("Health1");
		healthBar2 = GameObject.Find ("Health2");

		// start countdown
		StartCoroutine(Countdown(3));
	}
	
	// Update is called once per frame
	void Update () 
	{
		// check and update health bars
		checkHealthStatus ();

	}

	void createBall()
	{
		// create ball object
		ball = Instantiate (Resources.Load ("ball"), new Vector2 (0, 0), Quaternion.identity) as GameObject;
		// set its tags
		ball.gameObject.tag = "Ball";
		ball.name = "ball";
	}

	void moveBall(Vector2 direction)
	{
		// get ball's starting speed
		startSpeed = ball.GetComponent<BallBounce> ().startSpeed;
		// move the ball in given direction
		ball.GetComponent<Rigidbody2D> ().AddForce(direction*startSpeed,ForceMode2D.Impulse);
	}

	// checks health status and restarts the game if smbd ded
	void checkHealthStatus()
	{
		if(PlayerWallCollision.health1 <= 0 || PlayerWallCollision.health2 <= 0) 
		{
			// destroy all existing powerups
			GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
			foreach (GameObject p in powerups)
			{
				Destroy(p);
			}
			// reset powerup spawner
			GameObject.Find("Spawner").GetComponent<PowerupSpawner>().resetCounter();

			// refill health and update healthbars
			PlayerWallCollision.health1 = 100;
			PlayerWallCollision.health2 = 100;
			healthBar1.GetComponent<Image> ().fillAmount = 1.0f;
			healthBar2.GetComponent<Image> ().fillAmount = 1.0f;

			// destroy ball and start a countdown to start the game again
			Destroy(ball);
			gameIsRunning = false;
			StartCoroutine(Countdown(3));
		}
	}

	// returns a random normalized direction 
	Vector2 getRandomDirection()
	{
		Vector2 direction = new Vector2();
		// select random direction (up or down) and an angle to launch the ball
		if (Random.Range (0, 2) == 1) // down
		{
			direction [0] = Random.Range (-120, 120);
			direction [1] = -180;
			//Debug.Log ("Dół "+direction);
		} 
		else //up
		{	
			direction [0] = Random.Range (-120, 120);
			direction [1] = 180;
			//Debug.Log ("Góra "+direction);
		}
		return direction.normalized;
	}
		
	IEnumerator Countdown(int seconds)
	{
		// create objects displaying countdown numbers and put then on Canvas
		GameObject countdown1 = Instantiate(Resources.Load("CountdownUI"), new Vector2(0,-360), Quaternion.identity) as GameObject;
		countdown1.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, false);
		GameObject countdown2 = Instantiate(Resources.Load("CountdownUI"), new Vector2(0,360), new Quaternion(0,0,180,0)) as GameObject;
		countdown2.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, false);

		// get a random direction to draw arrow and move ball after countdown
		Vector2 dir = getRandomDirection ();

		// create arrow
		GameObject arrow = Instantiate(Resources.Load("ArrowUI"), new Vector2(0,0)+(dir*60), Quaternion.identity) as GameObject; //dir is multiplied to move the arrow away from the center
		arrow.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, false); //set its parent to Canvas
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // get angle to point
		arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // rotate

		// create a ball, obviously
		createBall();

		// counting loop
		int count = seconds;
		while (count > 0) {
			yield return new WaitForSeconds(1);
			count --;
			//Debug.Log("Countdown: "+count);
		}

		// countdown is finished...
		// reset powerup spawner timer 
		GameObject.Find("Spawner").GetComponent<PowerupSpawner>().resetCounter();
		// destroy countdown objects and arrow
		Destroy(countdown1);
		Destroy(countdown2);
		Destroy(arrow);
		// and move the ball
		moveBall(dir);
		gameIsRunning = true;
	}
}
