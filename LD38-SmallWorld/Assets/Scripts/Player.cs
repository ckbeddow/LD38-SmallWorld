using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour {

	public float maxSpeed = 5;
	public float currentSpeed;
	public float accerlation = 1;
	public float minPushback = -0.5f;
	public float throwStrength = 1;
	public float preCollisionSpeed;
	PlayerController controller;
	Camera viewCamera;
	ThrowSimulation throwSim;
	private Vector3 preCollisionVelocity;
	Animator animator;
	bool inputEnabled = true;

	void Start() {
		controller = GetComponent<PlayerController> ();
		viewCamera = Camera.main;
		currentSpeed = 0;
		throwSim = GetComponent<ThrowSimulation> ();
		animator = GetComponentInChildren<Animator> ();
	}

	void Update () {

		//Collect Input
		Vector3 input = Vector3.zero;
		if (inputEnabled) {
			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		}
		if (input.magnitude > 0) {
			currentSpeed = currentSpeed + accerlation * Time.deltaTime;
			if (currentSpeed >= maxSpeed) {
				currentSpeed = maxSpeed;
			}
		}else {
			currentSpeed = currentSpeed - accerlation * 2 * Time.deltaTime;
			if (currentSpeed < 0) {
				currentSpeed = 0;
			}
		}
		//Storing speed and velocity to be passed to enemies for throw calculations
		preCollisionSpeed = currentSpeed;
		preCollisionVelocity = transform.forward * currentSpeed;


		//Move
		controller.MoveForward(currentSpeed);
		animator.SetFloat ("speedPercent", currentSpeed / maxSpeed);

	


		//Look to mouse position
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;

		if(groundPlane.Raycast(ray, out rayDistance)){
			Vector3 point = ray.GetPoint (rayDistance);
			//Debug.DrawLine (ray.origin, point, Color.red);
			controller.LookAt(point);
		}
	}

	public Vector3 GetVelocity(){
		return preCollisionVelocity;
	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log ("collision");
		Debug.Log (collision.gameObject.name);
		Vector3 myVelocity = GetVelocity ();

		if (collision.gameObject.tag == "Enemy") {
			//Debug.Log("Should Launch");

			Enemy enemy = collision.gameObject.GetComponent<Enemy> ();
			currentSpeed = 0;
			Vector3 target;

			target = enemy.GetVelocity () * enemy.preCollisionSpeed * throwStrength + transform.position;
			controller.Launch (target);
			animator.SetBool ("InTheAir", true);
//			}

		}

		//Collision with the ground will reallow movement
		if(collision.gameObject.tag == "Ground"){
			Debug.Log ("Collision with ground: " + collision.gameObject.name);
			controller.Ground();
			animator.SetBool ("InTheAir", false);
		}

		//If colliding with enemy
		//Stop Movement
		//Launch player into air based on enemies velocity
		//Launch enemy into air based on enemies velocity

	}
	public void enableControls(bool enabled){
		inputEnabled = enabled;
	}
}
