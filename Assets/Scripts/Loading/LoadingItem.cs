using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingItem : MonoBehaviour
{
	public float speed = 1.0f;
	public GameObject[] randomItems;
	private Vector3 rotation = Vector3.zero;


	private void Start()
	{
		SetRandomItem();
	}

	private void SetRandomItem()
	{
		int random = Random.Range(0, randomItems.Length);
		randomItems[random].SetActive(true);
	}

	private void Update()
	{
		rotation.y += Time.deltaTime * speed;
		transform.eulerAngles = rotation;
	}
}
