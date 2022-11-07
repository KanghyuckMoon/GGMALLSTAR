using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    [SerializeField] private float currentPosY = 0.0f;
    [SerializeField] private float power = 1.0f;
    [SerializeField] private float speed = 1.0f;
    private float originPosY = 0.0f;
    private float time = 0.0f;

    private void Start()
    {
        originPosY = transform.position.y;
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        Vector3 pos = transform.position;
        pos.y = originPosY + (Mathf.Sin(time + currentPosY) * power);
        transform.position = pos;
    }
}
