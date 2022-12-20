using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class ItemBoxSpawner : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("itemBoxObj")]
        private GameObject _itemBoxObj;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("SpawnItem", Random.Range(20f, 50f));
        }

        private void SpawnItem()
        {
            _itemBoxObj.gameObject.SetActive(true);
        }
    }

}