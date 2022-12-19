using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new HitBoxData")]
public class HitBoxDataSO : ScriptableObject
{
    public HitBoxDatas[] hitBoxDatasList;

    public void Copy(ref HitBoxDataSO hitBoxDataSO)
	{
        hitBoxDataSO.hitBoxDatasList = this.hitBoxDatasList;
        for(int i = 0; i < this.hitBoxDatasList.Length; ++i)
		{
            for(int j = 0; j < this.hitBoxDatasList[i].hitBoxDatas.Length; ++j)
			{
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j] = this.hitBoxDatasList[i].hitBoxDatas[j];
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].actionName = this.hitBoxDatasList[i].hitBoxDatas[j].actionName;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].damage = this.hitBoxDatasList[i].hitBoxDatas[j].damage;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].knockBack = this.hitBoxDatasList[i].hitBoxDatas[j].knockBack;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].knockAngle = this.hitBoxDatasList[i].hitBoxDatas[j].knockAngle;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].hitTime = this.hitBoxDatasList[i].hitBoxDatas[j].hitTime;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].sturnTime = this.hitBoxDatasList[i].hitBoxDatas[j].sturnTime;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j]._attackOffset = this.hitBoxDatasList[i].hitBoxDatas[j]._attackOffset;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j]._attackSize = this.hitBoxDatasList[i].hitBoxDatas[j]._attackSize;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].shakePower = this.hitBoxDatasList[i].hitBoxDatas[j].shakePower;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].shakeTime = this.hitBoxDatasList[i].hitBoxDatas[j].shakeTime;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].atkEffectType = this.hitBoxDatasList[i].hitBoxDatas[j].atkEffectType;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].atkEffectDirectionType = this.hitBoxDatasList[i].hitBoxDatas[j].atkEffectDirectionType;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].atkEffectOffset = this.hitBoxDatasList[i].hitBoxDatas[j].atkEffectOffset;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].hitEffectType = this.hitBoxDatasList[i].hitBoxDatas[j].hitEffectType;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].hitEffectDirectionType = this.hitBoxDatasList[i].hitBoxDatas[j].hitEffectDirectionType;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].hitEffSoundName = this.hitBoxDatasList[i].hitBoxDatas[j].hitEffSoundName;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].atkEffSoundName = this.hitBoxDatasList[i].hitBoxDatas[j].atkEffSoundName;
                hitBoxDataSO.hitBoxDatasList[i].hitBoxDatas[j].addExp = this.hitBoxDatasList[i].hitBoxDatas[j].addExp;
            }
        }
    }

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
