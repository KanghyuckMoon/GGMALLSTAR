using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class PedestalMove : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("rotation")]
        private Vector3 _rotation = Vector3.zero;
        [SerializeField, FormerlySerializedAs("speed")]
        private float _speed = 1.0f;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rotation.y += Time.deltaTime * _speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rotation.y -= Time.deltaTime * _speed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                _rotation.x += Time.deltaTime * _speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _rotation.x -= Time.deltaTime * _speed;
            }
            transform.eulerAngles = _rotation;
        }
    }

}