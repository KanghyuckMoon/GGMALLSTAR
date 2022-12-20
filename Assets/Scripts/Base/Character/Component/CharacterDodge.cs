using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterDodge : CharacterComponent
{
    // �������� ���ߴ� �ð�
    private float _stunTime = 0f;

    // ���� ��� �ð�
    private float _coolTime = 0f;

    // Dodge ���� ���ð��� ������ �� ȣ���� Action
    private System.Action _changeDodgeCoolTime;

    /// <summary>
    /// ���� ��� �ð� Getter
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
    /// ���� ��� �ð� ���� Getter
    /// </summary>
    /// <value></value>
    public float CoolTimeRatio
    {
        get
        {
            return _coolTime / 0.5f;
        }
    }

    // �������� ���� �ִ��� äũ�ϴ� bool ����
    private bool _isRight = false;

    // �Է� ���� ����
    private Vector2 _inputDirection = Vector2.zero;

    /// <summary>
    /// CharacterDodge ������
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterDodge(Character character) : base(character)
    {
        // _inputDirection �����ϴ� �̺�Ʈ ���
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

        // �������� ���� �ִ��� äũ�ϴ� �̺�Ʈ ���
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

        // Dodge �̺�Ʈ ���
        CharacterEvent.AddEvent(EventKeyWord.DODGE, () =>
        {
            Dodge();
        }, EventType.KEY_DOWN);
    }

    /// <summary>
    /// Dodge ���� ���ð��� ������ �� ȣ���� Action ���
    /// </summary>
    /// <param name="action"></param>
    public void AddChangeDodgeCoolTimeAction(System.Action action)
    {
        _changeDodgeCoolTime += action;
    }

    /// <summary>
    /// StunTime ����
    /// </summary>
    /// <param name="time"></param>
    public void SetStunTime(float time)
    {
        _stunTime = time;
    }

    /// <summary>
    /// CoolTime ����
    /// </summary>
    /// <param name="time"></param>
    public void SetCoolTime(float time)
    {
        _coolTime = time;
    }

    /// <summary>
    /// StunTime�� CoolTime ó��
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // �������̸�
        if (_stunTime > 0f)
        {
            // ���� �ð� ����
            _stunTime -= Time.fixedDeltaTime;
            return;
        }
        // ��Ÿ���� 0���� ũ��
        if (_coolTime > 0f)
        {
            // ��Ÿ�� ����
            _coolTime -= Time.fixedDeltaTime;
            // ��Ÿ�� ó��
            _changeDodgeCoolTime?.Invoke();
        }
    }

    /// <summary>
    /// Dodge
    /// </summary>
    public void Dodge()
    {
        // �׾����� Ȯ��
        if (!Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
        {
            Character.Rigidbody.velocity = Vector3.zero;
            return;
        }

        // ���������� Ȯ��
        if (_stunTime > 0f)
        {
            return;
        }

        // ��Ÿ������ Ȯ��
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
