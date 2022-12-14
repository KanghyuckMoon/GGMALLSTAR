using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using KeyWord;

public class CharacterAttack : CharacterComponent
{
    public CharacterAttack(Character character) : base(character)
    {

    }

    private Direction _direction = Direction.RIGHT;

    private CharacterDamage _targetCharacterDamage = null;
    private CharacterAnimation characterAnimation = null;

    public CharacterDamage TargetCharacterDamage
    {
        get => _targetCharacterDamage;
        set
        {
            _targetCharacterDamage = value;
            _targetCharacterDamage?.CharacterEvent?.EventTrigger(EventKeyWord.DAMAGE);
        }
    }

    public bool IsRight
	{
        get
        {
            characterMove ??= Character.GetCharacterComponent<CharacterMove>(ComponentType.Move);
            return characterMove.IsRight;
        }
	}

    protected override void Awake()
    {
        _direction = Character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite).Direction;

    }

	CharacterMove characterMove = null;

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, () => 
        { 
            SetInputDelay();
            AttackAnimation();
        }, EventType.KEY_DOWN);
    }

    private void AttackAnimation()
    {
        characterAnimation ??= Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
        if(Character.GetCharacterComponent<CharacterMove>(ComponentType.Move).IsGround)
        {
            Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddGroundAttackCount(1);
        }
		else
        {
            Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddAirAttackCount(1);
        }
        characterAnimation.SetAnimationTrigger(AnimationType.Attack);
    }

    public void SetInputDelay()
	{
        var characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);
        if (characterInput is not null)
		{
            characterInput.SetInputDelayTime(Character.CharacterSO.attackDelay);
        }
        else
        {
            var aiInput = Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            aiInput.SetInputDelayTime(Character.CharacterSO.attackDelay);
        }
    }

    public void OnAttack(int hitBoxIndex)
    {
        foreach (var hitboxData in Character.HitBoxDataSO.hitBoxDatasList[hitBoxIndex].hitBoxDatas)
        {
            if (hitboxData.atkEffSoundName != "")
			{
                Sound.SoundManager.Instance.PlayEFF(hitboxData.atkEffSoundName);
            }

            Vector3 attackOffset = hitboxData._attackOffset;
            attackOffset.x *= IsRight ? 1 : -1;
            Vector3 attackeffectOffset = hitboxData.atkEffectOffset;
            attackeffectOffset.x *= IsRight ? 1 : -1;
            PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(hitboxData, this, () => Debug.Log("Hit"), hitboxData._attackSize, attackOffset);

            if (hitboxData.atkEffectType != Effect.EffectType.None)
            {
                Effect.EffectManager.Instance.SetEffect(hitboxData.atkEffectType, Character.transform.position + attackOffset, hitboxData.atkEffectDirectionType, attackeffectOffset, IsRight);
                Sound.SoundManager.Instance.PlayEFF(hitboxData.atkEffSoundName);
            }
        }
    }
}
