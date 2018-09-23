using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour {

	public enum PowerUps {
		speedUp = 0,
		freeze,
		infiniteAmmo
	}

	public float maxHealth = 100.0f;
	public float health = 100.0f;
	public static Dictionary<PowerUps, bool> powerUpBase = new Dictionary<PowerUps, bool>();
	public float freezeTimer = 0f;
	public float infiniteAmmoTimer = 0f;
	public float speedUpTimer = 0f;
	public Transform startPoint = null;
	public GameObject gamemanager = null;
	private float fillAmount = 1f;
	public Image healthImage = null;
	public Text ammoCountInMagazine = null;
	public Text ammoCountTotal = null;
	public Text slash = null;
	public Text points = null;
	public WeaponBehaviour script = null;
	private Color blackColor = Color.black;
	private Color redColor = Color.red;
	public bool isHit = false;
	public float hitTimer = 0f;
	public static int playerScore = 0;
	private bool reducedSpeed = false;
	public AudioSource OOF = null;


	void Start() {
		FillPowerUpDictionary();
		this.transform.position = startPoint.position;
		script = GetComponent<WeaponBehaviour>();
		this.health = this.maxHealth;
		OOF = GetComponent<AudioSource>();
	}

	void Update() {
		if (speedUpTimer > 0f) {
			speedUpTimer -= Time.deltaTime;
		} else {
			powerUpBase[PowerUps.speedUp] = false;
			if (!reducedSpeed) {
				GetComponent<PlayerMovement>().speed -= 2f;
				reducedSpeed = true;
			}
		}
		if (freezeTimer > 0f) {
			freezeTimer -= Time.deltaTime;
		} else {
			powerUpBase[PowerUps.freeze] = false;
		}
		if (infiniteAmmoTimer > 0f) {
			infiniteAmmoTimer -= Time.deltaTime;
		} else {
			powerUpBase[PowerUps.infiniteAmmo] = false;
		}
		if (hitTimer > 0f) {
			hitTimer -= Time.deltaTime;
		} else {
			isHit = false;
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
			ammoCountTotal.color = Color.white;
			ammoCountInMagazine.color = Color.white;
			slash.color = Color.white;
		}

		points.text = playerScore.ToString();

		if (this.health > this.maxHealth) this.health = this.maxHealth;
	}

	public void FillPowerUpDictionary() {
		(System.Enum.GetValues(typeof(PowerUps)) as PowerUps[]).ToList().ForEach(powerUp => powerUpBase.Add(powerUp, false));
	}

	public void EnablePowerUpEffect(PowerUps id) {
		switch (id) {
			case PowerUps.speedUp:
				speedUpTimer = 10f;
				GetComponent<PlayerMovement>().speed += 2f;
				reducedSpeed = false;
				powerUpBase[id] = true;
				break;
			case PowerUps.freeze:
				freezeTimer = 4f;
				powerUpBase[id] = true;
				break;
			case PowerUps.infiniteAmmo:
				infiniteAmmoTimer = 10f;
				WeaponBehaviour script = gameObject.GetComponent<WeaponBehaviour>();
				script.activeWeaponScript.currentAmmoCount = script.activeWeaponScript.magCapacity;
				powerUpBase[id] = true;
				break;
		}
	}

	public void Die() {
		GameManager.SavePoints();
		DontDestroyOnLoad(gamemanager);
		UnityEngine.SceneManagement.SceneManager.LoadScene("LostGame");
	}
}
