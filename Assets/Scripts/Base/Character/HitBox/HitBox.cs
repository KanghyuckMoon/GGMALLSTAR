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
    // 충돌체 캐싱 변수
    private BoxCollider _boxCollider = null;

    // HitBox의 주인
    private CharacterAttack _owner = null;
    public CharacterAttack Owner { get => _owner; set => _owner = value; }

    // 때릴때 호출할 함수
    private Action _onHit = null;
    public Action OnHit { get => _onHit; set => _onHit = value; }

    // HitBox의 데이터
    public HitBoxData hitBoxData;

    /// <summary>
    /// HitBox를 설정하는 함수
    /// </summary>
    /// <param name="hitBoxData"></param>
    /// <param name="owner"></param>
    /// <param name="onHit"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
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
        // 충돌체 캐싱
        _boxCollider = GetComponent<BoxCollider>();
        // 충돌체 기본 설정
        _boxCollider.isTrigger = true;
    }

    /// <summary>
    /// 때렸을때 후처리
    /// </summary>
    /// <param name="hitTime"></param>
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

    /// <summary>
    /// 실질적인 피격 후 처리
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (_owner != null && other.gameObject == _owner.Character.gameObject)
        {
            return;
        }

        if (!other.gameObject.CompareTag(_owner.Character.tag) && !other.gameObject.CompareTag("Invincibility") && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")))
        {
            Owner.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>(ComponentType.Damage);
            Owner.TargetCharacterDamage?.OnAttacked(this, hitBoxData, other.ClosestPoint(transform.position), Owner.IsRight);
            OnHit?.Invoke();

            //AI
            CharacterAIInput aiInput = Owner.Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            if (aiInput is not null)
            {
                aiInput.IsHit(hitBoxData.actionName);
            }

            //Exp

            int expCount = (hitBoxData.addExp / 7) + 1;

            for (int i = 0; i < expCount; ++i)
            {
                StarEffect starEffect = PoolManager.GetItem("StarEff").GetComponent<StarEffect>();
                starEffect.SetEffect(transform.position, Owner.Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level), hitBoxData.addExp / expCount);
            }

        }
    }

    /// <summary>
    /// 때리는 시간 끝~
    /// </summary>
    /// <param name="character"></param>
    /// <param name="hitTime"></param>
    /// <param name="vec"></param>
    /// <returns></returns>
    private IEnumerator OwnerHitTimeEnd(Character character, float hitTime, Vector3 vec)
    {
        yield return new WaitForSeconds(hitTime);
        if (character is not null && RoundManager.ReturnIsSetting())
        {
            character.Rigidbody.velocity = vec;
        }
    }

    /// <summary>
    /// Pool 되었을때
    /// </summary>
    private void OnEnable()
    {
        // 피격 판정 Pull 코루틴 실행
        StartCoroutine(SetActiveCoroutine());
    }

    private void OnDisable()
    {
        _onHit = null;
        _owner = null;
    }

    /// <summary>
    /// 피격 판정 삭제해주는 코르틴
    /// </summary>
    /// <param name="active"></param>
    /// <returns></returns>
    private IEnumerator SetActiveCoroutine(bool active = false)
    {
        yield return new WaitForSeconds(0.1f);
        transform.SetParent(null);
        gameObject.SetActive(active);
        PoolManager.AddObjToPool("HitBox", gameObject);
    }
}
