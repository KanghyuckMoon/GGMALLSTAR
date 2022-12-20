using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSkill : Skill
{
    private Character _character = null;
    private HitBoxData _hitBoxData;

    public void SetWindSkill(Character character, HitBoxData hitBoxData, Vector3 position)
    {
        _character = character;
        _hitBoxData = hitBoxData;
        transform.position = position;
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

            gameObject.SetActive(false);
            Pool.PoolManager.AddObjToPool("Assets/Prefabs/WindSkill.prefab", gameObject);
        }
    }

    private void OnDisable()
    {
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/WindSkill.prefab", gameObject);
    }
}
