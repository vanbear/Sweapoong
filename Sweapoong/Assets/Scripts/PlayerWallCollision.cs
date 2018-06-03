using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWallCollision : Photon.MonoBehaviour 
{
	
	public int player;
	public GameObject Ball;
	public GameObject healthbar1;
	public GameObject healthbar2;
	public static int health1 = 100;
	public static int health2 = 100;
	public PhotonView view;

	// Use this for initialization
	void Start () 
	{
		Ball = GameObject.Find("ball");
		healthbar1 = GameObject.Find("Health1");
		healthbar2 = GameObject.Find("Health2");
		view = GetComponent<PhotonView> ();

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
			view.RPC("reduceHealth1",PhotonTargets.AllViaServer);
		}
		if (col.gameObject.tag == "Ball" && col.gameObject.GetComponent<BallBounce> ().possesion == 1 && this.gameObject.tag == "WallUp" && col.gameObject.GetComponent<BallBounce> ().possesion != 0)
		{
			//Debug.Log ("Player " +player.ToString()+ "wall was hit");
			view.RPC("reduceHealth2",PhotonTargets.AllViaServer);
		}
	}

	[PunRPC]
	public void reduceHealth1 ()
	{
		health1 -= 10 + (int)Ball.gameObject.GetComponent<BallBounce> ().GetVelocity();

		healthbar1.GetComponent<Image> ().fillAmount = (float)health1/100;
		//healthbar.fillAmount = health / 1000;
		Ball.gameObject.GetComponent<BallBounce> ().view.RPC("slowDown",PhotonTargets.AllViaServer, 1.5f);
		Ball.gameObject.GetComponent<BallBounce> ().view.RPC("changePossesion",PhotonTargets.AllViaServer, 1);
	}

	[PunRPC]
	public void reduceHealth2 ()
	{
		health2 -= 10 + (int)Ball.gameObject.GetComponent<BallBounce> ().GetVelocity();

		healthbar2.GetComponent<Image> ().fillAmount = (float)health2/100;
		//healthbar.fillAmount = health / 1000;
		Ball.gameObject.GetComponent<BallBounce> ().view.RPC("slowDown",PhotonTargets.AllViaServer, 1.5f);
		Ball.gameObject.GetComponent<BallBounce> ().view.RPC("changePossesion",PhotonTargets.AllViaServer, 2);
	}



	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			
			stream.SendNext (PlayerWallCollision.health1);
			stream.SendNext (PlayerWallCollision.health2);



		} else {
			
			PlayerWallCollision.health1 = (int)stream.ReceiveNext ();
			PlayerWallCollision.health2 = (int)stream.ReceiveNext ();

		}

	}
}
