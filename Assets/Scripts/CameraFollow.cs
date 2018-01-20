using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public Vector3 cameraOffset;
	public float dampTime = 0.05f;
	public float velocityMod = 1f;
	public string targetTag = "Player";

	private Transform target;
	// private Vector3 velocity = Vector3.zero;

	void Awake () {
		// Setting up the reference.
		target = GameObject.FindGameObjectWithTag(targetTag).transform;
	}

	void LateUpdate () {
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z) + cameraOffset;
		
		// Thanks to https://answers.unity.com/answers/1156821/view.html
		// if (target) {
		// 	Vector3 velocity = target.GetComponent<Rigidbody2D>().velocity;
        //     Vector3 aheadPoint = target.position + (new Vector3(velocity.x, 0, 0) * velocityMod);// + cameraOffset;
        //     Vector3 point = Camera.main.WorldToViewportPoint(aheadPoint);
        //     Vector3 delta = aheadPoint - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        //     Vector3 destination = transform.position + delta;// + cameraOffset;
        //     transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);// + cameraOffset;
        // }
	}
}
