using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSkill_Agent : CharacterSkill
{
    public CharacterLevel CharacterLevel
    {
        get
        {
            characterLevel ??= Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
            return characterLevel;
        }
    }

    public CharacterSkill_Agent(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            if (CharacterLevel.Level > 1 && skillCoolTime1 >= Character.CharacterSO.skill1Delay)
            {
                Pool.PoolManager.GetItem("Assets/Prefabs/Agent_Slash.prefab").GetComponent<SlashSkill>().SetSlashSkill(_character, _character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0], _character.GetCharacterComponent<CharacterSprite>().Direction, 5f, new Vector3(0f, 0.035f, 0f));
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (CharacterLevel.Level > 2 && skillCoolTime2 >= Character.CharacterSO.skill2Delay)
            {
                character.StartCoroutine(StatUpgrade());
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            if (CharacterLevel.Level > 3 && !CharacterLevel.IsAllStarSkillUse)
            {
                Pool.PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(_character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0], _character.GetCharacterComponent<CharacterAttack>(), null, _character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]._attackSize, _character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]._attackOffset);
                character.Animator.SetTrigger(AnimationKeyWord.ALL_STAR_SKILL);
            }
        }, EventType.KEY_DOWN);
    }

    private IEnumerator StatUpgrade()
    {
        CharacterSO characterSO = Character.CharacterSO;

        float defaultSpeed = characterSO.MoveSpeed;
        float defaultJumpPower = characterSO.FirstJumpPower;

        characterSO.MoveSpeed *= 2f;
        characterSO.FirstJumpPower *= 2f;

        yield return new WaitForSeconds(5f);

        characterSO.MoveSpeed = defaultSpeed;
        characterSO.FirstJumpPower = defaultJumpPower;

    }
}
