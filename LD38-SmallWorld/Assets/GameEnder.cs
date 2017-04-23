using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour {

	public GameObject winPanel;
	public GameObject losePanel;
	public EnemyController enemy;
	public Player player;

	void OnCollisionEnter(Collision collision){
		Debug.Log ("collision");
		if (collision.gameObject.tag == "Enemy") {
			YouWin ();
		}
		if (collision.gameObject.tag == "Player") {
			YouLose ();
		}
		Destroy (collision.gameObject);
	}

	void YouWin(){
		winPanel.SetActive (true);
		player.enableControls (false);
		player.currentSpeed = 0f;
	}

	void YouLose(){
		losePanel.SetActive (true);
		enemy.disableMovment ();
	}
}
