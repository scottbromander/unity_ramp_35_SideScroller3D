using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	private Rigidbody rigidBody;
	public Vector2 jumpForce = new Vector2 (0, 450);

	public float maxSpeed = 3.0f;

	public float speed = 50.0f;

	private float xMove;
	private bool shouldJump;


	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		shouldJump = false;
		xMove = 0.0f;
	}

	void FixedUpdate() {
		Movement ();

		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		Jumping ();
	}

	void Movement() {
		xMove = Input.GetAxis ("Horizontal");

		if (xMove != 0) {
			float xSpeed = Mathf.Abs (xMove * rigidBody.velocity.x);

			if (xSpeed < maxSpeed) {
				Vector3 movementForce = new Vector3 (1, 0, 0);
				movementForce *= xMove * speed;
				rigidBody.AddForce (movementForce);
			}

			if (Mathf.Abs (rigidBody.velocity.x) > maxSpeed) {
				Vector2 newVelocity;
				newVelocity.x = Mathf.Sign (rigidBody.velocity.x) * maxSpeed;
				newVelocity.y = rigidBody.velocity.y;
				rigidBody.velocity = newVelocity;
			}
		} else {
			Vector2 newVelocity = rigidBody.velocity;
			newVelocity.x *= 0.9f;
			rigidBody.velocity = newVelocity;
		}
	}

	void Jumping(){
		if(Input.GetButtonDown("Jump")){
			shouldJump = true;
		}

		if(shouldJump){
			rigidBody.AddForce(jumpForce);
			shouldJump = false;
		}
	}
}
