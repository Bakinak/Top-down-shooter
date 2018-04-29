using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	public float health = 10;
	public float movementspeed;
	public float reloadspeed;
	public float projectilespeed;
	public int scoreValue;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			die ();
		}
	}

	//Methods
	public void die(){
		Destroy (this.gameObject);
		GameObject.FindWithTag ("GameController").GetComponent<GameController> ().Score (scoreValue);

		//The following does the same as the line above:
		//GameObject.FindWithTag ("GameController").SendMessage ("Score", scoreValue);
	}
}