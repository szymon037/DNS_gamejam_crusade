using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
	public GameObject currentSprite = null;
	public List<GameObject> flashSprites = new List<GameObject>();
	// Use this for initialization
	void Start () {
		this.shotDelay = 0.25f;
		cameraTransform = cameraToShake.transform;
		soundSource.volume = 0.6f;
		reloadSound.volume = 0.9f;	
		int tempIt = 0;
		foreach (Transform child in muzzleFlash.transform) {
			flashSprites.Add(child.gameObject);
			flashSprites[tempIt++].SetActive(false);
		}

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
			if (currentSprite != null) {
				currentSprite.SetActive(false);
			}
		}
	}

	public override void Shoot() {
		if (reloadTimer > 0f) return;
		if (shotTimer > 0f) return;
		muzzleFlash.gameObject.GetComponent<MuzzleFlash>().ps.Play();
		muzzleTimer = 0.3f;
		this.shotTimer = this.shotDelay;
		--this.currentAmmoCount;
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
