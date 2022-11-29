using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Pool;

public class CharacterSkill_Jaeby : CharacterSkill
{
    public CharacterSkill_Jaeby(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            Debug.Log("Skill1");
            PoolManager.GetItem("Assets/Prefabs/SkillProjectile_Jaeby.prefab").GetComponent<SkillProjectile_Jaeby>().SetSkillProjectile(Character, Character.GetCharacterComponent<CharacterSprite>().Direction);
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            Debug.Log("Skill2");
        }, EventType.KEY_DOWN);
    }

    private SkillProjectile_Jaeby _skill1 = null;
    private Skill _skill2 = null;
}
