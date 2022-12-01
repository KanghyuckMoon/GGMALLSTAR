using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSkill_MythicalDice : CharacterSkill
{
    private Queue<int> diceNumQueue = new();

    public CharacterSkill_MythicalDice(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            diceQueueAdd(Random.Range(1, 7));
            Character.HitBoxDataSO.hitBoxDatasList[0].hitBoxDatas[0].damage = diceNumQueue.Peek();
            Debug.Log(Character.HitBoxDataSO.hitBoxDatasList[0].hitBoxDatas[0].damage);
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (diceNumQueue.Count == 4)
            {
                int damage = 0;
                while (diceNumQueue.Count > 0)
                {
                    damage += diceNumQueue.Dequeue();
                }
                // damage 변수 만큼 데미지를 준다.
                Debug.Log(damage);
            }
            else
            {
                Debug.Log($"주사위가 {diceNumQueue.Count}개 있습니다.");
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            Debug.Log("AllStarSkill");
        }, EventType.KEY_DOWN);
    }

    public void diceQueueAdd(int diceNum)
    {
        diceNumQueue.Enqueue(diceNum);
        if (diceNumQueue.Count > 4)
        {
            diceNumQueue.Dequeue();
        }
    }
}
