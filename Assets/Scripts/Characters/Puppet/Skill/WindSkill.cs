using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSkill : Skill
{
    public void SetWindSkill(Vector3 position)
    {
        transform.position = position;
    }

    private void OnDisable()
    {
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/WindSkill.prefab", gameObject);
    }
}
