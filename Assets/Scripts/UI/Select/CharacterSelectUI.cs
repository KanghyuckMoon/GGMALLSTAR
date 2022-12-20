using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class CharacterSelectUI : MonoBehaviour
    {
        [SerializeField]
        private CharacterDataBaseSO _characterDataBaseSO;

        [SerializeField]
        private GameObject _characterSelectBBlockPrefab;

        private void Start()
        {
            for (int i = 0; i < _characterDataBaseSO.CharacterScriptableObjects.Length; i++)
            {
                GameObject obj = Instantiate(_characterSelectBBlockPrefab, transform);
                obj.GetComponent<Image>().sprite = _characterDataBaseSO.CharacterScriptableObjects[i].CharacterImage;
            }
        }
    }
}