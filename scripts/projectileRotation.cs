using UnityEngine;
using System.Collections;

public class projectileRotation : MonoBehaviour {

	float angle = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		angle += 1;
		transform.rotation = Quaternion.Euler (0, 0, angle);
	}
}
