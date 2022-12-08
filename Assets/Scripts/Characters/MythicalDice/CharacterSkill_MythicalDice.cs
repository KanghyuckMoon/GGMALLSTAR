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
            characterLevel ??= Character.GetCharacterComponent<CharacterLevel>();
            return characterLevel;
        }
    }

    private Queue<Dice> diceQueue = new();

    public CharacterSkill_MythicalDice(Character character) : base(character)
    {
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
                    while (diceQueue.Count > 0)
                    {
                        Debug.Log(diceQueue.Peek().DiceNumber);
                        diceQueue.Peek().gameObject.SetActive(false);
                        PoolManager.AddObjToPool("Assets/Prefabs/Dice.prefab", diceQueue.Peek().gameObject);
                        damage += diceQueue.Dequeue().DiceNumber;
                    }
                    Character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0].damage = damage;
                    PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0], _character.GetCharacterComponent<CharacterAttack>(),
                        () => { Debug.Log($"Hit damage: {damage}"); }, character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0]._attackSize, character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0]._attackOffset);

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
        global::Character targetCharacter =
            Character.GetCharacterComponent<CharacterAttack>().TargetCharacterDamage.Character;
        
        Vector3 targetPos = new Vector3(targetCharacter.transform.position.x , targetCharacter.transform.position.y, targetCharacter.transform.position.z);

        targetCharacter.CharacterEvent._canEvent = false;
        float timer = 0;

        while (timer < time)
        {
            targetCharacter.transform.position = targetPos;

            timer += Time.deltaTime;
            yield return null;
        }
        
        while (diceQueue.Count > 0)
        {
            diceQueue.Peek().transform.SetParent(null);
            diceQueue.Peek().gameObject.SetActive(false);
            PoolManager.AddObjToPool("Assets/Prefabs/Dice.prefab", diceQueue.Dequeue().gameObject);
        }
        targetCharacter.CharacterEvent._canEvent = true;
    }

    private void RollDice()
    {
        int random = Random.Range(1, 7);
        diceQueueAdd(random);
        Character.HitBoxDataSO.hitBoxDatasList[0].hitBoxDatas[0].damage = random;
    }

    public void diceQueueAdd(int diceNum)
    {
        Dice dice = PoolManager.GetItem("Assets/Prefabs/Dice.prefab").GetComponent<Dice>();
        
        dice.SetDice(diceNum, (Character as Character_MythicalDice).DicePosition);
        
        diceQueue.Enqueue(dice);
        
        if (diceQueue.Count > 4)
        {
            diceQueue.Peek().transform.SetParent(null);
            diceQueue.Peek().gameObject.SetActive(false);
            PoolManager.AddObjToPool("Assets/Prefabs/Dice.prefab", diceQueue.Dequeue().gameObject);
        }
        string str = "";
        
        foreach (var VARIABLE in diceQueue)
        {
            str += VARIABLE.DiceNumber + " ";
        }
        
        Debug.Log("DiceQueue Number: " + str);
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
