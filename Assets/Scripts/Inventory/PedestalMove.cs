using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalMove : MonoBehaviour
{
    public Vector3 rotation = Vector3.zero;
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
		{
            rotation.y += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotation.y -= Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rotation.x += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotation.x -= Time.deltaTime * speed;
        }
        transform.eulerAngles = rotation;
    }
}
