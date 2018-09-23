using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	// Use this for initialization
	void Awake () {
		//enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
		//powerUpSpawnPoints = GameObject.FindGameObjectsWithTag("PowerUpSpawnPoint");
		player = GameObject.FindWithTag("Player").transform;
		StartLevel();

	}
	
	// Update is called once per frame
	void Update () {
		// if (enemyAmount <= 0) {
		// 	++levelTag;
		// 	StartLevel();
		// }
	}

	void StartLevel() {
		//SpawnEnemies();
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
