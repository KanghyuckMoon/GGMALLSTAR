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
        SkillAllStarAction = AllStarSkill;

        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            if (CharacterLevel.Level > 1 && skillCoolTime1 >= Character.CharacterSO.skill1Delay)
            {
                skillCoolTime1 = 0f;
                Sound.SoundManager.Instance.PlayEFF("se_common_swing_07");
                Pool.PoolManager.GetItem("Assets/Prefabs/Agent_Slash.prefab").GetComponent<SlashSkill>().SetSlashSkill(_character, _character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0], _character.GetCharacterComponent<CharacterSprite>().Direction, 5f, new Vector3(0f, 0.035f, 0f));
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (CharacterLevel.Level > 2 && skillCoolTime2 >= Character.CharacterSO.skill2Delay)
            {
                skillCoolTime2 = 0f;
                Sound.SoundManager.Instance.PlayEFF("se_common_stage_demon_dojo_wall_break");
                character.StartCoroutine(StatUpgrade());
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            if (CharacterLevel.Level > 3 && !CharacterLevel.IsAllStarSkillUse)
            {
                isCanUseSkill3 = false;
                characterLevel.IsAllStarSkillUse = true;

                Sound.SoundManager.Instance.PlayEFF("se_common_boss_core_hit");
                CameraManager.SetAllStar(Character.transform, 2.5f);

                RoundManager.StaticSetInputSturnTime(1f);
                RoundManager.StaticStopMove(1f);
                AllStarSkillUse?.Invoke();

                Character.Animator.SetTrigger(AnimationKeyWord.ALL_STAR_SKILL);
            }
        }, EventType.KEY_DOWN);
    }

    public System.Action SkillAllStarAction;

    private void AllStarSkill()
    {
        Pool.PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(_character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0], _character.GetCharacterComponent<CharacterAttack>(), null, _character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]._attackSize, _character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]._attackOffset);
    }

    private IEnumerator StatUpgrade()
    {
        (Character as Character_Agent).SpeedEffect.SetActive(true);
        CharacterSO characterSO = Character.CharacterSO;

        float defaultSpeed = characterSO.MoveSpeed;
        float defaultAirSpeed = characterSO.AirMoveSpeed;
        float defaultGravitySpeed = characterSO.GravityScale;
        float defaultDodgeSpeed = characterSO.DodgeSpeed;
        //float defaultJumpPower = characterSO.FirstJumpPower;

        characterSO.MoveSpeed *= 2f;
        characterSO.GravityScale *= 2f;
        characterSO.AirMoveSpeed *= 2f;
        characterSO.DodgeSpeed *= 2f;
        //characterSO.FirstJumpPower *= 2f;

        yield return new WaitForSeconds(5f);
        (Character as Character_Agent).SpeedEffect.SetActive(false);

        characterSO.MoveSpeed = defaultSpeed;
        characterSO.GravityScale = defaultGravitySpeed;
        characterSO.AirMoveSpeed = defaultAirSpeed;
        characterSO.DodgeSpeed = defaultDodgeSpeed;
        //characterSO.FirstJumpPower = defaultJumpPower;

    }

    public override void Update()
    {
        base.Update();
        if (skillCoolTime1 < Character.CharacterSO.skill1Delay)
        {
            skillCoolTime1 += Time.deltaTime;
            skill1CoolTimeChange?.Invoke();
            isCanUseSkill1 = false;
        }
        else if (CharacterLevel.Level > 1)
        {
            isCanUseSkill1 = true;
        }

        if (skillCoolTime2 < Character.CharacterSO.skill2Delay)
        {
            skillCoolTime2 += Time.deltaTime;
            skill2CoolTimeChange?.Invoke();
            isCanUseSkill2 = false;
        }
        else if (CharacterLevel.Level > 2)
        {
            isCanUseSkill2 = true;
        }

        if (!CharacterLevel.IsAllStarSkillUse && CharacterLevel.Level > 3)
        {
            isCanUseSkill3 = true;
        }

    }
}
