using UnityEngine;
using System.Collections;

public class asteroidMovement : MonoBehaviour {

	Vector3 velocity;
	int count;

	// player boundries
	float max_y = 18;
	float max_x = 39;
	float min_y = 1;
	float min_x = 1;

	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 force = Vector3.zero;
		if (count > 0) {
			count--;
		}
		else {
			count = 200;
			force += new Vector3 (Random.Range (-10f, 10f), Random.Range (-10f, 10f), 0);
			velocity = Vector3.zero;
		}
		velocity += force;
	}

	void FixedUpdate(){
		if (velocity != Vector3.zero) {
			//implement position change
			transform.position += velocity * Time.deltaTime;	
		}
		//Limit player (x,y) to (min_x, min_y) boundries
		Vector3 player = transform.position;
		player.x = Mathf.Clamp (transform.position.x, min_x, max_x);
		player.y = Mathf.Clamp (transform.position.y, min_y, max_y);

		transform.position = player;
	}
}
