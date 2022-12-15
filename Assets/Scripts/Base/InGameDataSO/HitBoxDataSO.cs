using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new HitBoxData")]
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
    public Effect.EffectType atkEffectType;
    public Effect.EffectDirectionType atkEffectDirectionType;
    public Vector3 atkEffectOffset = Vector3.zero;
    public Effect.EffectType hitEffectType;
    public Effect.EffectDirectionType hitEffectDirectionType;
    public string hitEffSoundName;
    public string atkEffSoundName;

    //Exp
    public int addExp;
}
