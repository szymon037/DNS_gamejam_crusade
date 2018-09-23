using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MusicList : MonoBehaviour {

	public List<AudioClip> clips = new List<AudioClip>();
	private AudioSource playerSource;
	private int iterator = 0;
	void Start() {
		AudioSource[] tempArray = GetComponentsInChildren<AudioSource>();
		int temp = 0;
		foreach (AudioSource src in tempArray) {
			clips.Add(tempArray[temp++].clip);
		}
		playerSource = GetComponent<AudioSource>();
	}

	void Update() {
		if (!playerSource.isPlaying) {
			PlayAudio();
		}
	}

	void PlayAudio() {
		if (iterator >= clips.Count) iterator = 0;
		playerSource.clip = clips[iterator++];
		playerSource.Play();
		try {
			Invoke("PlayAudio1", clips[iterator - 1].length);
			} catch (System.Exception) {
				Debug.Log("wut");
				return;
			}
	}

	void PlayAudio1() {
		PlayAudio();
	}
}
