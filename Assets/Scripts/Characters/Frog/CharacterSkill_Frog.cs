using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSkill_Frog : CharacterSkill
{
    public CharacterSkill_Frog(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {

        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            Debug.Log("Skill 2");
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            Debug.Log("All Star Skill");
        }, EventType.KEY_DOWN);
    }
}
