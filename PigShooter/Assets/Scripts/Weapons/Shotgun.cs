using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

	public int minPellets = 0;
	public int maxPellets = 0;
	public float spreadValue = 0.5f;
	// Use this for initialization
	void Start () {
		this.shotDelay = 1.5f;
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
		if (muzzleTimer > 0f) muzzleTimer -= Time.deltaTime;
		else {

		}
	}

	public override void Shoot() {
		if (reloadTimer > 0f) return;
		if (shotTimer > 0f) return;
		this.shotTimer = this.shotDelay;
		soundSource.Play();
		muzzleFlash.gameObject.GetComponent<MuzzleFlash>().ps.Play();
		//RaycastHit hit = Physics.Raycast();
		if (this.gameObject.activeSelf) soundSource.Play();
		--this.currentAmmoCount;
		int pelletCount = Random.Range(minPellets, maxPellets);
		RaycastHit hit;
		bool enemyIsHit = false;
		Vector3 camPosition = Camera.main.transform.forward;
		for (int i = 0; i < pelletCount; ++i) {
			Vector3 direction = new Vector3(camPosition.x + Random.Range(0, spreadValue),camPosition.y + Random.Range(0, spreadValue), camPosition.z + Random.Range(0, spreadValue)) * this.range;
			
			enemyIsHit = Physics.Raycast(this.shootPoint.position,direction.normalized, out hit, this.range);
			//Debug.Log("pellet direction: " + direction.normalized.ToString());
			if (hit.transform != null) {
				if (hit.transform.gameObject.CompareTag("Pig")) {
					//Debug.Log(hit.transform.gameObject.tag);
					hit.transform.gameObject.GetComponent<Pig>().ReduceHealth(this.damage);
				}
			}
			//Debug.DrawRay(this.shootPoint.position, direction.normalized * this.range, Color.red, 20f);
		}
		Shake();
	}

	public override void Reload() {
		if (currentAmmoCount == magCapacity) return;
		Debug.Log("Reloading");
		reloadSound.Play();
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
