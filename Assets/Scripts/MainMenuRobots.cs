using UnityEngine;
using System.Collections;

public class MainMenuRobots : MonoBehaviour 
{
	float timer;
	int ang;
	float randTime;
	// Use this for initialization
	void Start () 
	{
		timer = 0.0f;
		ang = Random.Range (-5, 5) * 25;
		randTime = Random.Range (0.75f, 2.0f);
	}


	void Update()
	{
		timer += Time.deltaTime;
		if(timer > randTime)
		{
			timer = 0.0f;
			ang = Random.Range(-5, 5) * 25;
			randTime = Random.Range (0.75f, 2.0f);
		}
		transform.RotateAround(transform.position, Vector3.forward, ang * Time.deltaTime);
	}

}
