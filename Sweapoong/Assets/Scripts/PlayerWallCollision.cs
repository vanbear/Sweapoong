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
	public static int health1 = 100;
	public static int health2 = 100;

	// Use this for initialization
	void Start () 
	{
		Ball = GameObject.Find("ball");
		healthbar1 = GameObject.Find("Health1");
		healthbar2 = GameObject.Find("Health2");

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("zycie1: " + health1);
		//Debug.Log ("zycie2: " + health2);
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "Ball" && col.gameObject.GetComponent<BallBounce>().possesion == 2 && this.gameObject.tag == "WallDown" && col.gameObject.GetComponent<BallBounce> ().possesion != 0)
		{
			//Debug.Log ("Player " +player.ToString()+ "wall was hit");
			health1 -= 10 + (int)col.gameObject.GetComponent<BallBounce> ().GetVelocity();

			healthbar1.GetComponent<Image> ().fillAmount = (float)health1/100;
			//healthbar.fillAmount = health / 1000;
			col.gameObject.GetComponent<BallBounce> ().slowDown (1.5f);
			col.gameObject.GetComponent<BallBounce> ().changePossesion (1);
		}
		if (col.gameObject.tag == "Ball" && col.gameObject.GetComponent<BallBounce> ().possesion == 1 && this.gameObject.tag == "WallUp" && col.gameObject.GetComponent<BallBounce> ().possesion != 0)
		{
			//Debug.Log ("Player " +player.ToString()+ "wall was hit");
			health2 -= 10 + (int)col.gameObject.GetComponent<BallBounce> ().GetVelocity();

			healthbar2.GetComponent<Image> ().fillAmount = (float)health2/100;
			//healthbar.fillAmount = health / 1000;
			col.gameObject.GetComponent<BallBounce> ().slowDown (1.5f);
			col.gameObject.GetComponent<BallBounce> ().changePossesion (2);
		}
	}
}
