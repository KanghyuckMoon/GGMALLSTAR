using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSkill2_MythicalDice : CharacterSkill
{
    public CharacterSkill2_MythicalDice(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            Debug.Log("Skill2");
        }, EventType.KEY_DOWN);
    }
}
