using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Attack");
    }
}
