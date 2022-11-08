using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalMove : MonoBehaviour
{
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        var angle = transform.eulerAngles;
        if (Input.GetKey(KeyCode.A))
		{
            angle.y += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            angle.y -= Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            angle.x += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            angle.x -= Time.deltaTime * speed;
        }
        transform.eulerAngles = angle;
    }
}
