using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class PoolObj : MonoBehaviour, IPoolable
    {
        [ContextMenu("OnPoolEnter")]
        public void OnPoolEnter()
        {
            gameObject.SetActive(false);
            PoolManager.AddObjToPool<PoolObj>("PoolObj", this);
        }

        public void OnPoolOut()
        {
            gameObject.SetActive(true);
            StartCoroutine(Disable());
        }

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(1f);
            OnPoolEnter();
        }
    }

}