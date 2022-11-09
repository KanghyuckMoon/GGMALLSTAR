using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : CharacterComponent
{
    public CharacterAnimation(Character character) : base(character)
    {

    }

    protected Animator _animator = null;

    protected override void Awake()
    {
        _animator = Character.GetComponentInChildren<Animator>();
    }
}
