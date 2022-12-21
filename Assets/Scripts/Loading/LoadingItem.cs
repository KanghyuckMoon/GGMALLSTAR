using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Loading
{
    public class LoadingItem : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("speed")]
        private float _speed = 1.0f;

        [SerializeField]
        private GameObject[] _randomItems;
        private Vector3 _rotation = Vector3.zero;


        private void Start()
        {
            SetRandomItem();
        }

        private void SetRandomItem()
        {
            int random = Random.Range(0, _randomItems.Length);
            _randomItems[random].SetActive(true);
        }

        private void Update()
        {
            _rotation.y += Time.deltaTime * _speed;
            transform.eulerAngles = _rotation;
        }
    }
}

