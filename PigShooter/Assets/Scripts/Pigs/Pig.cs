﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

	public float health = 0f;
	public GameObject bacon = null;
	public int scoreValue = 0;
	public float damage = 0f;
	public AudioSource pigSqueal = null;
	public bool dead = false;
	private Animator anim;

	void Awake() {
		//float scale = Random.Range(3f, 4.5f);
		//this.transform.localScale = new Vector3(scale, scale, scale);

		anim = GetComponent<Animator>();
	}

	void Update() {
		if (health <= 0f) {
			Die();
		}

		anim.SetBool("isDead", dead);
	}

	public void ReduceHealth(float value) {
		this.health -= value;
	}

	public void Die() {
		int chance = (int)(Random.value * 100f);
		if (chance <= 10) {
			int amountOfBacon = Random.Range(1, 4);
			for (int i = 0; i < amountOfBacon; ++i) {
				GameObject temp = Instantiate(bacon, this.transform.position, Quaternion.identity) as GameObject;
				temp.transform.eulerAngles = new Vector3(temp.transform.position.x + Random.Range(0, 180f), temp.transform.position.y + Random.Range(0, 180f), temp.transform.position.z + Random.Range(0, 180f));
			}
		}
		--GameManager.enemyCounter;
		PlayerScript.playerScore += this.scoreValue;
		Destroy(this.gameObject);
		dead = true;
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			PlayerScript reference = other.gameObject.GetComponent<PlayerScript>();
			if (reference.isHit == false) {
				reference.health -= this.damage;
				reference.hitTimer = 0.3f;
				reference.OOF.Play();
				reference.isHit = true;
			}
			pigSqueal.Play();
		}
	}
}
