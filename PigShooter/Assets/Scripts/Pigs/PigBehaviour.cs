﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehaviour : MonoBehaviour {

	public GameObject player = null;
	public PlayerScript scriptRef = null;
	public float speed = 0f;
	// Use this for initialization
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		scriptRef = player.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!PlayerScript.powerUpBase[PlayerScript.PowerUps.freeze] || GameManager.startTimer <= 0f) {
			transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
			transform.LookAt(player.transform);
			transform.rotation = Quaternion.Euler( new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z));
			transform.rotation = Quaternion.Euler( new Vector3(0f, transform.eulerAngles.y + 90f, transform.eulerAngles.z));
		}
	}


}
