using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	public float speed = 1.0f;
	private Vector3 rotation = Vector3.zero;
	public void Update()
	{
		rotation.y += Time.deltaTime * speed;
		transform.eulerAngles = rotation;
	}
}
