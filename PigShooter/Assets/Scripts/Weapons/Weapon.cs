using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public int magCapacity = 0;
	public int ammoCount = 0;
	public float shotDelay = 0;
	public float shotTimer = 0;

	public virtual void Shoot() {

	}
}
