using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

	public ParticleSystem ps = null;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		Debug.LogWarning(ps);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
