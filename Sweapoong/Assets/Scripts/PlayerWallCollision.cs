using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWallCollision : MonoBehaviour 
{
	
	public int player;
	public GameObject Ball;
	public GameObject healthbar1;
	public GameObject healthbar2;
	int health1 = 100;
	int health2 = 100;

	// Use this for initialization
	void Start () 
	{
		Ball = GameObject.Find ("ball");
		healthbar1 = GameObject.Find("Health1");
		healthbar2 = GameObject.Find("Health2");

	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log ("zycie1: " + health1);
		Debug.Log ("zycie2: " + health2);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ball" && Ball.GetComponent<BallBounce>().possesion == 0 && this.gameObject.tag == "WallDown")
		{
			Debug.Log ("Player" +player.ToString()+ "wall was hit");
			health1 -= 10 + (int)Ball.GetComponent<BallBounce> ().GetVelocity();

			healthbar1.GetComponent<Image> ().fillAmount = (float)health1/100;
			//healthbar.fillAmount = health / 1000;



		}
		if (col.gameObject.tag == "Ball" && Ball.GetComponent<BallBounce> ().possesion == 0 && this.gameObject.tag == "WallUp")
		{

			Debug.Log ("Player" +player.ToString()+ "wall was hit");
			health2 -= 10 + (int)Ball.GetComponent<BallBounce> ().GetVelocity();

			healthbar2.GetComponent<Image> ().fillAmount = (float)health2/100;
			//healthbar.fillAmount = health / 1000;


		}
	}
}
