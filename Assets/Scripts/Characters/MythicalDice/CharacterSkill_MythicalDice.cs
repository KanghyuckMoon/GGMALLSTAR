using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Pool;

public class CharacterSkill_MythicalDice : CharacterSkill
{
    private Queue<Dice> diceQueue = new();

    public CharacterSkill_MythicalDice(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            // TODO: 레벨 제한 + 스킬쿨 적용 필요
            RollDice();
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (diceQueue.Count == 4)
            {
                int damage = 0;
                while (diceQueue.Count > 0)
                {
                    diceQueue.Peek().gameObject.SetActive(false);
                    PoolManager.AddObjToPool("Assets/Prefabs/Dice.prefab", diceQueue.Peek().gameObject);
                    damage += diceQueue.Dequeue().DiceNumber;
                }
                Character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0].damage = damage;
                PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0], _character.GetCharacterComponent<CharacterAttack>(),
                    () => { }, character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0]._attackSize, character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0]._attackOffset);
            }
            else
            {
                Debug.Log($"주사위가 {diceQueue.Count}개 있습니다.");
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            Debug.Log("AllStarSkill");
            for (int i = 0; i < 4; ++i)
            {
                RollDice();
            }
        }, EventType.KEY_DOWN);
    }

    public override void Start()
    {
        base.Start();
        Debug.Log("start roll");
        RollDice();
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
        
    }
}
