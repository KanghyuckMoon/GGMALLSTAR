using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : CharacterComponent
{
    public CharacterStat(Character character) : base(character)
    {
    }

    protected override void SetEvent()
    {

    }

    protected float _maxHP = 0;
    public float MaxHP => _maxHP;

    protected override void Awake()
    {
        CharacterSO characterSO = Character.CharacterSO;

        _maxHP = characterSO.MaxHP;
    }
}
