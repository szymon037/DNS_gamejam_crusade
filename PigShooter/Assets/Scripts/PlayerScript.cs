using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public enum PowerUps {
		speedUp = 0,
		freeze
	}

	public float health = 0f;
	public Dictionary<PowerUps, bool> powerUpBase = new Dictionary<PowerUps, bool>();
	public float freezeTimer = 0f;
	public float speedUpTimer = 0f;

	void Start() {
		FillPowerUpDictionary();
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
