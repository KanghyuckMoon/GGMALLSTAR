using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : CharacterComponent
{
    public CharacterAttack(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.ATTACK, Attack);
    }

    private bool _isAttack = false;

    protected override void Awake()
    {
        _isAttack = false;
    }

    private void Attack()
    {
        _isAttack = !_isAttack;
        if (_isAttack)
        {
            return;
        }
        Debug.Log("Attack");
    }
}
