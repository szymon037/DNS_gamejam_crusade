using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//public gameObject player = null;

	//private Vector3 velocity = Vector3.zero;
	public float speed = 1.0f;
	public float currentSpeed = 0f;
	private float posY = 0f;
	public float period = 2.0f;
	public float amplitude = 0.1f;
	private float time = 0f;
	public bool isMoving = false;
	private float move = 0;
	private float oldMove = 0;
	private GameObject player = null;
	public Transform startPoint = null;

	void Start () {
		posY = this.transform.position.y;
		currentSpeed = speed;
		player = GameObject.FindGameObjectWithTag("Player");
		this.transform.position = startPoint.position;
	}
	
	void Update () {
		move = this.transform.position.x*this.transform.position.x + this.transform.position.z*this.transform.position.z;
		if(move != oldMove)
			isMoving = true; 

		else isMoving = false;

			oldMove = move;

		player.transform.position = this.transform.position;
		time += Time.deltaTime;
		if (Input.GetKey(KeyCode.W)){
			this.transform.position += Vector3.forward * Time.deltaTime * currentSpeed;
		}

		else if(Input.GetKey(KeyCode.S)){
			this.transform.position += Vector3.back * Time.deltaTime* currentSpeed;
		}


		if(Input.GetKey(KeyCode.A)){
			this.transform.position += Vector3.left * Time.deltaTime* currentSpeed;
		}

		else if(Input.GetKey(KeyCode.D)){
			this.transform.position += Vector3.right * Time.deltaTime* currentSpeed;
		}

		if(isMoving)
			transform.position = new Vector3(this.transform.position.x , (posY + amplitude * Mathf.Sin(period * time)), this.transform.position.z);
	}
}
