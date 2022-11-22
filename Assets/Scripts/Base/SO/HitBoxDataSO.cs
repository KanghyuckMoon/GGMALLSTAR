using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HitBoxDataSO : ScriptableObject
{
	public HitBoxData[] hitBoxDatas;
}

[System.Serializable]
public class HitBoxData
{
	public int damage = 10;
	public float knockBack = 10;
	public float knockAngle = 50;

	public Effect.EffectType effectType;
	public string effSoundName;
}
