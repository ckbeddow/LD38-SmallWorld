using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : MonoBehaviour {


	Transform myTransform;
	ThrowSimulation throwSim;
	public float throwStrength;
	NavMeshAgent pathfinder;


	[SerializeField] Player player;

	void Start () {
		myTransform = transform;
		throwSim = GetComponent<ThrowSimulation> ();
		pathfinder = GetComponent<NavMeshAgent> ();
	}
	

	void Update () {
		
	}

	//FIX THIS
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "Player") {
			Vector3 launchTarget = player.GetVelocity () * (player.currentSpeed * player.throwStrength) + transform.position;
			StartCoroutine( throwSim.SimulateProjectile (launchTarget));
		}
	}

	public Vector3 GetVelocity(){
		return Vector3.zero;
	}
}
