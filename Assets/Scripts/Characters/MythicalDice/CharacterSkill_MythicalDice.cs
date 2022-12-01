using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSkill_MythicalDice : CharacterSkill
{
    public CharacterSkill_MythicalDice(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            Debug.Log("Skill1");
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            Debug.Log("Skill2");
        }, EventType.KEY_DOWN);
    }
}
