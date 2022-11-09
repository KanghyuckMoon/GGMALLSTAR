using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
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
