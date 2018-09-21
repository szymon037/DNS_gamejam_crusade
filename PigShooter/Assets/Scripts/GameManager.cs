using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject[] enemySpawnPoints = null;
	public GameObject[] powerUpSpawnPoints = null;
	public List<GameObject> pigsToSpawn = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
		powerUpSpawnPoints = GameObject.FindGameObjectsWithTag("PowerUpSpawnPoint");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
