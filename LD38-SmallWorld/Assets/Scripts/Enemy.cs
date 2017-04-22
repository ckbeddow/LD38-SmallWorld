using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	Transform myTransform;
	ThrowSimulation throwSim;
	void Start () {
		myTransform = transform;
		throwSim = GetComponent<ThrowSimulation> ();
	}
	

	void Update () {
		
	}

	//FIX THIS
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "Player") {
			Vector3 launchTarget = (collision.gameObject.transform.position - myTransform.position) * 5;
			StartCoroutine( throwSim.SimulateProjectile (launchTarget));
		}
	}
}
