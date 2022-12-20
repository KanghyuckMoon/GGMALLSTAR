using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Pool;

public class CharacterSkill_MythicalDice : CharacterSkill
{
    public CharacterLevel CharacterLevel
    {
        get
        {
            characterLevel ??= Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
            return characterLevel;
        }
    }

    private Queue<Dice> diceQueue = new();

    public CharacterSkill_MythicalDice(Character character) : base(character)
    {
        var hitboxDataSO = (ScriptableObject.CreateInstance(typeof(HitBoxDataSO)) as HitBoxDataSO);
        character.HitBoxDataSO.Copy(ref hitboxDataSO);
        character.HitBoxDataSO = hitboxDataSO;

        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            if (CharacterLevel.Level > 1 && skillCoolTime1 >= Character.CharacterSO.skill1Delay)
            {
                skillCoolTime1 = 0;
                RollDice();
                Skill1Action();
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (CharacterLevel.Level > 2 && skillCoolTime2 >= Character.CharacterSO.skill2Delay)
            {
                if (diceQueue.Count == 4)
                {
                    skillCoolTime2 = 0;
                    int damage = 0;
                    Character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0].damage = 20;
                    while (diceQueue.Count > 0)
                    {
                        Debug.Log(diceQueue.Peek().DiceNumber);
                        diceQueue.Peek().gameObject.SetActive(false);
                        PoolManager.AddObjToPool("Assets/Prefabs/Dice.prefab", diceQueue.Peek().gameObject);
                        damage += diceQueue.Dequeue().DiceNumber;
                    }
                    Character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0].damage += damage;

                    Character.Animator.SetTrigger(AnimationKeyWord.SKILL2);

                    Skill2Action();
                }
                else
                {
                    Debug.Log($"주사위가 {diceQueue.Count}개 있습니다.");
                }
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            if (CharacterLevel.Level > 3 && !CharacterLevel.IsAllStarSkillUse)
            {
                isCanUseSkill3 = false;
                characterLevel.IsAllStarSkillUse = true;
                Sound.SoundManager.Instance.PlayEFF("se_common_boss_core_hit");
                AllStarSkillUse?.Invoke();
                CameraManager.SetAllStar(Character.transform);
                RoundManager.StaticSetInputSturnTime(1f);
                RoundManager.StaticStopMove(1f);

                while (diceQueue.Count > 0)
                {
                    diceQueue.Peek().transform.SetParent(null);
                    diceQueue.Peek().gameObject.SetActive(false);
                    PoolManager.AddObjToPool("Assets/Prefabs/Dice.prefab", diceQueue.Dequeue().gameObject);
                }

                RollDice();
                Character.StartCoroutine(DioTheWorld(diceQueue.Peek().DiceNumber));

                AllStarSkillAction();
            }
        }, EventType.KEY_DOWN);
    }

    public override void Start()
    {
        base.Start();
        RollDice();
    }

    private IEnumerator DioTheWorld(float time)
    {
        yield return new WaitForSeconds(1f);
        global::Character targetCharacter =
            Character.GetCharacterComponent<CharacterAttack>().TargetCharacterDamage.Character;

        Vector3 targetPos = new Vector3(targetCharacter.transform.position.x, targetCharacter.transform.position.y, targetCharacter.transform.position.z);

        float timer = 0;

        Vector3 pos = Character.transform.position;
        pos.y += 0.8f;
        var dice = Pool.PoolManager.GetItem("AllStarDice");
        dice.transform.position = pos;
        dice.gameObject.SetActive(true);
        dice.GetComponent<AllStarDice>().SetDeleteTime(time);

        CharacterInput input = targetCharacter.GetCharacterComponent<CharacterInput>();
        CharacterAIInput aiInput = targetCharacter.GetCharacterComponent<CharacterAIInput>();
        CharacterGravity gravity = targetCharacter.GetCharacterComponent<CharacterGravity>();
        CharacterMove move = targetCharacter.GetCharacterComponent<CharacterMove>();

        while (timer < time)
        {
            targetCharacter.transform.position = targetPos;
            
            if(input != null)
			{
                input.SetStunTime(1f);
            }
            if (aiInput != null)
            {
                aiInput.SetStunTime(1f);
            }
            gravity.SetHitTime(1f);
            move.SetSturnTime(1f);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void RollDice()
    {
        int random = Random.Range(1, 7);
        diceQueueAdd(random);
    }

    public void diceQueueAdd(int diceNum)
    {
        Dice dice = PoolManager.GetItem("Assets/Prefabs/Dice.prefab").GetComponent<Dice>();

        Transform dicePos = null;

        if (Character is Character_MythicalDice)
		{
            dicePos = (Character as Character_MythicalDice).DicePosition;

        }
        else if(Character as Character_MythicalDice_AI)
        {
            dicePos = (Character as Character_MythicalDice_AI).DicePosition;
        }
        dice.SetDice(diceNum, dicePos);

        diceQueue.Enqueue(dice);

        if (diceQueue.Count > 4)
        {
            diceQueue.Peek().transform.SetParent(null);
            diceQueue.Peek().gameObject.SetActive(false);
            PoolManager.AddObjToPool("Assets/Prefabs/Dice.prefab", diceQueue.Dequeue().gameObject);
        }
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

        if (diceQueue.Count < 4)
        {
            skillCoolTime2 = diceQueue.Count;
            skill2CoolTimeChange?.Invoke();
            isCanUseSkill2 = false;
        }
        else if (CharacterLevel.Level > 2 && diceQueue.Count == 4)
        {
            skillCoolTime2 = diceQueue.Count;
            skill2CoolTimeChange?.Invoke();
            isCanUseSkill2 = true;
        }


        if (!CharacterLevel.IsAllStarSkillUse && CharacterLevel.Level > 3)
        {
            isCanUseSkill3 = true;
        }

    }
}
