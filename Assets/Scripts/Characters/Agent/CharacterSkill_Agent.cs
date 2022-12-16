using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSkill_Agent : CharacterSkill
{
    public CharacterSkill_Agent(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            Pool.PoolManager.GetItem("Assets/Prefabs/Agent_Slash.prefab").GetComponent<SlashSkill>().SetSlashSkill(_character, _character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0], _character.GetCharacterComponent<CharacterSprite>().Direction, 5f, new Vector3(0f, 0.035f, 0f));
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {

        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {

        }, EventType.KEY_DOWN);
    }
}
