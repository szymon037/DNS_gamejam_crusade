using System.Collections;
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
	public Text ammoCount = null;
	public WeaponBehaviour script = null;


	void Start() {
		FillPowerUpDictionary();
		this.transform.position = startPoint.position;
		script = GetComponent<WeaponBehaviour>();

	

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

		ammoCount.text = script.activeWeaponScript.currentAmmoCount.ToString();

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
