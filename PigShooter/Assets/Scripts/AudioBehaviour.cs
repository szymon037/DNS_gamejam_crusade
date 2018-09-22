using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour {

	public AudioSource[] sounds = null;
	void Start() {
		sounds = GetComponentsInChildren<AudioSource>();
	}
}
