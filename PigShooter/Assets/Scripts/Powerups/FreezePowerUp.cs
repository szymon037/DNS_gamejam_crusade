using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerUp : MonoBehaviour {

	public PlayerScript.PowerUps id = PlayerScript.PowerUps.freeze;

	GameObject player = null;

	void Start() {
		player = GameObject.FindWithTag("Player");
	}

	void Update() {
		transform.LookAt(player.transform);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerScript>().EnablePowerUpEffect(this.id);
			Destroy(this.gameObject);
		}
	}
}
