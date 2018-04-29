using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProjectile : ProjectileClass {

	bool stopMoving = false;
	public int teleportNumber;

	// Use this for initialization
	//Rather than using an int from player, can make this script scan for all other
	//teleporters and change its teleport number accordingly. Can also use this when checking
	//whether player is allowed to shoot more projectiles.
	void Start () {
		applySpeedModifier ();
		teleportNumber = GameObject.FindWithTag ("Player").GetComponent<Player> ().teleportNumber;
		GameObject.FindWithTag ("Player").GetComponent<Player> ().teleportNumber += 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (stopMoving == false) {
			projectileMovement ();
		}
		teleportPlayer ();
	}


	void teleportPlayer(){
		//Teleport
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			if (teleportNumber == 0) {
				GameObject.FindWithTag ("Player").transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
				GameObject.FindWithTag ("Player").GetComponent<Player> ().teleportNumber -= 1;
				Destroy (this.gameObject);
			}
			teleportNumber -= 1;
			/*if (GameObject.FindWithTag ("Player").GetComponent<Player> ().teleportNumber > 0) {
				GameObject.FindWithTag ("Player").GetComponent<Player> ().teleportNumber -= 1;
			}*/
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			stopMoving = true;
		}

	}

	public override void expire ()
	{
		Debug.Log ("Hello, this works");
		stopMoving = true;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Wall") {
			stopMoving = true;
		}
	}
}
