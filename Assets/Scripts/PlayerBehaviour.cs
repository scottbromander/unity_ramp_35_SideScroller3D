using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	private Rigidbody rigidBody;
	public Vector2 jumpForce = new Vector2 (0, 450);

	public float maxSpeed = 3.0f;

	public float speed = 50.0f;

	private float xMove;
	private bool shouldJump;
	private bool onGround;
	private float yPrevious;
	private bool collidingWall;


	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		shouldJump = false;
		xMove = 0.0f;
		onGround = false;
		collidingWall = false;
		yPrevious = Mathf.Floor (transform.position.y);
	}

	void FixedUpdate() {
		Movement ();

		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		CheckGrounded ();
		Jumping ();
	}

	void OnCollisionStay(Collision collision){
		if (!onGround) {
			collidingWall = true;
		}
	}

	void OnCollisionExit(Collision collision){
		collidingWall = false;
	}

	void Movement() {
		xMove = Input.GetAxis ("Horizontal");

		if (xMove != 0) {
			float xSpeed = Mathf.Abs (xMove * rigidBody.velocity.x);

			if (collidingWall && !onGround) {
				xMove = 0;
			}

			if (xSpeed < maxSpeed) {
				Vector3 movementForce = new Vector3(1,0,0);
				movementForce *= xMove * speed;

				RaycastHit hit;
				if(!rigidBody.SweepTest(movementForce, out hit, 0.05f))
				{
					rigidBody.AddForce(movementForce);
				}
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

	void CheckGrounded(){
		float distance = (GetComponent<CapsuleCollider> ().height / 2 * this.transform.localScale.y) + 0.01f; 
		Vector3 floorDirection = transform.TransformDirection (-Vector3.up);
		Vector3 origin = transform.position;

		if (!onGround) {
			if (Physics.Raycast (origin, floorDirection, distance)) {
				onGround = true;
			}
		} else if((Mathf.Floor(transform.position.y) != yPrevious)){
			onGround = false;
		}

		yPrevious = Mathf.Floor (transform.position.y);
	}

	void Jumping(){
		if(Input.GetButtonDown("Jump")){
			shouldJump = true;
		}

		if(shouldJump && onGround){
			rigidBody.AddForce(jumpForce);
			shouldJump = false;
		}
	}

	void OnDrawGizmos() {
		//Debug.DrawLine (transform.position, transform.position + rigidBody.velocity, Color.red);
	}
}
