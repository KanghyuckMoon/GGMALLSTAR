using System.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill;

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
            Owner.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>();
            Owner.TargetCharacterDamage.OnAttcked(hitBoxData, other.ClosestPoint(transform.position), Owner.IsRight);

            Owner.Character.GetCharacterComponent<CharacterGravity>().SetHitTime(hitBoxData.hitTime);
            Vector3 vector = Owner.Character.Rigidbody.velocity;
            Owner.Character.Rigidbody.velocity = Vector3.zero;
            CharacterInput characterInput = Owner.Character.GetCharacterComponent<CharacterInput>();
            if (characterInput is not null)
            {
                characterInput.SetStunTime(hitBoxData.hitTime);
            }
            else
            {
                AITestInput aITestInput = Owner.Character.GetCharacterComponent<AITestInput>();
                if (aITestInput is not null)
                {
                    aITestInput.SetStunTime(hitBoxData.hitTime);
                }
            }
            StaticCoroutine.Instance.StartCoroutine(OwnerHitTimeEnd(Owner.Character, hitBoxData.hitTime, vector));

            OnHit?.Invoke();
        }
    }

    private IEnumerator OwnerHitTimeEnd(Character character, float hitTime, Vector3 vec)
	{
        yield return new WaitForSeconds(hitTime);
        if (character is not null)
		{
            character.Rigidbody.velocity = vec;
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
