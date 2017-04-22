using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour {

	public float maxSpeed = 5;
	public float currentSpeed;
	public float accerlation = 1;
	PlayerController controller;
	Camera viewCamera;
	void Start() {
		controller = GetComponent<PlayerController> ();
		viewCamera = Camera.main;
		currentSpeed = 0;
	}

	void Update () {

		//Collect Input and Do movement
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
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
		Vector3 velocity = input.normalized * currentSpeed;
		//controller.Move (velocity);
		controller.MoveForward(currentSpeed);
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

	void OnCollisionEnter(Collision collision){
		Debug.Log ("collision");
		Debug.Log (collision.gameObject.name);

		//If colliding with enemy
		//Stop Movement
		//Launch player into air based on enemies velocity
		//Launch enemy into air based on enemies velocity

	}
}
