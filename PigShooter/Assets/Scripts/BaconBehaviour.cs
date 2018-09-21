using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaconBehaviour : MonoBehaviour {

	public float healthRestore = 0f;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerScript>().health += healthRestore;
			Destroy(this.gameObject);
		}
	}
}
