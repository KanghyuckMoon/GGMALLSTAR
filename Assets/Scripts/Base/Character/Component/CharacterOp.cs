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

	protected virtual void Optimization()
	{
		foreach(var hitBoxDatas in Character.HitBoxDataSO.hitBoxDatasList)
		{
			foreach(var hitBoxData in hitBoxDatas.hitBoxDatas)
			{
				PoolManager.CreatePool(hitBoxData.hitEffectType.ToString());
				PoolManager.CreatePool(hitBoxData.atkEffectType.ToString());
			}
		}
	}
}
