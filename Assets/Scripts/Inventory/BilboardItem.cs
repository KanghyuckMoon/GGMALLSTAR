using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardItem : MonoBehaviour
{
	private Vector3 originRot;

	private void Start()
	{
		originRot = transform.eulerAngles;
	}

	private void LateUpdate()
	{
		transform.LookAt(Camera.main.transform);
		transform.eulerAngles += originRot;
	}
}
