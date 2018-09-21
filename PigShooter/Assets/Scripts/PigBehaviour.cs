using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehaviour : MonoBehaviour {

	public GameObject player = null;
	public PlayerScript scriptRef = null;
	public float speed = 0f;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		scriptRef = player.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!scriptRef.powerUpBase[PlayerScript.PowerUps.freeze]) {
			transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
			transform.LookAt(player.transform);
			transform.rotation = Quaternion.Euler( new Vector3(-90f, transform.eulerAngles.y, transform.eulerAngles.z));
			transform.rotation = Quaternion.Euler( new Vector3(-90f, transform.eulerAngles.y + 90f, transform.eulerAngles.z));
		}
	}


}
