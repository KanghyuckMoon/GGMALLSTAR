using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HitBoxDataSO : ScriptableObject
{
	public HitBoxDatas[] hitBoxDatasList;
}

[System.Serializable]
public class HitBoxDatas
{
	public HitBoxData[] hitBoxDatas;
}

[System.Serializable]
public class HitBoxData
{
	//Action Name
	public string actionName = "";

	//Stat
	public int damage = 10;
	public float knockBack = 10;
	public float knockAngle = 50;
	public float hitTime = 0.3f;
	public float sturnTime = 0.3f;

	//pos scale
	public Vector3 _attackOffset = Vector3.zero;
	public Vector3 _attackSize = Vector3.zero;

	//Shake
	public float shakePower = 20f;
	public float shakeTime = 0.3f;

	//Effect & Sound
	public Effect.EffectType effectType;
	public string hitEffSoundName;
	public string atkEffSoundName;

	//Exp
	public int addExp;
}
