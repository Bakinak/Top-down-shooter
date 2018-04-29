using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectiles : ProjectileClass {

	// Use this for initialization
	void Start () {
		applySpeedModifier ();
	}

	// Update is called once per frame
	void Update () {
		projectileMovement();
	}

	void OnTriggerEnter(Collider other){
		damageEnemy(other);
	}
}
