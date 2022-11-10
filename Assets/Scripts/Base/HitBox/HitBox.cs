using System.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    private Collider _collider = null;
    private Vector3 _size = Vector3.zero;
    private GameObject _owner = null;

    public GameObject Owner { get => _owner; set => _owner = value; }

    private Action _onHit = null;
    public Action OnHit { get => _onHit; set => _onHit = value; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_owner.tag))
        {
            OnHit?.Invoke();
            other?.GetComponent<Character>()?.CharacterEvent?.EventTrigger(EventKeyWord.DAMAGE);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SetActiveFalse());
    }

    private void OnDisable()
    {

    }

    private IEnumerator SetActiveFalse()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        PoolManager.AddObjToPool("HitBox", gameObject);
    }
}
