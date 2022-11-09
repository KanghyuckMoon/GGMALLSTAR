using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

public class CharacterAttack : CharacterComponent
{
    public CharacterAttack(Character character) : base(character)
    {
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, OnAttack, EventType.KEY_DOWN);
    }

    protected virtual void OnAttack()
    {
        HitBox hitBox = PoolManager.GetItem<HitBox>("HitBox");
        hitBox.transform.position = Character.transform.position;
    }
}
