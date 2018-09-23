using System.Linq;
using UnityEngine;

public class AmmoRecovery : MonoBehaviour {

	public int restore = 0;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<WeaponBehaviour>().weaponObjects.ForEach(weapon => weapon.GetComponent<Weapon>().ammoCount += restore);
			Destroy(this.gameObject);
		}
	}
}
