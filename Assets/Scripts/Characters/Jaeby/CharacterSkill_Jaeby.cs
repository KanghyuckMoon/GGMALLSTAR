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
            PoolManager.GetItem("Assets/Prefabs/SkillProjectile_Jaeby.prefab").GetComponent<SkillProjectile_Jaeby>().SetSkillProjectile(Character, Character.GetCharacterComponent<CharacterSprite>().Direction, Character.transform.position + new Vector3(0, 0.09f, 0));
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            PoolManager.GetItem("Assets/Prefabs/SkillSpike_Jaeby.prefab").GetComponent<SkillSpike_Jaeby>().SetSkillSpike(Character.transform.position + new Vector3(0, 0.09f, 0));
        }, EventType.KEY_DOWN);
    }
}
