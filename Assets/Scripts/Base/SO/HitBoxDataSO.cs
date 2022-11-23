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
	public float hitTime = 0.3f;
	public float sturnTime = 0.3f;

	public Effect.EffectType effectType;
	public string hitEffSoundName;
	public string atkEffSoundName;
}
