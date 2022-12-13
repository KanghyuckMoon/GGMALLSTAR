using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{

    public class EffectObject : MonoBehaviour
    {
        [SerializeField]
        private float _duration;

        private void OnEnable()
        {
            StartCoroutine(Delete());
        }
        private IEnumerator Delete()
        {
            yield return new WaitForSeconds(_duration);
            gameObject.SetActive(false);
        }

		private void OnDestroy()
		{
            Debug.LogError("Object Delete");
		}

	}

}