using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5f;
	public float maxSpeed = 20f;
	public float slideMultiplier = 2f;
	public bool sliding = false;
	public Transform groundDetector;
	public Transform rightGrabDetector;
	public Transform leftGrabDetector;
	public Animator anim;

	public PhysicsMaterial2D normalPhysics;
	public PhysicsMaterial2D grabPhysics;

	public int grabJumpCount = 0;

	private Rigidbody2D body;
	private Collider2D coll;
	private int animSlideHash = Animator.StringToHash("Sliding");

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		bool rightGrab = Physics2D.Linecast(transform.position, rightGrabDetector.position, 1 << LayerMask.NameToLayer("Ground"));
		bool leftGrab = Physics2D.Linecast(transform.position, leftGrabDetector.position, 1 << LayerMask.NameToLayer("Ground"));
		bool grounded = Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Ground"));
		
		float hx = Input.GetAxisRaw("Horizontal");

		float multiplier = 1f;
		if (sliding) multiplier *= slideMultiplier;

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
		if (wasGrab) {
			coll.sharedMaterial = grabPhysics;
		} else {
			coll.sharedMaterial = normalPhysics;
		}

		body.AddForce(new Vector2(hx * speed * slideMultiplier, 0f));

		if (Input.GetButton("Jump")) {
			if (grounded) {
				body.AddForce(new Vector2(0f, 450f));
			}
			else if (rightGrab && hx < 0 ||
					 leftGrab && hx > 0) {
				body.AddForce(new Vector2(0f, 450f));
				grabJumpCount += 1;
			}
		}

		sliding = grounded && Input.GetButton("Slide");
		anim.SetFloat(animSlideHash, sliding ? 1f : -1f);

		// Limit velocity
		// Thanks https://answers.unity.com/questions/683158/how-to-limit-speed-of-a-rigidbody.html
         if (body.velocity.magnitude >= maxSpeed) {
             body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
         }
	}
}
