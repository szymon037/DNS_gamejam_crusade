using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour {

	public float healthRestore = 0f;

	GameObject player = null;

	void Start() {
		player = GameObject.FindWithTag("Player");
	}

	void Update() {
		transform.LookAt(player.transform);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerScript>().health += healthRestore;
			Destroy(this.gameObject);
		}
	}
}
