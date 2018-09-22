using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour {

	public List<GameObject> weaponObjects = new List<GameObject>();
	public GameObject activeWeapon = null;
	public List<Weapon> weaponScripts = new List<Weapon>();
	public Weapon activeWeaponScript = null;
	private int index = 0;
	private bool isShooting = false;
	public Transform canvas = null;
	public bool switchedWeapon = false;
	// Use this for initialization
	void Start () {
		//sounds = canvas.gameObject.GetComponentsInChildren<AudioSource>();
		weaponObjects.ForEach(obj => weaponScripts.Add(obj.GetComponent<Weapon>()));
		weaponObjects.ForEach(obj => obj.SetActive(false));
		activeWeapon = weaponObjects[index];
		activeWeaponScript = weaponScripts[index];
		activeWeapon.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && activeWeaponScript.shotTimer <= 0f) {
			if (activeWeaponScript.currentAmmoCount > 0) {
				activeWeaponScript.Shoot();
			}
			else activeWeaponScript.Reload();	
		} 
		if (Input.GetKeyDown(KeyCode.Alpha1) && activeWeapon != weaponObjects[0]) {
			activeWeapon.SetActive(false);
			activeWeapon = weaponObjects[0];
			activeWeapon.SetActive(true);
			activeWeaponScript = weaponScripts[0];
			index = 0;
			switchedWeapon = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && activeWeapon != weaponObjects[1]) {
			activeWeapon.SetActive(false);
			activeWeapon = weaponObjects[1];
			activeWeapon.SetActive(true);
			activeWeaponScript = weaponScripts[1];
			index = 1;
			switchedWeapon = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && activeWeapon != weaponObjects[2]) {
			activeWeapon.SetActive(false);
			activeWeapon = weaponObjects[2];
			activeWeapon.SetActive(true);
			activeWeaponScript = weaponScripts[2];
			index = 2;
			switchedWeapon = true;
		}
		if (Input.GetKeyDown(KeyCode.R) && activeWeaponScript.currentAmmoCount != activeWeaponScript.magCapacity) {
			activeWeaponScript.Reload();
		}
	}
}
