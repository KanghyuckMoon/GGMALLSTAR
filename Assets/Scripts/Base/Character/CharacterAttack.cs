using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : CharacterComponent
{
    public CharacterAttack(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.ATTACK, Attack, EventType.KEY_DOWN);
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }
}
