using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : Photon.MonoBehaviour 
{

	// MODULES
	public Rigidbody2D rb;
	public BoxCollider2D boxCollider;
	public CircleCollider2D circleCollider;
	public PhotonView view;

	// VALUES
	public float force;

	// touchscreen handling
	Vector2 startPos;
	Vector2 endPos;
	Vector2 direction;
	bool isTouched = false;

	// ball propeties
	public float startSpeed = 300.0f;
	public float maxSpeed = 800.0f;
	public float speedMultiplier = 3.0f;
	public float currentSpeed;

	//Player possesion and current area
	public int possesion = 0;
	public int currentArea = 0;

	// colors
	Color m_Red = new Color(1f,0.25f,0.25f);
	Color m_Blue = new Color(0f,0.2f,0.6f);

	// Use this for initialization
	void Start () 
	{
		//rb.AddForce (new Vector2 (force, force));
		currentSpeed = startSpeed;
	}
	private void Awake()
	{
		view = GetComponent<PhotonView> ();
	}
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("lala " + rb.velocity.magnitude);

			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && possesion!= currentArea) 
			{
				Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (boxCollider.OverlapPoint(wp))
				{
					//your code
					rb.velocity = Vector2.zero; // stop ball when touched
					//Debug.Log ("Ball touched.");
					isTouched = true;
					startPos = Input.GetTouch (0).position;
					changePossesion (currentArea);
				}
			}


			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && isTouched == true ) 
			{
				// get touch position
				endPos = Input.GetTouch (0).position;

				// get swipe direction and normalize
				direction = startPos - endPos;
				direction = direction.normalized;

				// add speed
				currentSpeed = Mathf.Clamp(currentSpeed + speedMultiplier*(Mathf.Log(currentSpeed,2)),startSpeed,maxSpeed);
				//Debug.Log ("Current speed: "+currentSpeed);

				// add force to ball
				GetComponent<Rigidbody2D> ().AddForce (-direction * currentSpeed, ForceMode2D.Impulse);


				isTouched = false;
			}


	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Area1") 
		{
			currentArea = 1;
		}
		if (col.gameObject.tag == "Area2") 
		{
			currentArea = 2;
		}
		//Debug.Log ("Possesion :" + possesion);
		//Debug.Log ("Current area :" + currentArea);
	}

	public void changePossesion(int target)
	{
		//Debug.Log ("Toggle possesion");
		possesion = target;
		if (possesion == 1) 
		{
			this.gameObject.GetComponent<SpriteRenderer> ().color = m_Blue;

		} 
		else
		{
			this.gameObject.GetComponent<SpriteRenderer> ().color = m_Red;
		}
			
	}

	public float GetVelocity () 
	{
		return rb.velocity.magnitude;
	}

	// tutaj miałem zagwozdkę jak spowolnić tą piłkę, mogłem po prostu podzielić rb.velocity, ale wtedy się dziwnie zachowywała
	// po odbiciu, bo były inne wartości prędkości w atrybutach obiektu, więc po prostu wrzucam sobie kierunek do zmiennej tymczasowej,
	// zatrzymuję piłkę w miejscu i od razu puszczam ją w tym kierunku ze zmniejszoną prędkością w taki sam sposób jak przy flicku
	public void slowDown(float divider)
	{
		currentSpeed = Mathf.Clamp(currentSpeed/divider,startSpeed,maxSpeed);
		Vector2 temp = transform.InverseTransformDirection (rb.velocity).normalized;
		rb.velocity = Vector2.zero;
		GetComponent<Rigidbody2D> ().AddForce (temp * currentSpeed, ForceMode2D.Impulse);
	}

}
