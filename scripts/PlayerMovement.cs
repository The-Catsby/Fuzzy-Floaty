using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	Vector3 movementVelocity = new Vector3(1f,1f,0f);
	float maxVelocity = 5f;
	bool facingRight = true;
	bool didMove;
	Animator animator;
	Rigidbody2D playerRB;

	bool isDead = false;
	float deathCooldown;

	// player boundries
	float max_y = 18;
	float max_x = 39;
	float min_y = 1;
	float min_x = 1;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator> ();
		playerRB = transform.GetComponentInChildren<Rigidbody2D> ();
		if (animator == null)
			Debug.LogError ("Player Animator not found!");		
		if (playerRB == null)
			Debug.LogError ("Player RigidBody not found!");
	}
	
	// Update is called once per frame
	void Update () {

		//Exit
		Input.backButtonLeavesApp = true;
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit();
		
		//If player dies, exit
		if (isDead) {
			deathCooldown -= Time.deltaTime;
			if (deathCooldown < 0) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		else 
		{
			didMove = true;

			//	Keyboard Controls
			if (Input.GetKey (KeyCode.UpArrow))
				velocity.y += movementVelocity.y;
			else if (Input.GetKey (KeyCode.DownArrow))
				velocity.y -= movementVelocity.y;
			else if (Input.GetKey (KeyCode.LeftArrow))
				velocity.x -= movementVelocity.x;
			else if (Input.GetKey (KeyCode.RightArrow))
				velocity.x += movementVelocity.x;
			else
				didMove = false;
			

			//Android Tilt Controls
			velocity.x += 5f*Input.acceleration.x;
			velocity.y += 5f*Input.acceleration.y;

		}

			
	}

	void FixedUpdate(){

		if (isDead)
			return;
		else 
		{
			//Cap velocity at a maximum
			velocity = Vector3.ClampMagnitude (velocity, maxVelocity);

			//Trigger movement animation
			if (didMove)
				animator.SetTrigger ("didMove");
			
			//Flip sprite if moving left
			spriteFacing (velocity);

			//implement position change
			transform.position += velocity * Time.deltaTime;

			//Limit player (x,y) to (min_x, min_y) boundries
			Vector3 player = transform.position;
			player.x = Mathf.Clamp (transform.position.x, min_x, max_x);
			player.y = Mathf.Clamp (transform.position.y, min_y, max_y);

			transform.position = player;
		}
	}
			
	/**********************		spriteFacing()	**********************************
	* This function flips the sprite rendering along the x-axis so that if player 
	* is moving left, sprite faces left; same if moving right
	*****************************************************************************/
	void spriteFacing(Vector3 velocity){
		SpriteRenderer mySprite = GetComponent<SpriteRenderer> ();	//gets a reference to SpriteRenderer

		if (mySprite != null) {
			//If player is moving left & sprite is facing right -> flip-x
			if (velocity.x < 0 && facingRight) {	
				facingRight = false;
				mySprite.flipX = true;
			}
			//If player is moving left & sprite is facing right -> flip-x
			if (velocity.x > 0 && !facingRight) {	
				facingRight = true;
				mySprite.flipX = false;
			}	
		}
	}

	//Called when an object collides with player
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy") {
			animator.SetTrigger ("death");
			isDead = true;
			playerRB.gravityScale = 1;
			deathCooldown = 2f;
		}
	}
} 