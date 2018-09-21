using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
	public GameObject player = null;
	public float rotationSpeed = 0f;
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse X") != 0f) {
			player.transform.Rotate(0f, Input.GetAxis("Mouse X") * rotationSpeed, 0f);
		}
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
		if (Input.GetAxis("Mouse Y") != 0f) {
			transform.Rotate(-Input.GetAxis("Mouse Y") * rotationSpeed, 0f, 0f);
		}
	}
}
