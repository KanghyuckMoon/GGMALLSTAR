using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Effect;
using Sound;
using Utill;
using DG.Tweening;

public class CharacterDamage : CharacterComponent
{
    /// <summary>
    /// CharacterAnimation Getter
    /// </summary>
    /// <value></value>
    private CharacterAnimation CharacterAnimation
    {
        get
        {
            // CharacterAnimation이 null일 경우 Character로부터 CharacterAnimation을 가져옴
            _characterAnimation ??= Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
            // CharacterAnimation을 반환
            return _characterAnimation;
        }
    }

    // 콤보 카운트
    private int _comboCount = 0;

    // CharacterStat 캐싱
    private CharacterStat _characterStat;

    // CharacterAnimation 캐싱
    private CharacterAnimation _characterAnimation;

    // 스턴 시간
    private float _sturnTime;

    // 맞았을 때 할 Action
    private System.Action _damagedAction;

    // 콤보 카운트 Getter
    public int HitCount
    {
        get
        {
            return _comboCount;
        }
    }

    // 스턴 시간 Getter
    public float SturnTime
    {
        get
        {
            return _sturnTime;
        }
    }

    // 맞았을 때 할 Action property
    public System.Action DamagedAction
    {
        get
        {
            return _damagedAction;
        }
        set
        {
            _damagedAction = value;
        }
    }

    /// <summary>
    /// CharacterDamage 생성자
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterDamage(Character character) : base(character)
    {

    }

    /// <summary>
    /// 변수 할당
    /// </summary>
    protected override void Awake()
    {
        // CharacterStat 캐싱
        _characterStat = Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
    }

    /// <summary>
    /// Event 등록
    /// </summary>
    protected override void SetEvent()
    {
        // CharacterEvent에 맞았을 때 이벤트 등록
        CharacterEvent.AddEvent(EventKeyWord.DAMAGE, OnDamage, EventType.DEFAULT);
    }

    /// <summary>
    /// Update 함수
    /// </summary>
    public override void Update()
    {
        base.Update();

        // 스턴 시간이 0보다 크면
        if (_sturnTime > 0f)
        {
            // 캐릭터 데미지 애니메이션 Hash true
            CharacterAnimation.SetAnimationBool(AnimationType.Damage, true);
            // 스턴 시간 감소
            _sturnTime -= Time.deltaTime;
        }
        else
        {
            // 콤보 초기화
            _comboCount = 0;
            // 캐릭터 데미지 애니메이션 Hash false
            CharacterAnimation.SetAnimationBool(AnimationType.Damage, false);
        }
    }

    /// <summary>
    /// 데미지 입었을때
    /// </summary>
    private void OnDamage()
    {

    }

    /// <summary>
    /// 공격 당했을때 후처리
    /// </summary>
    /// <param name="hitBox"></param>
    /// <param name="hitBoxData"></param>
    /// <param name="collistionPoint"></param>
    /// <param name="isRight"></param>
    public void OnAttacked(HitBox hitBox, HitBoxData hitBoxData, Vector3 collistionPoint, bool isRight)
    {
        // 캐릭터 죽었거나 라운드 설정 중이면 리턴
        if (!_characterStat.IsAlive || !RoundManager.ReturnIsSetting())
        {
            // 캐릭터 리지드바디 속도 0으로 초기화
            Character.Rigidbody.velocity = Vector3.zero;
            return;
        }

        // Hp 처리
        _characterStat.AddHP(-hitBoxData.damage);
        // CharacterDebug에 데미지 추가
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddDamaged(hitBoxData.damage);

        // Exp 처리
        Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level).AddExp(hitBoxData.addExp / 2);

        // Die
        if (!_characterStat.IsAlive)
        {
            // 라운드 종료 처리
            RoundManager.RoundEnd(Character);

            //Effect & Sound
            SoundManager.Instance.PlayEFF("se_common_finishhit");
            EffectManager.Instance.SetEffect(EffectType.FinalHit, collistionPoint);

            return;
        }

        // StunTime
        float stunHitTime = hitBoxData.sturnTime + hitBoxData.hitTime;

        // CameraShake
        CameraManager.SetShake(hitBoxData.shakeTime, hitBoxData.shakePower);
        hitBox?.OwnerHitTime(hitBoxData.hitTime);

        // Effect & Sound
        EffectManager.Instance.SetEffect(hitBoxData.hitEffectType, collistionPoint, EffectDirectionType.ReverseDirection, hitBoxData.atkEffectOffset, (collistionPoint.x < Character.transform.position.x ? true : false));

        SoundManager.Instance.PlayEFF(hitBoxData.hitEffSoundName);

        // ComboCount
        _comboCount++;
        _sturnTime = stunHitTime;
        CharacterAnimation.SetAnimationBool(AnimationType.Damage, true);
        _damagedAction?.Invoke();

        // Set HitTime & StunTime
        Vector3 vector = Character.Rigidbody.velocity;
        CharacterGravity characterGravity = Character.GetCharacterComponent<CharacterGravity>(ComponentType.Gravity);
        CharacterMove characterMove = Character.GetCharacterComponent<CharacterMove>(ComponentType.Move);
        CharacterDodge characterDodge = Character.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);
        characterMove.SetStunTime(stunHitTime);
        characterDodge.SetStunTime(stunHitTime);
        characterGravity.SetHitTime(stunHitTime);
        Character.Rigidbody.velocity = Vector3.zero;
        CharacterInput characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);

        if (characterInput is not null)
        {
            characterInput.SetStunTime(stunHitTime);
        }
        else
        {
            CharacterAIInput aITestInput = Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            if (aITestInput is not null)
            {
                aITestInput.SetStunTime(stunHitTime);
            }
        }

        // CharacterShake
        CharacterSprite characterSprite = Character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite);
        characterSprite.Model.transform.DOKill();
        characterSprite.Model.transform.DOShakePosition(hitBoxData.hitTime, 0.05f, 20).OnComplete(() =>
        {
            characterSprite.ResetModelPosition();

            if (_characterStat.IsAlive || !RoundManager.ReturnIsSetting())
            {
                Vector3 knockBackVector3 = DegreeToVector3(isRight ? hitBoxData.knockAngle : (-hitBoxData.knockAngle + 180));
                Character.Rigidbody.AddForce(knockBackVector3 * (hitBoxData.knockBack + _comboCount * 0.08f), ForceMode.Impulse);
            }
            else
            {
                Character.Rigidbody.velocity = Vector3.zero;
            }
        });

    }

    /// <summary>
    /// 각도를 통해 날라가는 방향에 대한 Vector3를 반환
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    private Vector3 DegreeToVector3(float degree)
    {
        return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0);
    }
}
