using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon {
	// Use this for initialization
	public AudioSource[] sounds = null;
	public Transform reloadSoundHub = null;
	private int i = 0;
	void Start () {
		this.shotDelay = 0.1f;
		cameraTransform = cameraToShake.transform;
		sounds = gameObject.GetComponentsInChildren<AudioSource>();
		foreach (AudioSource source in sounds) {
			source.clip = soundSource.clip;
			source.volume = 0.35f;
		}
		reloadSound = reloadSoundHub.gameObject.GetComponent<AudioSource>();
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
		else {
		}
	}

	public override void Shoot() {
		if (reloadTimer > 0f) return;
		if (shotTimer > 0f) return;
		muzzleTimer = 0.3f;
		muzzleFlash.gameObject.GetComponent<MuzzleFlash>().ps.Play();
		if (i >= sounds.Length) i = 0;
		sounds[i++].Play();
		this.shotTimer = this.shotDelay;
		--this.currentAmmoCount;
		RaycastHit hit;
		bool hitTheEnemy = Physics.Raycast(this.shootPoint.position, Camera.main.transform.forward * this.range, out hit, this.range);
		//Debug.DrawRay(this.shootPoint.position, Camera.main.transform.forward * this.range, Color.red, this.range);
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
		reloadSound.Play();
		i = 0;
		Debug.Log("Reloading");
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
