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
	Animator animator;
	public bool victorious;

	Vector3 preCollisionVelocity = Vector3.zero;
	public float preCollisionSpeed = 0;
	[SerializeField] Player player;

	void Start () {
		myTransform = transform;
		throwSim = GetComponent<ThrowSimulation> ();
		controller = GetComponent<EnemyController>();
		currentSpeed = 0;
		animator = GetComponentInChildren<Animator> ();
		victorious = false;
	}
	

	void Update () {
		controller.FocusOnPlayer();
		currentSpeed = currentSpeed + acceleration * Time.deltaTime;
		preCollisionSpeed = currentSpeed;
		preCollisionVelocity = transform.forward * currentSpeed;
		if(currentSpeed > maxSpeed){
			currentSpeed = maxSpeed;
		}

		if (victorious) {
			Debug.Log ("enemy wins");
			controller.disableMovment ();
			currentSpeed = 0;
		}
		animator.SetFloat ("speedPercent", currentSpeed / maxSpeed);
		controller.MoveForward(currentSpeed);
	}


	void OnCollisionEnter(Collision collision){


		//Collision with player causes launch
		if (collision.gameObject.tag == "Player") {
			Debug.Log ("Enemy has collided with player");
			currentSpeed = 0;
			Vector3 launchTarget = player.GetVelocity () * (player.preCollisionSpeed * player.throwStrength) + transform.position;
			animator.SetBool ("InTheAir", true);
			controller.Launch(launchTarget);
		}else {
		//Collsion with obsticals lowers current speed
		currentSpeed *= .5f;
		}

		if (collision.gameObject.tag == "Ground") {
			controller.SetGrounded (true);
			animator.SetBool ("InTheAir", false);
		}
	}

	public Vector3 GetVelocity(){
		return preCollisionVelocity;
	}
}
