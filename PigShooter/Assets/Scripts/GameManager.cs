using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

	//public GameObject[] enemySpawnPoints = null;
	public GameObject[] powerUpSpawnPoints = null;
	public Transform player = null;
	public List<GameObject> pigsToSpawn = new List<GameObject>();
	//public List<GameObject> spawnedPigs = new List<GameObject>();
	public static int enemyCounter = 0;
	public int enemyAmount = 0;
	public int levelTag = 1;
	public int minEnemyAmount = 10;
	public int minPowerUpAmount = 3;
	public bool killedAllEnemies = false;
	// Use this for initialization
	void Awake () {
		//enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
		powerUpSpawnPoints = GameObject.FindGameObjectsWithTag("PowerUpSpawnPoint");
		player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnEnemies() {
		enemyAmount = Random.Range((minEnemyAmount * levelTag) / 2 + 3, minEnemyAmount * levelTag);
		for (int i = 0; i < enemyAmount; ++i) {
			int pigIndex = Random.Range(0, pigsToSpawn.Count);
			float x, z;
			x = Random.Range(player.position.x - 30f, player.position.x + 30f);
			z = Random.Range(player.position.z - 30f, player.position.z + 30f);
			Instantiate(pigsToSpawn[pigIndex], new Vector3(x, 0f, z), Quaternion.identity);
		}
		enemyCounter = enemyAmount;
	}

	void SpawnPowerUps() {
		if (powerUpSpawnPoints.Length <= 0) return;
		int amount = Random.Range(minPowerUpAmount, minPowerUpAmount * 2);
		PlayerScript.PowerUps[] powerUps = (PlayerScript.PowerUps[])(System.Enum.GetValues(typeof(PlayerScript.PowerUps)));
		for (int i = 0; i < amount; ++i) {
			retry:
			int powerUpIndex = Random.Range(0, powerUps.Length);
			int slotIndex = Random.Range(0, powerUpSpawnPoints.Length);
			if (powerUpSpawnPoints[slotIndex].transform.GetChild(0) != null) {
				goto retry;
			}
			switch (powerUps[powerUpIndex]) {
				case PlayerScript.PowerUps.speedUp:
					break;
				case PlayerScript.PowerUps.freeze:
					break;
			}
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
