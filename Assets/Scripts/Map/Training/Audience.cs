using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 관중 연출에 사용
/// </summary>
public class Audience : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("currentPosY")] 
    private float _currentPosY = 0.0f;
    [SerializeField, FormerlySerializedAs("power")] 
    private float _power = 1.0f;
    [SerializeField, FormerlySerializedAs("speed")] 
    private float _speed = 1.0f;

    private float _originPosY = 0.0f;
    private float _time = 0.0f;

    private void Start()
    {
        _originPosY = transform.position.y;
    }

    void Update()
    {
        _time += Time.deltaTime * _speed;
        Vector3 pos = transform.position;
        pos.y = _originPosY + (Mathf.Sin(_time + _currentPosY) * _power);
        transform.position = pos;
    }
}
