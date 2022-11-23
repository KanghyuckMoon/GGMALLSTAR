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

    public bool IsRight
	{
        get
		{
            return _isRight;
		}
	}

    private Direction _direction = Direction.RIGHT;

    private bool _isRight = false;

    private CharacterDamage _targetCharacterDamage = null;

    public CharacterDamage TargetCharacterDamage
    {
        get => _targetCharacterDamage;
        set
        {
            _targetCharacterDamage = value;
            _targetCharacterDamage?.CharacterEvent?.EventTrigger(EventKeyWord.DAMAGE);
        }
    }

    protected override void Awake()
    {
        _direction = Character.GetCharacterComponent<CharacterSprite>().Direction;
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, SetInputDelay, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _isRight = false;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _isRight = true;
        }, EventType.KEY_DOWN);
    }

    public void SetInputDelay()
	{
        var characterInput = Character.GetCharacterComponent<CharacterInput>();
        if (characterInput is not null)
		{
            characterInput.SetInputDelayTime(Character.CharacterSO.attackDelay);
        }
        else
        {
            var aiInput = Character.GetCharacterComponent<AITestInput>();
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
            PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(hitboxData, this, () => Debug.Log("Hit"), hitboxData._attackSize, attackOffset);
        }
    }
}
