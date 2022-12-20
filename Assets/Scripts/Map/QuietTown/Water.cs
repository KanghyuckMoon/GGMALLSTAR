using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Water : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("currentPosY")] 
    private float _currentPosY = 0.0f;
    [SerializeField, FormerlySerializedAs("power")] 
    private float _power = 1.0f;

    private float _originPosY = 0.0f;

	private void Start()
	{
        _originPosY = transform.position.y;
	}

	void Update()
    {
        Vector3 pos = transform.position;
        pos.y = _originPosY + (Mathf.Sin(Time.time + _currentPosY)* _power);
        transform.position = pos;
    }
}
