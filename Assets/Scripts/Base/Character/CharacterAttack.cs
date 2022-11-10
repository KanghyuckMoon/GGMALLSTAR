using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

public class CharacterAttack : CharacterComponent
{
    public CharacterAttack(Character character) : base(character)
    {

    }

    private Direction _direction = Direction.RIGHT;

    private Vector3 _attackOffset = Vector3.zero;
    private Vector3 _attackSize = Vector3.zero;

    private CharacterDamage _targetCharacterDamage = null;
    public CharacterDamage TargetCharacterDamage
    {
        get
        {
            return _targetCharacterDamage;
        }
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
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, OnAttack, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _attackOffset.x = -Mathf.Abs(_attackOffset.x);
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _attackOffset.x = Mathf.Abs(_attackOffset.x);
        }, EventType.KEY_DOWN);
    }

    protected virtual void OnAttack()
    {
        PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(this, () => Debug.Log("Hit"), _attackSize, _attackOffset);
    }
}
