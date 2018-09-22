using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPowerup : MonoBehaviour {

	public PlayerScript.PowerUps id = PlayerScript.PowerUps.speedUp;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerScript>().EnablePowerUpEffect(this.id);
			Destroy(this.gameObject);
		}
	}
}
