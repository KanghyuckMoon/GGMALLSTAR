using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using KeyWord;
using Pool;

public class CharacterSkill_Damvi : CharacterSkill
{
    public CharacterLevel CharacterLevel
    {
        get
        {
            characterLevel ??= Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
            return characterLevel;
        }
    }
    
    public CharacterSkill_Damvi(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            if (CharacterLevel.Level > 1 && skillCoolTime1 >= Character.CharacterSO.skill1Delay)
            {
                skillCoolTime1 = 0;

                PoolManager.GetItem("Bullet_Damvi").GetComponent<Bullet_Damvi>().SetBullet(
                    character, new Vector3(1 * (Character.GetCharacterComponent<CharacterMove>().IsRight ? 1 : -1),0,0),
                    character.transform.position,
                    character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0]);

                Skill1Action();
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (CharacterLevel.Level > 2 && skillCoolTime2 >= Character.CharacterSO.skill2Delay)
            {
                skillCoolTime2 = 0;

                PoolManager.GetItem("Bullet_Damvi").GetComponent<Bullet_Damvi>().SetBullet(
                    character, new Vector3(1 * (Character.GetCharacterComponent<CharacterMove>().IsRight ? 0.7f : -0.7f), 0.2f, 0),
                    character.transform.position,
                    character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]);

                PoolManager.GetItem("Bullet_Damvi").GetComponent<Bullet_Damvi>().SetBullet(
                    character, new Vector3(1 * (Character.GetCharacterComponent<CharacterMove>().IsRight ? 0.7f : -0.7f), 0, 0),
                    character.transform.position,
                    character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]);

                PoolManager.GetItem("Bullet_Damvi").GetComponent<Bullet_Damvi>().SetBullet(
                    character, new Vector3(1 * (Character.GetCharacterComponent<CharacterMove>().IsRight ? 0.7f : -0.7f), -0.2f, 0),
                    character.transform.position,
                    character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]);

                Skill2Action();
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            if (CharacterLevel.Level > 3 && !characterLevel.IsAllStarSkillUse)
            {
                isCanUseSkill3 = false;
                characterLevel.IsAllStarSkillUse = true;
                Sound.SoundManager.Instance.PlayEFF("se_common_boss_core_hit");
                CameraManager.SetAllStar(Character.transform);
                RoundManager.StaticSetInputSturnTime(1f);
                RoundManager.StaticStopMove(1f);
                AllStarSkillUse?.Invoke();
                Character.StartCoroutine(AllStarSkill());
                AllStarSkillUse?.Invoke();
                AllStarSkillAction();
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
        else if(CharacterLevel.Level > 1)
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

        if (RoundManager.ReturnIsSetting())
        {
            PoolManager.GetItem("Maggellan_Damvi").GetComponent<Maggellan_Damvi>().SetMaggellan(Character, Vector3.zero, Character.transform.position, Character.HitBoxDataSO.hitBoxDatasList[3].hitBoxDatas[0]);
        }
    }
}
