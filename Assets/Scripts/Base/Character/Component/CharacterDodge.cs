using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterDodge : CharacterComponent
{
    // 움직임을 멈추는 시간
    private float _stunTime = 0f;

    // 재사용 대기 시간
    private float _coolTime = 0f;

    // Dodge 재사용 대기시간을 변경할 때 호출할 Action
    private System.Action _changeDodgeCoolTime;

    /// <summary>
    /// 재사용 대기 시간 Getter
    /// </summary>
    /// <value></value>
    public float CoolTime
    {
        get
        {
            return _coolTime;
        }
    }

    /// <summary>
    /// 재사용 대기 시간 비율 Getter
    /// </summary>
    /// <value></value>
    public float CoolTimeRatio
    {
        get
        {
            return _coolTime / 0.5f;
        }
    }

    // 오른쪽을 보고 있는지 채크하는 bool 변수
    private bool _isRight = false;

    // 입력 방향 변수
    private Vector2 _inputDirection = Vector2.zero;

    /// <summary>
    /// CharacterDodge 생성자
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterDodge(Character character) : base(character)
    {
        // _inputDirection 설정하는 이벤트 등록
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _inputDirection.x = -1;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _inputDirection.x = 1;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _inputDirection.x = -1;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _inputDirection.x = 1;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _inputDirection.x = 0;
        }, EventType.KEY_UP);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _inputDirection.x = 0;
        }, EventType.KEY_UP);
    }


    protected override void SetEvent()
    {
        base.SetEvent();

        // 오른쪽을 보고 있는지 채크하는 이벤트 등록
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _isRight = false;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _isRight = true;
        }, EventType.KEY_DOWN);
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _isRight = false;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _isRight = true;
        }, EventType.KEY_HOLD);

        // Dodge 이벤트 등록
        CharacterEvent.AddEvent(EventKeyWord.DODGE, () =>
        {
            Dodge();
        }, EventType.KEY_DOWN);
    }

    /// <summary>
    /// Dodge 재사용 대기시간을 변경할 때 호출할 Action 등록
    /// </summary>
    /// <param name="action"></param>
    public void AddChangeDodgeCoolTimeAction(System.Action action)
    {
        _changeDodgeCoolTime += action;
    }

    /// <summary>
    /// StunTime 설정
    /// </summary>
    /// <param name="time"></param>
    public void SetStunTime(float time)
    {
        _stunTime = time;
    }

    /// <summary>
    /// CoolTime 설정
    /// </summary>
    /// <param name="time"></param>
    public void SetCoolTime(float time)
    {
        _coolTime = time;
    }

    /// <summary>
    /// StunTime과 CoolTime 처리
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // 스턴중이면
        if (_stunTime > 0f)
        {
            // 스턴 시간 감소
            _stunTime -= Time.fixedDeltaTime;
            return;
        }
        // 쿨타임이 0보다 크면
        if (_coolTime > 0f)
        {
            // 쿨타임 감소
            _coolTime -= Time.fixedDeltaTime;
            // 쿨타임 처리
            _changeDodgeCoolTime?.Invoke();
        }
    }

    /// <summary>
    /// Dodge
    /// </summary>
    public void Dodge()
    {
        // 죽었는지 확인
        if (!Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
        {
            Character.Rigidbody.velocity = Vector3.zero;
            return;
        }

        // 스턴중인지 확인
        if (_stunTime > 0f)
        {
            return;
        }

        // 쿨타임인지 확인
        if (_coolTime > 0f)
        {
            return;
        }


        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Dirty_02, Character.transform.position);
        Character.StartCoroutine(ReturnState(Character.tag));
        Character.tag = "Invincibility";

        var characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);

        if (characterInput == null)
        {
            var aiInput = Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            aiInput.SetInputDelayTime(0.2f);
        }
        else
        {
            characterInput.SetInputDelayTime(0.2f);
        }


        Character.GetCharacterComponent<CharacterColor>(ComponentType.Color).SetWhiteMaterial();
        Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation).SetAnimationTrigger(AnimationType.Dodge);
        Character.GetCharacterComponent<CharacterMove>(ComponentType.Move).SetMoveDirection(_inputDirection);
        SetCoolTime(0.5f);

        var velocity = Character.Rigidbody.velocity;
        velocity.x = 0f;
        Character.Rigidbody.velocity = velocity;
        Character.Rigidbody.AddForce(_inputDirection * Character.CharacterSO.DodgeSpeed, ForceMode.Impulse);
    }

    private IEnumerator ReturnState(string originTag)
    {
        yield return new WaitForSeconds(0.2f);
        Character.GetCharacterComponent<CharacterColor>(ComponentType.Color).SetOriginMaterial();
        Character.tag = originTag;
    }

}
