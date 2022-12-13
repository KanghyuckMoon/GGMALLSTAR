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
        transform.SetParent(owner?.Character?.transform);
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

    public void OwnerHitTime(float hitTime)
    {
        if (!RoundManager.ReturnIsSetting())
		{
            return;
		}

        Owner.Character.GetCharacterComponent<CharacterGravity>(ComponentType.Gravity).SetHitTime(hitTime);
        Vector3 vector = Owner.Character.Rigidbody.velocity;
        Owner.Character.Rigidbody.velocity = Vector3.zero;
        CharacterInput characterInput = Owner.Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);
        
        // 캐릭터 경직
        if (characterInput is not null)
        {
            characterInput.SetStunTime(hitTime);
        }
        else
        {
            CharacterAIInput aITestInput = Owner.Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            if (aITestInput is not null)
            {
                aITestInput.SetStunTime(hitTime);
            }
        }

        CharacterAnimation characterAnimation = Owner.Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
        characterAnimation?.SetHitTime(hitTime);

        if (Owner.TargetCharacterDamage.Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
        {
            StaticCoroutine.Instance.StartCoroutine(OwnerHitTimeEnd(Owner.Character, hitTime, vector));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_owner != null && other.gameObject == _owner.Character.gameObject)
		{
            return;
		}

        if (!other.gameObject.CompareTag(_owner.Character.tag) && !other.gameObject.CompareTag("Invincibility") && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")))
        {
            Owner.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>(ComponentType.Damage);
            Owner.TargetCharacterDamage?.OnAttcked(this, hitBoxData, other.ClosestPoint(transform.position), Owner.IsRight);
            OnHit?.Invoke();

            Debug.Log(hitBoxData.knockBack);

            //AI
            CharacterAIInput aiInput = Owner.Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            if (aiInput is not null)
			{
                aiInput.IsHit(hitBoxData.actionName);
            }

            //Exp

            int expCount = (hitBoxData.addExp / 5) + 1;

            for (int i = 0; i < expCount; ++i)
            {
                StarEffect starEffect = PoolManager.GetItem("StarEff").GetComponent<StarEffect>();
                starEffect.SetEffect(transform.position, Owner.Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level), hitBoxData.addExp / expCount);
			}

        }
    }

    private IEnumerator OwnerHitTimeEnd(Character character, float hitTime, Vector3 vec)
	{
        yield return new WaitForSeconds(hitTime);
        if (character is not null && RoundManager.ReturnIsSetting())
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
        transform.SetParent(null);
        gameObject.SetActive(active);
        PoolManager.AddObjToPool("HitBox", gameObject);
    }
}
