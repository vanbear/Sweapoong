using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour 
{


	public Rigidbody2D rb;
	public BoxCollider2D boxCollider;
	public CircleCollider2D circleCollider;
	public float force;
	Vector2 startPos;
	Vector2 endPos;
	Vector2 direction;
	float touchTimeStart;
	float touchTimeEnd;
	float touchTimeInterval;
	bool isTouched = false;

	//Player possesion
	public int possesion = 0;

	// Use this for initialization
	void Start () 
	{
		rb.AddForce (new Vector2 (force, force));
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("lala " + rb.velocity.magnitude);
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			

			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			if (boxCollider.OverlapPoint(wp))
			{
				//your code
				rb.velocity = Vector2.zero;
				Debug.Log ("Hello");
				isTouched = true;
				touchTimeStart = Time.time;
				startPos = Input.GetTouch (0).position;
			}


		}


		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && isTouched == true) 
		{

			touchTimeEnd = Time.time;
			touchTimeInterval = touchTimeEnd - touchTimeStart;
			endPos = Input.GetTouch (0).position;
			direction = startPos - endPos;

			GetComponent<Rigidbody2D> ().AddForce (-direction/touchTimeInterval * force,ForceMode2D.Impulse);
			isTouched = false;

		}

		
	}
	public float GetVelocity () 
	{
		return rb.velocity.magnitude;

	}
}
