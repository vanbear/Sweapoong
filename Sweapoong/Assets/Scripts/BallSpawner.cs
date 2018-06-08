using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : Photon.PunBehaviour
{
    // get game objects
    public GameObject ball;
    public GameObject wallUp;
    public GameObject wallDown;
    public GameObject healthBar1;
    public GameObject healthBar2;
    public PhotonView view;
    float startSpeed; // ball speed at the start of the round
    Vector2 dir;

    // countdown
    bool gameIsRunning = false;
    bool ballInstance = false;

    // Use this for initialization
    void Start()
    {
        wallUp = GameObject.FindGameObjectWithTag("WallDown");
        wallDown = GameObject.FindGameObjectWithTag("WallUp");
        healthBar1 = GameObject.Find("Health1");
        healthBar2 = GameObject.Find("Health2");
        view = GetComponent<PhotonView>();

        // start countdown
        StartCoroutine(Countdown(3));
    }

    // Update is called once per frame
    void Update()
    {
        // check and update health bars
        view.RPC("checkHealthStatus", PhotonTargets.All);

        if (ballInstance == true)
        {
            view.RPC("setBallInstance", PhotonTargets.All);
            ballInstance = false;
        }
            
        //checkHealthStatus ();

    }
    [PunRPC]
    void setBallInstance()
    {
        wallDown.GetComponent<PlayerWallCollision>().Ball = GameObject.FindGameObjectWithTag("Ball");//PhotonView.Find(4).gameObject;
        wallUp.GetComponent<PlayerWallCollision>().Ball = GameObject.FindGameObjectWithTag("Ball");//PhotonView.Find(4).gameObject;
        //wallUp.GetComponent<PlayerWallCollision>().Ball = GameObject.Find("ball");



    }

    
    void createBall()
    {
        // create ball object
        ball = PhotonNetwork.InstantiateSceneObject("ball", new Vector2(0, 0), Quaternion.identity, 0, null) as GameObject;
        //ball.GetComponent<BallBounce>().view.viewID = 4;

        // set its tags
        ball.gameObject.tag = "Ball";
        ball.name = "ball";

        //wallUp.GetComponent<PlayerWallCollision>().Ball = GameObject.Find("ball");
        //wallDown.GetComponent<PlayerWallCollision>().Ball = GameObject.Find("ball");
        ballInstance = true;
    }
    [PunRPC]
    void moveBall(Vector2 direction)
    {
        // get ball's starting speed
        startSpeed = ball.GetComponent<BallBounce>().startSpeed;
        // move the ball in given direction
        ball.GetComponent<Rigidbody2D>().AddForce(direction * startSpeed, ForceMode2D.Impulse);
    }

    // checks health status and restarts the game if smbd ded
    [PunRPC]
    void checkHealthStatus()
    {
        if (PlayerWallCollision.health1 <= 0 || PlayerWallCollision.health2 <= 0)
        {
            // refill health and update healthbars
            PlayerWallCollision.health1 = 100;
            PlayerWallCollision.health2 = 100;
            healthBar1.GetComponent<Image>().fillAmount = 1.0f;
            healthBar2.GetComponent<Image>().fillAmount = 1.0f;

            // destroy ball and start a countdown to start the game again
            PhotonNetwork.Destroy(ball);
            gameIsRunning = false;
            StartCoroutine(Countdown(3));
        }
    }

    // returns a random normalized direction 
    Vector2 getRandomDirection()
    {
        Vector2 direction = new Vector2();
        // select random direction (up or down) and an angle to launch the ball
        if (Random.Range(0, 2) == 1) // down
        {
            direction[0] = Random.Range(-120, 120);
            direction[1] = -180;
            //Debug.Log ("Dół "+direction);
        }
        else //up
        {
            direction[0] = Random.Range(-120, 120);
            direction[1] = 180;
            //Debug.Log ("Góra "+direction);
        }
        return direction.normalized;
    }

    IEnumerator Countdown(int seconds)
    {
        // create objects displaying countdown numbers and put then on Canvas
        GameObject countdown1 = Instantiate(Resources.Load("CountdownUI"), new Vector2(0, -360), Quaternion.identity) as GameObject;
        countdown1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        GameObject countdown2 = Instantiate(Resources.Load("CountdownUI"), new Vector2(0, 360), new Quaternion(0, 0, 180, 0)) as GameObject;
        countdown2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        // get a random direction to draw arrow and move ball after countdown
        dir = getRandomDirection();

        // create arrow
        GameObject arrow = Instantiate(Resources.Load("ArrowUI"), new Vector2(0, 0) + (dir * 60), Quaternion.identity) as GameObject; //dir is multiplied to move the arrow away from the center
        arrow.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false); //set its parent to Canvas
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // get angle to point
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // rotate

        // create a ball, obviously
        createBall();
        view.RPC("setBallInstance", PhotonTargets.All);
        //view.RPC("createBall", PhotonTargets.Others);

        // counting loop
        int count = seconds;
        while (count > 0) {
            yield return new WaitForSeconds(1);
            count--;
            //Debug.Log("Countdown: "+count);
        }

        // count down is finished...
        // destroy countdown objects and arrow
        Destroy(countdown1);
        Destroy(countdown2);
        Destroy(arrow);
        // and move the ball
        view.RPC("moveBall", PhotonTargets.All, dir);
        gameIsRunning = true;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(dir);
            stream.SendNext(PlayerWallCollision.health1);
            stream.SendNext(PlayerWallCollision.health2);
            stream.SendNext(ballInstance);




        }
        else
        {
            dir = (Vector2)stream.ReceiveNext();
            PlayerWallCollision.health1 = (int)stream.ReceiveNext();
            PlayerWallCollision.health2 = (int)stream.ReceiveNext();
            ballInstance = (bool)stream.ReceiveNext();

        }
	
	}
}



