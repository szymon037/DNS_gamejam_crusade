using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifleammo : MonoBehaviour {

	public int minAmmoRestore = 0;
	public int maxAmmoRestore = 0;
	public GameObject player = null;
	public WeaponBehaviour weaponScript = null;
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		weaponScript = player.GetComponent<WeaponBehaviour>();
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			weaponScript.weaponScripts[1].ammoCount += Random.Range(minAmmoRestore, maxAmmoRestore);
			Destroy(this.gameObject);
		}
	}
}
