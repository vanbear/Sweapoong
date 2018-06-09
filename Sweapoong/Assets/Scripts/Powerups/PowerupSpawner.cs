using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupSpawner : MonoBehaviour {

	public GameObject[] powerUps;
	public float xRange;
	public float yRange;
	public int spawnInterval;
	public int maxCount;

	 private GameObject[] getCount;

	 private float elapsedTime = 0f;
	// Use this for initialization
	void Start () {
		resetCounter();
	}
	
	// Update is called once per frame
	void Update () {
		// We want to spawn a powerup every *spawnInterval* (in seconds)
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= spawnInterval) {
			resetCounter();
			spawnPowerup();
		}
	}

	// check how many powerup instances
	int getPowerupCount()
	{
		getCount = GameObject.FindGameObjectsWithTag ("Powerup");
        return getCount.Length;
	}

	void spawnPowerup()
	{
		if (getPowerupCount() < maxCount)
		{
			// random position
			float x = Random.Range(-xRange,xRange);
			float y = Random.Range(-yRange,yRange);

			// random powerup
			GameObject obj =  powerUps[Random.Range(0,powerUps.Length)];

			// spawn it
			GameObject inst = Instantiate(obj, new Vector2(x,y), Quaternion.identity) as GameObject;
		}
	}
	
	public void resetCounter()
	{
		elapsedTime = 0f;
	}
}
