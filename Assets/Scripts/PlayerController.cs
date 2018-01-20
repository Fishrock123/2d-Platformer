using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float velocityMagnitude = 0f;
	public bool isMovementMaterial = false;
	public bool grounded;
	public bool leftGrab;
	public bool rightGrab;

	public float speed = 5f;
	public float jumpMultiplier = 1.5f;
	public float maxSpeed = 20f;
	public float groundedMaxSpeed = 20f;
	// public float slideMultiplier = 2f;
	// public bool sliding = false;
	public Transform groundDetector;
	public Transform rightGrabDetector;
	public Transform leftGrabDetector;

	public PhysicsMaterial2D normalPhysics;
	public PhysicsMaterial2D grabPhysics;
	public PhysicsMaterial2D movementPhysics;

	public bool hasMoved;
	public int collidersTouching = 0;

	private Rigidbody2D body;
	private Collider2D coll;
	private float originalGravityScale;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();

		originalGravityScale = body.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetButton("Jump") == true) {
			hasMoved = true;
		}
	}

	void FixedUpdate () {
		rightGrab = Physics2D.Linecast(transform.position, rightGrabDetector.position, 1 << LayerMask.NameToLayer("Ground"));
		leftGrab = Physics2D.Linecast(transform.position, leftGrabDetector.position, 1 << LayerMask.NameToLayer("Ground"));
		grounded = Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Ground"));
		
		float hx = Input.GetAxisRaw("Horizontal");

		float multiplier = 1f;
		if (Input.GetButton("Jump") && grounded) multiplier *= jumpMultiplier;
		// if (sliding) multiplier *= slideMultiplier;

		bool wasGrab = false;

		if (rightGrab && !grounded) {
			if (hx > 0) {
				multiplier = 1000f;
				wasGrab = true;
			} 
			if (hx < 0) {
				multiplier *= 2f;
			}
		}
		if (leftGrab && !grounded) {
			if (hx < 0) {
				multiplier = 1000f;
				wasGrab = true;
			} 
			if (hx > 0) {
				multiplier *= 2f;
			}
		}

		isMovementMaterial = false;
		if (wasGrab) {
			body.gravityScale = 0f;
			coll.sharedMaterial = grabPhysics;
		} else {
			body.gravityScale = originalGravityScale;
			if (hx > 0 && body.velocity.x > 0 || hx < 0 && body.velocity.x < 0) {
				isMovementMaterial = true;
				coll.sharedMaterial = movementPhysics;
			} else {
				coll.sharedMaterial = normalPhysics;
			}
		}

		// TODO: teleport the player if they get stuck?
		if (rightGrab && leftGrab) {
			coll.sharedMaterial = movementPhysics;
		}

		if (body.velocity.magnitude < groundedMaxSpeed ||
		    	(Input.GetButton("Jump") && grounded)) {
		 	body.AddForce(new Vector2(hx * speed * multiplier, 0f));
		}

		if (Input.GetButton("Jump")) {
			if (grounded) {
				body.AddForce(new Vector2(0f, 450f));
			}
			else if ((rightGrab && hx < 0 || leftGrab && hx > 0) &&
					 collidersTouching > 0) {
				body.AddForce(new Vector2(0f, 800f));
				
				collidersTouching = 0;
			}
		}

		// sliding = grounded && Input.GetButton("Slide");
		// anim.SetFloat(animSlideHash, sliding ? 1f : -1f);

		// Limit velocity
		// Thanks https://answers.unity.com/questions/683158/how-to-limit-speed-of-a-rigidbody.html
     	if (body.velocity.magnitude >= maxSpeed) {
            body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
        }

		velocityMagnitude = body.velocity.magnitude;
	}

	void OnCollisionEnter2D (Collision2D _) {
		collidersTouching += 1;
	}

	void OnCollisionExit2D (Collision2D _) {
		collidersTouching -= 1;

		if (collidersTouching < 0) {
			collidersTouching = 0;
		}
	}
}
