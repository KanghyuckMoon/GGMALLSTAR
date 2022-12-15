using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class Bullet_Damvi : MonoBehaviour
{
    private Character _character = null;
    private Vector3 _direction = Vector3.zero;
    private HitBoxData _hitBoxData;

    public void SetBullet(Character character, Vector3 direction, Vector3 position, HitBoxData hitBoxData)
    {
        _character = character;
        _hitBoxData = hitBoxData;
        _direction = direction;
        transform.position = position;

        if (_hitBoxData.atkEffSoundName != "")
        {
            Sound.SoundManager.Instance.PlayEFF(_hitBoxData.atkEffSoundName);
        }

        Vector3 offset = _hitBoxData.atkEffectOffset;

        if (hitBoxData.atkEffectType != Effect.EffectType.None)
        {
            Effect.EffectManager.Instance.SetEffect(hitBoxData.atkEffectType, transform.position, hitBoxData.atkEffectDirectionType, offset, _direction.x > 0 ? true : false);
        }
        transform.localEulerAngles = direction;


        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(_direction * 4f * Time.deltaTime);

            yield return null;
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
            characterAttack.TargetCharacterDamage?.OnAttcked(null, _hitBoxData, other.ClosestPoint(transform.position), _direction.x > 0 ? true : false);

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

            gameObject.SetActive(false);
            PoolManager.AddObjToPool("Assets/Prefabs/Fireball_Frog.prefab", gameObject);
        }
        
    }
    
    private void OnEnable()
    {
        StartCoroutine(SetActiveCoroutine());
    }

    private void OnDisable()
    {
        _character = null;
        _direction = Vector3.zero;
    }

    private IEnumerator SetActiveCoroutine(bool active = false)
    {
        yield return new WaitForSeconds(10f);
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(active);
            Pool.PoolManager.AddObjToPool("Bullet_Damvi", gameObject);
        }
    }
}
