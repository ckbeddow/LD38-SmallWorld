using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {


	Vector3 velocity;
	Rigidbody myRigidbody;
	public float gravity = 9.8f;
	bool grounded = true;

	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
	}
	

	void FixedUpdate () {

		//Standard Move
		if (grounded) {
			myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);
		}
		//Launched Move

	}

	public void Move(Vector3 _velocity){
		//get velocity for movement from outside class
		velocity = _velocity;
	}

	public void MoveForward(float speed){
		velocity = transform.forward * speed;
	}

//	public void Launch(Vector3 direction, float speed){
//		velocity = Vector3.zero;
//		Vector3 launchVelocity = direction * speed;
//		float Vx = Mathf.Sqrt(launchVelocity) * Mathf.Cos(45f * Mathf.Deg2Rad);
//		float Vy = Mathf.Sqrt(launchVelocity) * Mathf.Sin(45f * Mathf.Deg2Rad);
//
//	}

	public void Flight(float Vx, float Vy){
		float elapse_time = 0;
		while (transform.position.y >= .1) {
			transform.Translate (0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

			elapse_time += Time.deltaTime;
		}
	}


	public void LookAt(Vector3 lookPoint){
		//look at any point
		Vector3 correctedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt (correctedPoint);
	}
}
