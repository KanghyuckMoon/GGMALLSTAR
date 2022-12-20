using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Setting
{
    public class InGameSettingInput : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("canvas")]
        private GameObject _canvas;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
			{
                _canvas.SetActive(!_canvas.activeSelf);
            }
        }
    }

}