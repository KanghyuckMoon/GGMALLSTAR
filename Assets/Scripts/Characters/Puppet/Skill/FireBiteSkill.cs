using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBiteSkill : Skill
{
    [SerializeField]
    private float _speed = 0.1f;

    private Character _character = null;
    private Vector3 _direction = Vector3.zero;

    public void SetFireBiteSkill(Character character, Vector3 position, Vector3 direction)
    {
        transform.position = position;
        _character = character;
        _direction = direction;

        if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/FireBite.prefab", gameObject);
    }
}
