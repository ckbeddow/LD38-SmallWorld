using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : MonoBehaviour {


	Transform myTransform;
	ThrowSimulation throwSim;
	public float throwStrength;
	public float acceleration;
	public float maxSpeed;
	public float currentSpeed;
	EnemyController controller;

	Vector3 preCollisionVelocity = Vector3.zero;
	public float preCollisionSpeed = 0;
	[SerializeField] Player player;

	void Start () {
		myTransform = transform;
		throwSim = GetComponent<ThrowSimulation> ();
		controller = GetComponent<EnemyController>();
		currentSpeed = 0;
	}
	

	void Update () {
		controller.FocusOnPlayer();
		currentSpeed = currentSpeed + acceleration * Time.deltaTime;
		if(currentSpeed > maxSpeed){
			currentSpeed = maxSpeed;
		}
		controller.MoveForward(currentSpeed);
	}

	//FIX THIS
	void OnCollisionEnter(Collision collision){
		preCollisionVelocity = transform.forward * currentSpeed;
		preCollisionSpeed = currentSpeed;
		if (collision.gameObject.name == "Player") {
			currentSpeed = 0;
			Vector3 launchTarget = player.GetVelocity () * (player.currentSpeed * player.throwStrength) + transform.position;
			controller.Launch(launchTarget);
		}else {

		currentSpeed *= .5f;
		}
	}

	public Vector3 GetVelocity(){
		return preCollisionVelocity;
	}
}
