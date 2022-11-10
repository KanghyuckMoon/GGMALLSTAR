using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : CharacterComponent
{
    public CharacterDamage(Character character) : base(character)
    {

    }

    protected override void Awake()
    {
        _hp = Character.GetCharacterComponent<CharacterStat>().MaxHP;
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.DAMAGE, OnDamage, EventType.DEFAULT);
    }

    private float _hp = 0;

    private void OnDamage()
    {
        _hp -= 10;
        if (_hp <= 0)
        {
            Debug.Log("Die");
        }
    }
}
