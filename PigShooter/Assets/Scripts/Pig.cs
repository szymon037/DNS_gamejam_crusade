using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

	public float speed = 0f;
	public float health = 0f;

	public void ReduceHealth(float value) {
		this.health -= value;
	}
}
