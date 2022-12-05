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
            int random = Random.Range(1, 7);
            diceQueueAdd(random);
            Character.HitBoxDataSO.hitBoxDatasList[0].hitBoxDatas[0].damage = random;
            Debug.Log(Character.HitBoxDataSO.hitBoxDatasList[0].hitBoxDatas[0].damage);
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (diceQueue.Count == 4)
            {
                int damage = 0;
                while (diceQueue.Count > 0)
                {
                    damage += diceQueue.Dequeue().DiceNumber;
                }
                // damage 변수 만큼 데미지를 준다.
            }
            else
            {
                Debug.Log($"주사위가 {diceQueue.Count}개 있습니다.");
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            Debug.Log("AllStarSkill");
        }, EventType.KEY_DOWN);
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
