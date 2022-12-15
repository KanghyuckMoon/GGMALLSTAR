using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeamALLSTARSkill : Skill
{
    private Character _character = null;

    public void SetFireBeamALLSTARSkill(Character character, Vector3 position)
    {
        _character = character;
        transform.position = position;
    }

    private void OnDisable()
    {
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/FireBeamALLSTAR.prefab", gameObject);
    }
}
