using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour, IPoolable
{
    #region IPoolable
    public void OnPoolOut()
    {
        gameObject.SetActive(true);
        _collider.enabled = true;
    }

    public void OnPoolEnter()
    {
        PoolManager.AddObjToPool<HitBox>("HitBox", this);
        _collider.enabled = false;
        gameObject.SetActive(false);
    }
    #endregion

    private Collider _collider = null;
    private Vector3 _size = Vector3.zero;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
    }
}
