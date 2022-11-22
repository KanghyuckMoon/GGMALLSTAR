using System.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    private BoxCollider _boxCollider = null;

    private CharacterAttack _owner = null;
    public CharacterAttack Owner { get => _owner; set => _owner = value; }

    private Action _onHit = null;
    public Action OnHit { get => _onHit; set => _onHit = value; }

    public HitBoxData hitBoxData;
    public void SetHitBox(HitBoxData hitBoxData, CharacterAttack owner, Action onHit, Vector3 size = default, Vector3 offset = default)
    {
        _owner = owner;
        _onHit = onHit;

        this.hitBoxData = hitBoxData;

        transform.position = owner.Character.transform.position + offset;
        _boxCollider.size = size;
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_owner.Character.tag))
        {
            _owner.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>();
            _owner.TargetCharacterDamage.OnAttcked(hitBoxData, other.ClosestPoint(transform.position), Owner.IsRight);
            OnHit?.Invoke();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SetActiveCoroutine());
    }

    private void OnDisable()
    {
        _onHit = null;
        _owner = null;
    }

    private IEnumerator SetActiveCoroutine(bool active = false)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(active);
        PoolManager.AddObjToPool("HitBox", gameObject);
    }
}
