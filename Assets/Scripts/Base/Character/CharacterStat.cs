using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : CharacterComponent
{
    public CharacterStat(Character character) : base(character)
    {
    }

    protected float _maxHP = 0;
}
