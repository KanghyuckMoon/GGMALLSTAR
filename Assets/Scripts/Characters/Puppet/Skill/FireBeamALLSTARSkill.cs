using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeamALLSTARSkill : Skill
{
    private Character _character = null;
    private HitBoxData _hitBoxData = null;

    public void SetFireBeamALLSTARSkill(Character character, HitBoxData hitBoxData, Vector3 position)
    {
        _character = character;
        transform.position = position;
        _hitBoxData = hitBoxData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _character.gameObject)
            return;

        if (!other.gameObject.CompareTag(_character.tag))
        {
            CharacterAttack characterAttack = _character.GetCharacterComponent<CharacterAttack>(ComponentType.Attack);
            characterAttack.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>(ComponentType.Damage);
            characterAttack.TargetCharacterDamage?.OnAttacked(null, _hitBoxData, other.ClosestPoint(transform.position), characterAttack.IsRight);

            //AI
            CharacterAIInput aiInput = characterAttack.Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            if (aiInput is not null)
            {
                aiInput.IsHit(_hitBoxData.actionName);
            }

            //Exp

            int expCount = (_hitBoxData.addExp / 5) + 1;

            for (int i = 0; i < expCount; ++i)
            {
                StarEffect starEffect = Pool.PoolManager.GetItem("StarEff").GetComponent<StarEffect>();
                starEffect.SetEffect(transform.position, _character.GetCharacterComponent<CharacterLevel>(ComponentType.Level), _hitBoxData.addExp / expCount);
            }
        }
    }

    private void OnDisable()
    {
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/FireBeamALLSTAR.prefab", gameObject);
    }
}
