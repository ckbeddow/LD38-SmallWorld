using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {


	Vector3 velocity;
	Rigidbody myRigidbody;
	ThrowSimulation throwSim;
	public float gravity = 9.8f;
	bool grounded = true;


	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
		throwSim = GetComponent<ThrowSimulation> ();
	}
	

	void FixedUpdate () {
		if (myRigidbody.position.y <= 1.01f) {
			grounded = true;
		}
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
		Debug.DrawRay (transform.position, velocity, Color.red);
		//Debug.DrawRay (transform.position, velocity * -1, Color.blue);
	}

	public void Launch (Vector3 target){
		grounded = false;
		StartCoroutine (throwSim.SimulateProjectile (target));

	}


	public void LookAt(Vector3 lookPoint){
		//look at any point
		if (grounded) {
			Vector3 correctedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
			transform.LookAt (correctedPoint);
		}
	}
}
