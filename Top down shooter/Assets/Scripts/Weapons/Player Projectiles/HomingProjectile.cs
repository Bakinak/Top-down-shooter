using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : ProjectileClass {

	Vector3 target;

	// Use this for initialization
	void Start () {
		applySpeedModifier ();
		//target.y = 1;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		homingMovement ();
		//Debug.Log ("z = "+ target.z + " x = "+  target.x, gameObject);
	}

	void OnTriggerEnter(Collider other){
		damageEnemy (other);
	}

	void homingMovement(){
		//The following doesn't work, as mousePosition is derived from pixels.
		//Thus, the x position will only be 0 if mouse is moved all way to the left.
		target.x = Input.mousePosition.x; 
		target.z = Input.mousePosition.z;
		/*target.x = Camera.main.ScreenToWorldPoint(Input.mousePosition.x);
		target.z = Camera.main.ScreenToWorldPoint(Input.mousePosition.z);*/
		transform.position = Vector3.MoveTowards (transform.position, target, projectileSpeed*Time.deltaTime);

		projectileTravelCounter ();
	}

}
