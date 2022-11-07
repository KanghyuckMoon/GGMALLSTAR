using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float currentPosY = 0.0f;
    [SerializeField] private float power = 1.0f;
    private float originPosY = 0.0f;

	private void Start()
	{
        originPosY = transform.position.y;
	}

	void Update()
    {
        Vector3 pos = transform.position;
        pos.y = originPosY + (Mathf.Sin(Time.time + currentPosY)* power);
        transform.position = pos;
    }
}
