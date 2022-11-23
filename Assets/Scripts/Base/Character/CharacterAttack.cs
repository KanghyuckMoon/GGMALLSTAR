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

    private Vector3 _attackOffset = Vector3.zero;
    private Vector3 _attackSize = Vector3.zero;
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
        _attackOffset = Character.CharacterSO.HitBoxOffset;
        _attackSize = Character.CharacterSO.HitBoxSize;
    }

    protected override void SetEvent()
    {
        //CharacterEvent.AddEvent(EventKeyWord.ATTACK, OnAttack, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _isRight = false;
            _attackOffset.x = -Mathf.Abs(_attackOffset.x);
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _isRight = true;
            _attackOffset.x = Mathf.Abs(_attackOffset.x);
        }, EventType.KEY_DOWN);
    }

    public void OnAttack(int hitBoxIndex)
    {
        foreach (var hitboxData in Character.HitBoxDataSO.hitBoxDatasList[hitBoxIndex].hitBoxDatas)
        {
            Sound.SoundManager.Instance.PlayEFF(hitboxData.atkEffSoundName);
            PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(hitboxData, this, () => Debug.Log("Hit"), _attackSize, _attackOffset);
        }
    }
}
