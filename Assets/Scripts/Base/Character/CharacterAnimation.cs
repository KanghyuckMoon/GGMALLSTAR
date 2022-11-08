using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : CharacterComponent
{
    public CharacterAnimation(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.ATTACK, Attack);
    }

    private Animator _animator = null;

    protected override void Awake()
    {
        _animator = Character.GetComponentInChildren<Animator>();
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
