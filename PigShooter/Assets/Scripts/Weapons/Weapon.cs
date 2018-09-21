using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public int magCapacity = 0;
	public int currentAmmoCount = 0;
	public int ammoCount = 0;
	public float reloadTimer = 0f;
	public float reloadDelay = 0f;
	public float shotDelay = 0f;
	public float shotTimer = 0f;
	public float damage = 0f;
	public float shakeValue = 0.03f;
	public Transform shootPoint = null;
	public Transform cameraTransform = null;
	public GameObject cameraToShake = null;
	public Vector3 cameraPosition = Vector3.zero;

	public virtual void Shoot() {

	}

	public virtual void Reload() {
		
	}

	public void Shake() {
		cameraPosition = cameraToShake.transform.position;
		int shakes = Random.Range(10, 20);
		for (int i = 0; i < shakes; ++i) {
			transform.position = new Vector3(
				transform.position.x + Random.Range(-this.shakeValue, this.shakeValue),
				transform.position.y + Random.Range(-this.shakeValue, this.shakeValue),
				transform.position.z + Random.Range(-this.shakeValue, this.shakeValue)
			);
		}
		cameraToShake.transform.position = cameraPosition;
	}


}
