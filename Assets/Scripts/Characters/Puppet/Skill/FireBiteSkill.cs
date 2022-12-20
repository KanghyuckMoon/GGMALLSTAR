using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBiteSkill : Skill
{
    [SerializeField]
    private float _speed = 0.1f;

    private Character _character = null;
    private HitBoxData _hitBoxData = null;

    private Vector3 _direction = Vector3.zero;

    public void SetFireBiteSkill(Character character, HitBoxData hitBoxData, Vector3 position, Vector3 direction)
    {
        transform.position = position;
        _character = character;
        _direction = direction;
        _hitBoxData = hitBoxData;

        if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_character != null && other.gameObject == _character.gameObject)
        {
            return;
        }

        if (!other.gameObject.CompareTag(_character.tag) && !other.gameObject.CompareTag("Invincibility") && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")))
        {
            CharacterAttack characterAttack = _character.GetCharacterComponent<CharacterAttack>(ComponentType.Attack);
            characterAttack.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>(ComponentType.Damage);
            characterAttack.TargetCharacterDamage?.OnAttacked(null, _hitBoxData, other.ClosestPoint(transform.position), characterAttack.IsRight);

            //Exp
            int expCount = (_hitBoxData.addExp / 5) + 1;

            for (int i = 0; i < expCount; ++i)
            {
                StarEffect starEffect = Pool.PoolManager.GetItem("StarEff").GetComponent<StarEffect>();
                starEffect.SetEffect(transform.position, _character.GetCharacterComponent<CharacterLevel>(ComponentType.Level), _hitBoxData.addExp / expCount);
            }
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
