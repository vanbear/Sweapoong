using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour 
{

	// MODULES
	public Rigidbody2D rb;
	public BoxCollider2D boxCollider;
	public CircleCollider2D circleCollider;

	// VALUES
	public float force;

	Vector2 startPos;
	Vector2 endPos;
	Vector2 direction;

	float touchTimeStart;
	float touchTimeEnd;
	float touchTimeInterval;
	bool isTouched = false;

	float startSpeed = 300.0f;
	public float speedMultiplier = 3.0f;
	float currentSpeed;

	//Player possesion
	public int possesion = 0;

	// Use this for initialization
	void Start () 
	{
		rb.AddForce (new Vector2 (force, force));
		currentSpeed = startSpeed;
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
				rb.velocity = Vector2.zero; // stop ball when touched
				Debug.Log ("Ball touched.");
				isTouched = true;
				touchTimeStart = Time.time;
				startPos = Input.GetTouch (0).position;
			}
		}


		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && isTouched == true) 
		{

			touchTimeEnd = Time.time;
			touchTimeInterval = touchTimeEnd - touchTimeStart;
			// get touch position
			endPos = Input.GetTouch (0).position;

			// get swipe direction and normalize
			direction = startPos - endPos;
			direction = direction.normalized;

			// add speed
			currentSpeed += speedMultiplier*(Mathf.Log(currentSpeed,2));
			Debug.Log ("Current speed: "+currentSpeed);

			// add force to ball
			GetComponent<Rigidbody2D> ().AddForce (-direction * currentSpeed, ForceMode2D.Impulse);


			isTouched = false;

		}

		
	}
	public float GetVelocity () 
	{
		return rb.velocity.magnitude;

	}
}
