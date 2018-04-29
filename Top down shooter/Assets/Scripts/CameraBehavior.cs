using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	public Transform player;
	public float smooth = 0.1f;
	public float zPosition = 1;
	public float yPosition = 15;

	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = new Vector3 ();
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			position.x = player.position.x;
			position.z = player.position.z - zPosition;
			position.y = player.position.y + yPosition;

			transform.position = Vector3.SmoothDamp (transform.position, position, ref velocity, smooth);
		}

	}

	//Methods
}
