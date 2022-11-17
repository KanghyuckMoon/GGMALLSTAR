using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSkill1_MythicalDice : CharacterSkill
{
    public CharacterSkill1_MythicalDice(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL1, () =>
        {
            Debug.Log("Skill1");
        }, EventType.KEY_DOWN);
    }
}
