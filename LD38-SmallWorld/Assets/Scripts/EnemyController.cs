using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	bool grounded;
	public Transform player;
	Vector3 lastKnown;
	Vector3 velocity;
	Rigidbody myRigidbody;
	ThrowSimulation throwSim;
	void Start () {
			grounded = true;
			myRigidbody = GetComponent<Rigidbody>();
			//lastKnown = transform.position;
			throwSim = GetComponent<ThrowSimulation>();
	}

	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate () {
		if (myRigidbody.position.y <= 1.01f) {
			grounded = true;
		}
		//Standard Move
		if (grounded) {
			myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);
		}
		

	}
	public void LookAt(Vector3 lookPoint){
		//look at any point
		if (grounded) {
			Vector3 correctedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
			transform.LookAt (correctedPoint);
		}
	}

	public void FocusOnPlayer(){
		if(Physics.Linecast(transform.position, player.position)){
			lastKnown = player.position;
			Debug.DrawLine(transform.position, player.position, Color.green);
			//Debug.Log("Player Visable");

		}else {

			Debug.Log("Player Not Visable");
		}
		LookAt(lastKnown);
	}

	public void MoveForward(float speed){
		velocity = transform.forward * speed;
	//	Debug.DrawRay (transform.position, velocity, Color.red);
		//Debug.DrawRay (transform.position, velocity * -1, Color.blue);
	}
	
	public void Launch (Vector3 target){
		grounded = false;
		StartCoroutine (throwSim.SimulateProjectile (target));

	}
}
