using UnityEngine;
using System.Collections;

public class MoveUV : MonoBehaviour {

	float timer = 0f;
	float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > 0.5f) {
			timer = 0f;
			speed = Random.Range(0.5f, 1.5f);
		}

		renderer.material.mainTextureOffset += Vector2.one * speed * Time.deltaTime;
	}
}
