using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Setting
{
    public class InGameSettingInput : MonoBehaviour
    {
        public GameObject canvas;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
			{
                canvas.SetActive(!canvas.activeSelf);
            }
        }
    }

}