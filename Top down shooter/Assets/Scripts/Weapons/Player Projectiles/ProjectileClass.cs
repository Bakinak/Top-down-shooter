using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileClass : MonoBehaviour {

	//Need all the variables here, and the methods don't need arguments, as this just means
	//we are changing the values of the arguments, not the actual necessary variables.
	//Learn how to use constructor correctly, and the problem is solved.

	public float reloadTime;
	public float projectileSpeed;
	float travelledDistance;
	public float maxDistance;
	public float damage;
	float speedModifier;
	GameObject triggeringEnemy;

	/*public ProjectileClass(){

	}*/

	public void applySpeedModifier(){
		speedModifier = GameObject.FindWithTag("GameController").GetComponent<GameController>().gameSpeed;
		projectileSpeed *= speedModifier;

	}

	public void projectileMovement(){
		transform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);
		projectileTravelCounter ();
	}

	public void projectileTravelCounter(){
		travelledDistance += 1 * speedModifier *  Time.deltaTime;
		if (travelledDistance >= maxDistance) {
			expire ();
		}

	}

	//Methods
	public void damageEnemy(Collider other){
		if (other.tag == "Enemy") {
			triggeringEnemy = other.gameObject;
			triggeringEnemy.GetComponent<EnemyStats> ().health -= damage;
			if (triggeringEnemy.GetComponent<EnemyStats> ().health > 0) {
				Destroy (this.gameObject);
			}
		}
		if (other.tag == "Wall") {
			Destroy (this.gameObject);
		}
	}

	public virtual void expire(){
		Destroy (this.gameObject);
	}
}
