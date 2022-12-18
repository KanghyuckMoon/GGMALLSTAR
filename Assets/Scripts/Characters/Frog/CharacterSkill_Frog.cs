using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using KeyWord;
using Pool;

public class CharacterSkill_Frog : CharacterSkill
{
    public CharacterLevel CharacterLevel
    {
        get
        {
            characterLevel ??= Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
            return characterLevel;
        }
    }

    public CharacterSkill_Frog(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            if (CharacterLevel.Level > 1 && skillCoolTime1 >= Character.CharacterSO.skill1Delay)
            {
                skillCoolTime1 = 0;

                PoolManager.GetItem("Assets/Prefabs/Fireball_Frog.prefab").GetComponent<FireBall_Frog>().SetFireBall(
                    character, character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite).Direction,
                    character.transform.position + new Vector3(0.2f, 0.05f, 0),
                    character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0]);

                Skill1Action();
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (CharacterLevel.Level > 2 && skillCoolTime2 >= Character.CharacterSO.skill2Delay)
            {
                skillCoolTime2 = 0;

                //Transform enemyTransform = null;
                //var characterAtk = character.GetCharacterComponent<CharacterAttack>();
                //PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(
                //    character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0],
                //    characterAtk,
                //    () => { },
                //    character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]._attackSize,
                //    character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0]._attackOffset +
                //    (character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT
                //        ? new Vector3(0.15f, 0, 0)
                //        : new Vector3(-0.15f, 0, 0)));
                character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation).SetAnimationTrigger(AnimationType.Skill2);
                Skill2Action();
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            if (CharacterLevel.Level > 3 && !characterLevel.IsAllStarSkillUse)
            {
                isCanUseSkill3 = false;
                characterLevel.IsAllStarSkillUse = true;
                Transform enemyTransform = character.GetCharacterComponent<CharacterAttack>(ComponentType.Attack).TargetCharacterDamage
                    .Character.transform;
                Sound.SoundManager.Instance.PlayEFF("se_common_boss_core_hit");
                CameraManager.SetAllStar(Character.transform);
                RoundManager.StaticSetInputSturnTime(1f);
                RoundManager.StaticStopMove(1f);
                AllStarSkillUse?.Invoke();
                character.StartCoroutine(AllStarSkill(enemyTransform));
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

    private IEnumerator SecondSkill(Transform trm)
    {
        yield return new WaitForSeconds(1f);
        trm.position = Character.transform.position + (Character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite).Direction == Direction.LEFT ? Vector3.left : Vector3.right) * 0.25f;
    }

    private IEnumerator AllStarSkill(Transform trm)
    {
        string tag = Character.tag;
        Character.tag = "Invincibility";
        yield return new WaitForSeconds(1f);
        Character.GetComponentInChildren<SpriteRenderer>().transform.DOScale(3f, 1f);
        Character.transform.DOJump(trm.position, 0.5f, 1, 1f);

        if (RoundManager.ReturnIsSetting())
        {
            PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(Character.HitBoxDataSO.hitBoxDatasList[3].hitBoxDatas[0], Character.GetCharacterComponent<CharacterAttack>(ComponentType.Attack), null, Character.HitBoxDataSO.hitBoxDatasList[3].hitBoxDatas[0]._attackSize, Character.HitBoxDataSO.hitBoxDatasList[3].hitBoxDatas[0]._attackOffset);
        }
        yield return new WaitForSeconds(1f);
        Character.tag = tag;
        Character.GetComponentInChildren<SpriteRenderer>().transform.DOScale(1f, 0.3f);
    }
}
