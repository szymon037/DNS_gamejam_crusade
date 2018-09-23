using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

	public PlayerScript script = null;

	void Start () {
		this.shotDelay = 0.25f;
		cameraTransform = cameraToShake.transform;
		soundSource.volume = 0.6f;
		reloadSound.volume = 0.9f;	
		script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.shotTimer > 0f) {
			this.shotTimer -= Time.deltaTime;
		}
		if (this.reloadTimer > 0f) {
			this.reloadTimer -= Time.deltaTime;
		}
		if (muzzleTimer > 0f) muzzleTimer -= Time.deltaTime;
	}

	public override void Shoot() {
		if (reloadTimer > 0f) return;
		if (shotTimer > 0f) return;
		muzzleFlash.gameObject.GetComponent<MuzzleFlash>().ps.Play();
		muzzleTimer = 0.3f;
		this.shotTimer = this.shotDelay;
		if (!PlayerScript.powerUpBase[PlayerScript.PowerUps.infiniteAmmo]) --this.currentAmmoCount;
		RaycastHit hit;
		soundSource.Play();
		bool hitTheEnemy = Physics.Raycast(this.shootPoint.position, Camera.main.transform.forward * this.range, out hit, this.range);
	//	Debug.DrawRay(this.shootPoint.position, Camera.main.transform.forward * this.range, Color.red, this.range);
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
		if (currentAmmoCount == 0 && ammoCount == 0) return;
		reloadSound.Play();
		reloadTimer = reloadDelay;
		if (ammoCount < magCapacity) {
			if (currentAmmoCount < magCapacity) {
				int temp = currentAmmoCount + ammoCount;
				if (temp > magCapacity) {
					temp -= magCapacity;
					ammoCount = temp;
					currentAmmoCount = magCapacity;
					return;
				} else {
					currentAmmoCount += ammoCount;
					ammoCount = 0;
					return;
				}
			}
		}
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
