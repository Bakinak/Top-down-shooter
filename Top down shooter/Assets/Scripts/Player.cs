using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int numberOfLives = 1;
	public float movementSpeed = 10;
	public float rotationSpeed = 10;
	float reloadSpeed;
	public float reloadTime = 0.5f;
	private float speedModifier;
	public GameObject playerObject;

	//Gun stuff
	public GameObject bulletSpawnPoint;
	public GameObject bullet;
	private Transform bulletSpawned;
	public int weapon;

	//Telepot stuff
	public GameObject teleportProjectile;
	public int teleportNumber = 0;
	//public int maxTeleporters;
	public float teleportReloadTime;
	float teleportReloadProgress;


	void Start () {
		//SetSpeed
		speedModifier = GameObject.FindWithTag("GameController").GetComponent<GameController>().gameSpeed;
		movementSpeed *= speedModifier;
		rotationSpeed *= speedModifier;
		teleportReloadProgress = teleportReloadTime;
	}
	

	void Update () {
		playerRotation ();
		playerMovement ();
		playerShooting ();
	}

	//Methods
	void playerRotation(){
		Plane playerPlane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitDistance = 0.0f;

		if (playerPlane.Raycast (ray, out hitDistance)) {
			Vector3 targetPoint = ray.GetPoint (hitDistance);
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			targetRotation.x = 0;
			targetRotation.z = 0;
			playerObject.transform.rotation = Quaternion.Slerp (playerObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}

	void playerMovement(){
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * movementSpeed;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * movementSpeed;

		transform.Translate (x, 0, z);
	}


	/*playerShooting and shoot method should be moved to a separate weapon object
	as this allows for weapons that shoot multiple projectiles, and weapons that have
	different reload times. Player should however still have the teleportation gun, which
	can always be used no matter what other weapon is equipped */
	void playerShooting(){
		reloadSpeed += 1 * speedModifier * Time.deltaTime;
		teleportReloadProgress += 1 * speedModifier * Time.deltaTime;
		if (Input.GetMouseButton (0)) {
			if (reloadSpeed >= bullet.GetComponent<ProjectileClass>().reloadTime) {
				shoot ();
				reloadSpeed = 0;
			}
		}

		if (Input.GetMouseButton (1)) {
			if (teleportNumber < GameObject.FindWithTag("GameController").GetComponent<GameController>().maxTeleporters && teleportReloadProgress >= teleportReloadTime) {
				
				bulletSpawned = Instantiate (teleportProjectile.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
				bulletSpawned.Rotate (0, bulletSpawnPoint.transform.rotation.eulerAngles.y, 0);

				//teleportNumber += 1;
				teleportReloadProgress = 0;
			}
		}
			
	}

	void shoot(){
		if (weapon == 0) {
			instantiateBullet (0);
		}
		if (weapon == 1) {
			instantiateBullet (5);
			instantiateBullet (-5);
		}
		if (weapon == 2) {
			instantiateBullet (15);
			instantiateBullet (30);
			instantiateBullet (-15);
			instantiateBullet (-30);
		}
	}

	void instantiateBullet(float angle){
		bulletSpawned = Instantiate (bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
		bulletSpawned.Rotate (0, bulletSpawnPoint.transform.rotation.eulerAngles.y+angle, 0);

	}


	//Collission stuff
	//Need to stop enemies from colliding with pickups. 
	//Can probably be done by making an onTriggerEnter, and making pickups triggers.
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy") {
			GameObject.FindWithTag ("GameController").GetComponent<GameController> ().resetPlayer ();
			GameObject.FindWithTag ("GameController").GetComponent<GameController> ().lose ();
			//Destroy (this.gameObject);
		}
			
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "ProjectilePickUp") {
			bullet = other.gameObject.GetComponent<PUProjectile>().projectile;
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "WeaponPickUp") {
			weapon = other.gameObject.GetComponent<PUWeapon>().weapon;
			Destroy (other.gameObject);
		}

	}


}
