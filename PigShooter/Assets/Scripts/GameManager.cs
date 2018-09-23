using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour {

	//public GameObject[] enemySpawnPoints = null;
//	public GameObject[] powerUpSpawnPoints = null;
	public GameObject[] ammoPrefabs = null;
	public GameObject[] powerUpPrefabs = null;
	public Transform player = null;
	public List<GameObject> pigsToSpawn = new List<GameObject>();
	//public List<GameObject> spawnedPigs = new List<GameObject>();
	public static int enemyCounter = 0;
	public int enemyAmount = 0;
	public int levelTag = 1;
	public int minEnemyAmount = 4;
	public int minPowerUpAmount = 3;
	public bool killedAllEnemies = false;
	public int ammoPickUpAmount = 0;
	public float powerUpTimer = 0f;
	public float enemyTimer = 0f;
	public static float startTimer = 4f;
	private PlayerScript healthRef = null;
	public Text startText = null;
	// Use this for initialization
	void Awake () {
		//enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
		//powerUpSpawnPoints = GameObject.FindGameObjectsWithTag("PowerUpSpawnPoint");
		player = GameObject.FindWithTag("Player").transform;
		StartLevel();
		healthRef = player.gameObject.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		// if (enemyAmount <= 0) {
		// 	++levelTag;
		// 	StartLevel();
		// }
		if (startTimer > 0f) startTimer -= Time.deltaTime;
		if (powerUpTimer > 0f) {
			powerUpTimer -= Time.deltaTime;
		} else {
			int choice = Random.Range(0, 2);
			if (choice == 0) {
				Instantiate(ammoPrefabs[Random.Range(0, ammoPrefabs.Length)], new Vector3(Random.Range(player.position.x - 30f, player.position.x + 30f), 1f, Random.Range(player.position.z - 30f, player.position.z + 30f)), Quaternion.identity);
			} else {
				int temp = Random.Range(0, powerUpPrefabs.Length);
				Instantiate(powerUpPrefabs[temp], new Vector3(Random.Range(player.position.x - 30f, player.position.x + 30f), 1f, Random.Range(player.position.z - 30f, player.position.z + 30f)), Quaternion.identity);
				powerUpTimer = (float)Random.Range(7, 13);
			}
		}
		if (enemyTimer > 0f) {
			enemyTimer -= Time.deltaTime;
		} else {
			int temp = Random.Range(0, pigsToSpawn.Count);
			Instantiate(pigsToSpawn[temp], new Vector3(Random.Range(player.position.x - 30f, player.position.x + 30f), 1f, Random.Range(player.position.z - 30f, player.position.z + 30f)), Quaternion.identity);
			enemyTimer = (float)Random.Range(3, 10);
			++enemyCounter;
		}
		if (healthRef.health <= 0f) {
			UnityEngine.SceneManagement.SceneManager.LoadScene("LostGame");
		}
	}

	void StartLevel() {
		SpawnEnemies();
		SpawnPowerUps();
		SpawnAmmo();
	}

	void SpawnEnemies() {
		enemyAmount = Random.Range((minEnemyAmount * levelTag) / 2 + 3, minEnemyAmount * levelTag);
		for (int i = 0; i < enemyAmount; ++i) {
			int pigIndex = Random.Range(0, pigsToSpawn.Count);
			float x, z;
			x = Random.Range(player.position.x - 30f / 2f, player.position.x + 30f / 2f);
			z = Random.Range(player.position.z - 30f / 2f, player.position.z + 30f / 2f);
			Instantiate(pigsToSpawn[pigIndex], new Vector3(x, 1f, z), Quaternion.identity);
		}
		enemyCounter = enemyAmount;
		enemyTimer = (float)Random.Range(5, 10);
	}

	void SpawnPowerUps() {
		int amount = Random.Range(minPowerUpAmount, minPowerUpAmount * 2);
		//PlayerScript.PowerUps[] powerUps = (PlayerScript.PowerUps[])(System.Enum.GetValues(typeof(PlayerScript.PowerUps)));
		for (int i = 0; i < amount; ++i) {
			int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
			float x, z;
			x = Random.Range(player.position.x - 30f, player.position.x + 30f);
			z = Random.Range(player.position.x - 30f, player.position.x + 30f);
			Instantiate(powerUpPrefabs[powerUpIndex], new Vector3(x, 1f, z), Quaternion.identity);
		}
		powerUpTimer = (float)Random.Range(5, 10);
	}

	void SpawnAmmo() {
		if (ammoPrefabs.Length <= 0) return;
		int amount = Random.Range(ammoPickUpAmount, ammoPickUpAmount * levelTag + 3);
		for (int i = 0; i < amount; ++i) {
			int ammoIndex = Random.Range(0, ammoPrefabs.Length);
			float x, z;
			x = Random.Range(player.position.x - 15f, player.position.x + 15f);
			z = Random.Range(player.position.z - 15f, player.position.z + 15f);
			Instantiate(ammoPrefabs[ammoIndex], new Vector3(x, 1f, z), Quaternion.identity);
		}
	}

	public static void SavePoints() {
		FileStream fs = new FileStream("score.txt", FileMode.OpenOrCreate);
		StreamReader read = new StreamReader(fs);
		int score = System.Convert.ToInt32(read.ReadLine());
		read.Close();
		if (score > PlayerScript.playerScore) {
			fs.Seek(0, SeekOrigin.Begin);
			using (StreamWriter writer = new StreamWriter(fs)) {
				writer.WriteLine(PlayerScript.playerScore.ToString());
			}
		}
		fs.Close();
	}
	
}
