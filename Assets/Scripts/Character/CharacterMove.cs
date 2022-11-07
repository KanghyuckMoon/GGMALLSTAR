using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public void Move()
    {
        Debug.Log("Move");
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            transform.Translate(Vector3.right * horizontal * Time.deltaTime * 10);
        }
    }
}
