using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public abstract class CharacterSkill : CharacterComponent
{
    protected CharacterLevel characterLevel;
    protected float skillCoolTime1 = 0f;
    protected float skillCoolTime2 = 0f;
    public float Skill1RemainCoolTime
	{
        get
		{
            return Character.CharacterSO.skill1Delay - skillCoolTime1;
		}
	}
    public float Skill2RemainCoolTime
    {
        get
        {
            return Character.CharacterSO.skill2Delay - skillCoolTime2;
        }
    }

    public float Skill1CoolTimeRatio
    {
        get
        {
            return skillCoolTime1/ Character.CharacterSO.skill1Delay;
        }
    }
    public float Skill2CoolTimeRatio
    {
        get
        {
            return skillCoolTime2 / Character.CharacterSO.skill2Delay;
        }
    }


    protected System.Action skill1CoolTimeChange;
    protected System.Action skill2CoolTimeChange;
    protected System.Action allStarSkillUse;

    public System.Action Skill1CoolTimeChange => skill1CoolTimeChange;
    public System.Action Skill2CoolTimeChange => skill2CoolTimeChange;
    protected System.Action AllStarSkillUse => allStarSkillUse;

    public CharacterSkill(Character character) : base(character)
    {

    }

    public void AddSkill1CoolTimeChange(System.Action action)
	{
        skill1CoolTimeChange += action;
    }
    public void AddSkill2CoolTimeChange(System.Action action)
    {
        skill2CoolTimeChange += action;
    }
    public void AddAllStarSkillUse(System.Action action)
    {
        allStarSkillUse += action;
    }

}
