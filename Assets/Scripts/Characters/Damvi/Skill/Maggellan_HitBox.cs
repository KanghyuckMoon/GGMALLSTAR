using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class Maggellan_HitBox : MonoBehaviour
{
    private Character _character = null;
    private Vector3 _direction = Vector3.zero;
    private HitBoxData _hitBoxData;

    public void SetMaggellan(Character character, HitBoxData hitBoxData)
    {
        _character = character;
        _hitBoxData = hitBoxData;
        if (_hitBoxData.atkEffSoundName != "")
        {
            Sound.SoundManager.Instance.PlayEFF(_hitBoxData.atkEffSoundName);
        }
        if (hitBoxData.atkEffectType != Effect.EffectType.None)
        {
            Effect.EffectManager.Instance.SetEffect(hitBoxData.atkEffectType, transform.position, hitBoxData.atkEffectDirectionType, hitBoxData.atkEffectOffset, _direction.x > 0 ? true : false);
        }

        Sound.SoundManager.Instance.PlayEFF("se_ingame_staffroll_bn_explo");
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
            characterAttack.TargetCharacterDamage?.OnAttacked(null, _hitBoxData, other.ClosestPoint(transform.position), _direction.x > 0 ? true : false);

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
}
