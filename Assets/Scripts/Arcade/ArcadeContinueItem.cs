using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeContinueItem : MonoBehaviour
{
	public float speed = 1.0f;
	public GameObject[] randomItems;
	private Vector3 rotation = Vector3.zero;


	private void Start()
	{
		SetItem();
	}

	private void SetItem()
	{
		randomItems[(int)SelectDataSO.characterSelectP1 - 1].SetActive(true);
	}

	private void Update()
	{
		rotation.y += Time.deltaTime * speed;
		rotation.x += Time.deltaTime * speed;
		rotation.z += Time.deltaTime * speed;
		transform.eulerAngles = rotation;
	}
}
