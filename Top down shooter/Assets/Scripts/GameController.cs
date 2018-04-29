using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	//Game Settings
	public float gameSpeed = 1;
	public GameObject startProjectile;
	public int startWeapon;
	public int maxTeleporters = 2;

	//Display
	public GUIText scoreText;
	public int score;
	public GUIText deathText;

	//Other
	GameObject[] teleporters;

	// Use this for initialization
	void Start () {
		score = 0;
		scoreText.text = "Score: " + score;
		deathText.text = "";
		if (startProjectile != null) {
			GameObject.FindWithTag ("Player").GetComponent<Player> ().bullet = startProjectile;
		}
		GameObject.FindWithTag ("Player").GetComponent<Player> ().weapon = startWeapon;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Methods

	//Make one that objects can call each time the score is increased
	public void Score(int value){
		//Increase score by value, which is given by the objects that was destroyed,
		//and then update the score on the UI.
		score += value;
		scoreText.text = "Score: " + score;
	}

	public void lose(){
		deathText.text = "Thou be deadeth";
	}

	public void resetPlayer (){
		if (startProjectile != null) {
			GameObject.FindWithTag ("Player").GetComponent<Player> ().bullet = startProjectile;
		}
		GameObject.FindWithTag ("Player").GetComponent<Player> ().weapon = startWeapon;
		GameObject.FindWithTag ("Player").transform.position = new Vector3 (0f, 1f, 0f);
		destroyTeleporters ();
	}

	public void destroyTeleporters(){
		teleporters = GameObject.FindGameObjectsWithTag ("Teleport");
		for (int i = 0; i < teleporters.Length; i++) {
			Destroy(teleporters[i]);
		}
		GameObject.FindWithTag ("Player").GetComponent<Player> ().teleportNumber = 0;
	}

}
