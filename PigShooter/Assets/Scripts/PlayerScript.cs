﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour {

	public enum PowerUps {
		speedUp = 0,
		freeze
	}

	public float maxHealth = 100.0f;
	public float health = 100.0f;
	public Dictionary<PowerUps, bool> powerUpBase = new Dictionary<PowerUps, bool>();
	public float freezeTimer = 0f;
	public float speedUpTimer = 0f;
	public Transform startPoint = null;
	private float fillAmount = 1f;
	public Image healthImage = null;
	public Text ammoCountInMagazine = null;
	public Text ammoCountTotal = null;
	public Text slash = null;
	public WeaponBehaviour script = null;
	private Color blackColor = Color.black;
	private Color redColor = Color.red;


	void Start() {
		FillPowerUpDictionary();
		this.transform.position = startPoint.position;
		script = GetComponent<WeaponBehaviour>();
		this.health = this.maxHealth;
	}

	void Update() {
		if (speedUpTimer > 0f) {
			speedUpTimer -= Time.deltaTime;
		} else {
			powerUpBase[PowerUps.speedUp] = false;
		}
		if (freezeTimer > 0f) {
			freezeTimer -= Time.deltaTime;
		} else {
			powerUpBase[PowerUps.freeze] = false;
		}

		fillAmount = health / maxHealth;
		healthImage.fillAmount = fillAmount; 

		if (ammoCountTotal != null && script != null && script.activeWeaponScript != null) {
			ammoCountTotal.text = script.activeWeaponScript.ammoCount.ToString();
		}

		if (ammoCountInMagazine != null && script != null && script.activeWeaponScript != null) {
			ammoCountInMagazine.text = script.activeWeaponScript.currentAmmoCount.ToString();
		}

		if(script.activeWeaponScript.ammoCount == 0){
			ammoCountTotal.color = redColor;

			if(script.activeWeaponScript.currentAmmoCount == 0)
			{
				ammoCountInMagazine.color = redColor;
				slash.color = redColor;
			}
		}

		
	
		else
		{
			ammoCountTotal.color = blackColor;
			ammoCountInMagazine.color = blackColor;
			slash.color = blackColor;
		}


		if (this.health > this.maxHealth) this.health = this.maxHealth;
	}

	public void FillPowerUpDictionary() {
		PowerUps[] powerUps = (PowerUps[])System.Enum.GetValues(typeof(PowerUps));
		powerUps.ToList().ForEach(powerUp => powerUpBase.Add(powerUp, false));
	}

	public void EnablePowerUpEffect(PowerUps id) {
		switch (id) {
			case PowerUps.speedUp:
				speedUpTimer = 10f;
				powerUpBase[id] = true;
				break;
			case PowerUps.freeze:
				freezeTimer = 4f;
				powerUpBase[id] = true;
				break;
		}
	}
}
