using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Pool;
using System;

public class CharacterSkill_Jaeby : CharacterSkill
{

    public CharacterLevel CharacterLevel
    {
        get
        {
            characterLevel ??= Character.GetCharacterComponent<CharacterLevel>();
            return characterLevel;
        }
    }

    public override void Start()
    {
        base.Start();
    }

    public CharacterSkill_Jaeby(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            if (CharacterLevel.Level > 1 && skillCoolTime1 > Character.CharacterSO.skill1Delay)
            {
                skillCoolTime1 = 0f;

                Character.GetCharacterComponent<CharacterAnimation>().SetAnimationTrigger(AnimationType.Skill1);
                PoolManager.GetItem("Assets/Prefabs/SkillProjectile_Jaeby.prefab").GetComponent<SkillProjectile_Jaeby>().SetSkillProjectile(Character, Character.GetCharacterComponent<CharacterSprite>().Direction, Character.transform.position + new Vector3(0, 0.09f, 0), Character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0]);
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (CharacterLevel.Level > 2 && skillCoolTime2 > Character.CharacterSO.skill2Delay)
            {
                skillCoolTime2 = 0f;

                Sound.SoundManager.Instance.PlayEFF("JaebyAttack");
                Character.GetCharacterComponent<CharacterAnimation>().SetAnimationTrigger(AnimationType.Skill2);
                PoolManager.GetItem("Assets/Prefabs/SkillSpike_Jaeby.prefab").GetComponent<SkillSpike_Jaeby>().SetSkillSpike(Character, Character.transform.position + new Vector3(0, 0.09f, 0), Character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]);
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            if (CharacterLevel.Level > 3 && !CharacterLevel.IsAllStarSkillUse)
            {
                isCanUseSkill3 = false;
                characterLevel.IsAllStarSkillUse = true;
                Sound.SoundManager.Instance.PlayEFF("se_common_boss_core_hit");
                CameraManager.SetAllStar(Character.transform);
                RoundManager.StaticSetInputSturnTime(1f);
                RoundManager.StaticStopMove(1f);
                AllStarSkillUse?.Invoke();
                Character.StartCoroutine(AllStarSkill());
            }
        }, EventType.KEY_DOWN);
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

    private IEnumerator AllStarSkill()
    {
        yield return new WaitForSeconds(1f);

        if(RoundManager.ReturnIsSetting())
        {
            PoolManager.GetItem("Assets/Prefabs/ALL_STAR_SKILL_Projectile_Jaeby.prefab").GetComponent<AllStarSkillProjectile_Jaeby>().SetSkillProjectile(Character, Character.GetCharacterComponent<CharacterSprite>().Direction, Character.transform.position + new Vector3(0, 0.09f, 0), Character.HitBoxDataSO.hitBoxDatasList[3].hitBoxDatas[0]);
		}
    }
}
