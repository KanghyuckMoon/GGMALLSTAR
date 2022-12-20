using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

public class CharacterOp : CharacterComponent
{
    public CharacterOp(Character character) : base(character)
    {
        Optimization();
    }

    /// <summary>
    /// 최적화를 위해 만들었던 함수 오히려 더 느려져서 사용 안하는 중
    /// </summary>
    protected virtual void Optimization()
    {
        foreach (var hitBoxDatas in Character.HitBoxDataSO.hitBoxDatasList)
        {
            foreach (var hitBoxData in hitBoxDatas.hitBoxDatas)
            {
                PoolManager.CreatePool(hitBoxData.hitEffectType.ToString());
                PoolManager.CreatePool(hitBoxData.atkEffectType.ToString());
            }
        }
    }
}
