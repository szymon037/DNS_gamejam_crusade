using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

	// Use this for initialization
	void Start () {
		this.shotDelay = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Shoot() {
		this.shotTimer = this.shotDelay;
		--this.ammoCount;
		RaycastHit hit;
		bool hitTheEnemy = Physics.Raycast(this.shootPoint.position, Vector3.forward, out hit, Mathf.Infinity);
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
		if (ammoCount < magCapacity) {
			currentAmmoCount = ammoCount;
			ammoCount = 0;
			return;
		}
		if (currentAmmoCount != magCapacity && currentAmmoCount != 0) {
			int temp = magCapacity - currentAmmoCount;
			ammoCount -= temp;
			currentAmmoCount = magCapacity;
		}
		currentAmmoCount = magCapacity;
		ammoCount -= magCapacity;
	}
}
