using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using KeyWord;

public class CharacterAttack : CharacterComponent
{
    /// <summary>
    /// CharacterAttack ������
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterAttack(Character character) : base(character)
    {

    }

    // Character�� ���� �ٶ󺸴� ������ �����ϴ� ����
    private Direction _direction = Direction.RIGHT;

    // ���� ����� CharacterDamage ĳ��
    private CharacterDamage _targetCharacterDamage = null;

    // CharacterAnimation ĳ��
    private CharacterAnimation _characterAnimation = null;

    // ���� ����� CharacterDamage Getter
    public CharacterDamage TargetCharacterDamage
    {
        get => _targetCharacterDamage;
        set
        {
            _targetCharacterDamage = value;
            _targetCharacterDamage?.CharacterEvent?.EventTrigger(EventKeyWord.DAMAGE);
        }
    }

    // CharacterMove ĳ��
    CharacterMove _characterMove = null;

    // ĳ���Ͱ� �������� ���� �ִ��� Ȯ���ϴ� Getter
    public bool IsRight
    {
        get
        {
            _characterMove ??= Character.GetCharacterComponent<CharacterMove>(ComponentType.Move);
            return _characterMove.IsRight;
        }
    }

    /// <summary>
    /// ���� �ʱ�ȭ���ִ� Awake()
    /// </summary>
    protected override void Awake()
    {
        _direction = Character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite).Direction;
    }

    /// <summary>
    /// �̺�Ʈ ���
    /// </summary>
    protected override void SetEvent()
    {
        // Attack �̺�Ʈ ���
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, () =>
        {
            SetInputDelay();
            AttackAnimation();
        }, EventType.KEY_DOWN);
    }

    /// <summary>
    /// Attack �ִϸ��̼� ����
    /// </summary>
    private void AttackAnimation()
    {
        // CharacterAnimation �ȵǾ������� ĳ��
        _characterAnimation ??= Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
        // ĳ������ ��ġ�� ���� ����� ����
        if (Character.GetCharacterComponent<CharacterMove>(ComponentType.Move).IsGround)
        {
            // ���� ������ �� ���� Ƚ�� ����
            Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddGroundAttackCount(1);
        }
        else
        {
            // ���߿� ������ ���� ���� Ƚ�� ����
            Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddAirAttackCount(1);
        }
        // Attack �ִϸ��̼� ����
        _characterAnimation.SetAnimationTrigger(AnimationType.Attack);
    }

    /// <summary>
    /// ���� ������ ����
    /// </summary>
    public void SetInputDelay()
    {
        // CharacterInput ��������
        var characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);
        // CharacterInput�� ������
        if (characterInput is not null)
        {
            // ���� ������ ����
            characterInput.SetInputDelayTime(Character.CharacterSO.attackDelay);
        }
        else
        {
            // ������ AIInput ��������
            var aiInput = Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            // AIInput Delay ����
            aiInput.SetInputDelayTime(Character.CharacterSO.attackDelay);
        }
    }

    /// <summary>
    /// ���ݽ� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="hitBoxIndex"></param>
    public void OnAttack(int hitBoxIndex)
    {
        // ���� ���� ������ŭ �ݺ�
        foreach (var hitboxData in Character.HitBoxDataSO.hitBoxDatasList[hitBoxIndex].hitBoxDatas)
        {
            // ���� ���尡 �����ϸ�
            if (hitboxData.atkEffSoundName != "")
            {
                // ���� ���
                Sound.SoundManager.Instance.PlayEFF(hitboxData.atkEffSoundName);
            }

            // offset ����
            Vector3 attackOffset = hitboxData._attackOffset;
            // ���� ���� �Ǵ�
            attackOffset.x *= IsRight ? 1 : -1;
            // Effect offset ����
            Vector3 attackeffectOffset = hitboxData.atkEffectOffset;
            // ����Ʈ ���� �Ǵ�
            attackeffectOffset.x *= IsRight ? 1 : -1;

            // HitBox ���� �� ����
            PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(hitboxData, this, () => Debug.Log("Hit"), hitboxData._attackSize, attackOffset);

            // ���� ����Ʈ Ÿ���� None�� �ƴϸ�
            if (hitboxData.atkEffectType != Effect.EffectType.None)
            {
                // ����Ʈ ����
                Effect.EffectManager.Instance.SetEffect(hitboxData.atkEffectType, Character.transform.position + attackOffset, hitboxData.atkEffectDirectionType, attackeffectOffset, IsRight);
                // ���� ���� ���
                Sound.SoundManager.Instance.PlayEFF(hitboxData.atkEffSoundName);
            }
        }
    }
}
