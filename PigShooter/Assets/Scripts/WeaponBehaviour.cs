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
	public Text ammoCount = null;
	// Use this for initialization
	void Start () {
		weaponObjects.ForEach(obj => weaponScripts.Add(obj.GetComponent<Weapon>()));
		activeWeapon = weaponObjects[index];
		activeWeaponScript = weaponScripts[index];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && activeWeaponScript.shotTimer <= 0f) {
			activeWeaponScript.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Alpha1) && activeWeapon != weaponObjects[0]) {
			activeWeapon = weaponObjects[0];
			activeWeaponScript = weaponScripts[0];
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && activeWeapon != weaponObjects[1]) {
			activeWeapon = weaponObjects[1];
			activeWeaponScript = weaponScripts[1];
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && activeWeapon != weaponObjects[2]) {
			activeWeapon = weaponObjects[2];
			activeWeaponScript = weaponScripts[2];
		}
	}
}
