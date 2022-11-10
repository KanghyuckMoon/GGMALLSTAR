using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotShowUI : MonoBehaviour
{
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
		{
            canvas.SetActive(!canvas.activeSelf);
        }
    }
}
