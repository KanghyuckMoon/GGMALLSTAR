using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using KeyWord;

public class CharacterAttack : CharacterComponent
{
    /// <summary>
    /// CharacterAttack 생성자
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterAttack(Character character) : base(character)
    {

    }

    // Character의 현재 바라보는 방향을 저장하는 변수
    private Direction _direction = Direction.RIGHT;

    // 공격 대상의 CharacterDamage 캐싱
    private CharacterDamage _targetCharacterDamage = null;

    // CharacterAnimation 캐싱
    private CharacterAnimation _characterAnimation = null;

    // 공격 대상의 CharacterDamage Getter
    public CharacterDamage TargetCharacterDamage
    {
        get => _targetCharacterDamage;
        set
        {
            _targetCharacterDamage = value;
            _targetCharacterDamage?.CharacterEvent?.EventTrigger(EventKeyWord.DAMAGE);
        }
    }

    // CharacterMove 캐싱
    CharacterMove _characterMove = null;

    // 캐릭터가 오른쪽을 보고 있는지 확인하는 Getter
    public bool IsRight
    {
        get
        {
            _characterMove ??= Character.GetCharacterComponent<CharacterMove>(ComponentType.Move);
            return _characterMove.IsRight;
        }
    }

    /// <summary>
    /// 변수 초기화해주는 Awake()
    /// </summary>
    protected override void Awake()
    {
        _direction = Character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite).Direction;
    }

    /// <summary>
    /// 이벤트 등록
    /// </summary>
    protected override void SetEvent()
    {
        // Attack 이벤트 등록
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, () =>
        {
            SetInputDelay();
            AttackAnimation();
        }, EventType.KEY_DOWN);
    }

    /// <summary>
    /// Attack 애니메이션 실행
    /// </summary>
    private void AttackAnimation()
    {
        // CharacterAnimation 안되어있으면 캐싱
        _characterAnimation ??= Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
        // 캐릭터의 위치에 따라 디버그 변경
        if (Character.GetCharacterComponent<CharacterMove>(ComponentType.Move).IsGround)
        {
            // 땅에 있으면 땅 공격 횟수 증가
            Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddGroundAttackCount(1);
        }
        else
        {
            // 공중에 있으면 공중 공격 횟수 증가
            Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddAirAttackCount(1);
        }
        // Attack 애니메이션 실행
        _characterAnimation.SetAnimationTrigger(AnimationType.Attack);
    }

    /// <summary>
    /// 공격 딜래이 설정
    /// </summary>
    public void SetInputDelay()
    {
        // CharacterInput 가져오기
        var characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);
        // CharacterInput이 있으면
        if (characterInput is not null)
        {
            // 공격 딜레이 설정
            characterInput.SetInputDelayTime(Character.CharacterSO.attackDelay);
        }
        else
        {
            // 없으면 AIInput 가져오기
            var aiInput = Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            // AIInput Delay 설정
            aiInput.SetInputDelayTime(Character.CharacterSO.attackDelay);
        }
    }

    /// <summary>
    /// 공격시 실행되는 함수
    /// </summary>
    /// <param name="hitBoxIndex"></param>
    public void OnAttack(int hitBoxIndex)
    {
        // 공격 판정 갯수만큼 반복
        foreach (var hitboxData in Character.HitBoxDataSO.hitBoxDatasList[hitBoxIndex].hitBoxDatas)
        {
            // 공격 사운드가 존재하면
            if (hitboxData.atkEffSoundName != "")
            {
                // 사운드 재생
                Sound.SoundManager.Instance.PlayEFF(hitboxData.atkEffSoundName);
            }

            // offset 설정
            Vector3 attackOffset = hitboxData._attackOffset;
            // 공격 방향 판단
            attackOffset.x *= IsRight ? 1 : -1;
            // Effect offset 설정
            Vector3 attackeffectOffset = hitboxData.atkEffectOffset;
            // 이펙트 방향 판단
            attackeffectOffset.x *= IsRight ? 1 : -1;

            // HitBox 생성 후 설정
            PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(hitboxData, this, () => Debug.Log("Hit"), hitboxData._attackSize, attackOffset);

            // 공격 이펙트 타입이 None이 아니면
            if (hitboxData.atkEffectType != Effect.EffectType.None)
            {
                // 이펙트 생성
                Effect.EffectManager.Instance.SetEffect(hitboxData.atkEffectType, Character.transform.position + attackOffset, hitboxData.atkEffectDirectionType, attackeffectOffset, IsRight);
                // 공격 사운드 재생
                Sound.SoundManager.Instance.PlayEFF(hitboxData.atkEffSoundName);
            }
        }
    }
}
