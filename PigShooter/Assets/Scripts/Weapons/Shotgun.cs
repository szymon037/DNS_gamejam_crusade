using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

	public int minPellets = 0;
	public int maxPellets = 0;

	// Use this for initialization
	void Start () {
		this.shotDelay = 0.33f;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.shotTimer > 0f) {
			this.shotTimer -= Time.deltaTime;
		}
	}

	public override void Shoot() {
		this.shotTimer = this.shotDelay;
		//RaycastHit hit = Physics.Raycast();
		int pelletCount = Random.Range(minPellets, maxPellets);
		RaycastHit hit;
		bool enemyIsHit = false;
		for (int i = 0; i < pelletCount; ++i) {
			enemyIsHit = Physics.Raycast(this.shootPoint.position, Vector3.forward, out hit, Mathf.Infinity);
			if (hit.transform != null) {
				if (hit.transform.gameObject.CompareTag("Pig")) {
					hit.transform.gameObject.GetComponent<Pig>().ReduceHealth(this.damage);
				}
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
