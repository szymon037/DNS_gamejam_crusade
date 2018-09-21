using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
	// Use this for initialization
	void Start () {
		this.shotDelay = 0.2f;
		cameraTransform = cameraToShake.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.shotTimer > 0f) {
			this.shotTimer -= Time.deltaTime;
		}
		if (this.reloadTimer > 0f) {
			this.reloadTimer -= Time.deltaTime;
		}
	}

	public override void Shoot() {
		if (reloadTimer > 0f) return;
		this.shotTimer = this.shotDelay;
		--this.currentAmmoCount;
		RaycastHit hit;
		bool hitTheEnemy = Physics.Raycast(this.shootPoint.position, cameraTransform.forward, out hit, Mathf.Infinity);
		if (!hitTheEnemy) return;
		if (hit.transform != null) {
			if (hit.transform.CompareTag("Pig")) {
				/*obsługa obrażeń*/
				hit.transform.gameObject.GetComponent<Pig>().ReduceHealth(this.damage);
			}
		}
		Shake();
	}

	public override void Reload() {
		if (currentAmmoCount == magCapacity) return;
		reloadTimer = reloadDelay;
		if (ammoCount < magCapacity) {
			currentAmmoCount = ammoCount;
			ammoCount = 0;
			return;
		}
		if (currentAmmoCount != magCapacity && currentAmmoCount != 0) {
			int temp = magCapacity - currentAmmoCount;
			ammoCount -= temp;
			currentAmmoCount = magCapacity;
			return;
		}
		currentAmmoCount = magCapacity;
		ammoCount -= magCapacity;
	}
}
