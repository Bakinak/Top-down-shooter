using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {
	Transform target;
	private float movementSpeed;
	private float speedModifier;

	// Update is called once per frame
	void Start (){
		movementSpeed = GetComponent<EnemyStats> ().movementspeed;
		speedModifier = GameObject.FindWithTag("GameController").GetComponent<GameController>().gameSpeed;
		movementSpeed *= speedModifier;
	}

	void FixedUpdate () {
		float step = movementSpeed * Time.deltaTime;
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		}
	}
}
