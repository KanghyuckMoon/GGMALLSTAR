using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class NotShowUI : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("canvas")]
        private GameObject _canvas;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _canvas.SetActive(!_canvas.activeSelf);
            }
        }
    }
}
