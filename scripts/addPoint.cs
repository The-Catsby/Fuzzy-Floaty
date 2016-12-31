using UnityEngine;
using System.Collections;

public class addPoint : MonoBehaviour {

	// player boundries
	float max_y = 18;
	float max_x = 39;
	float min_y = 1;
	float min_x = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "Player") {
			Score.AddPoint ();
			changePos ();
		}
	}

	void changePos(){
		Vector3 pos = transform.position;

		pos.x = Random.Range (min_x, max_x);
		pos.y = Random.Range (min_y, max_y);

		transform.position = pos;
	}
}
