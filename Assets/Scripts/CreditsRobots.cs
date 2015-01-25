using UnityEngine;
using System.Collections;

public class CreditsRobots : MonoBehaviour 
{
	float timer;
	Vector3 direction;
	float randTime;
	// Use this for initialization
	void Start () 
	{
		timer = 0.0f;
		direction.x = Random.Range (-5, 5) * 10;
		direction.y = Random.Range (-5, 5) * 10;
		randTime = Random.Range (0.75f, 2.0f);
	}

	void Update()
	{
		Vector3 pos = transform.position;
		timer += Time.deltaTime;
		if(timer > randTime)
		{
			timer = 0.0f;
			direction.x = Random.Range (-5, 5) * 10;
			direction.y = Random.Range (-5, 5) * 10;
			randTime = Random.Range (0.75f, 2.0f);
		}
		pos += direction * Time.deltaTime;
		pos.x = Mathf.Clamp (pos.x, 75.0f, 625.0f);
		pos.y = Mathf.Clamp (pos.x, 75.0f, 325.0f);
		transform.position = pos;
		transform.RotateAround( transform.position, Vector3.forward, 60.0f * Time.deltaTime);
	}
}
