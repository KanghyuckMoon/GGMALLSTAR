using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillSpike_Jaeby : MonoBehaviour
{
    private HitBoxData _hitBoxData;
    private Character _character;

    public void SetSkillSpike(Character character, Vector3 position, HitBoxData hitBoxData)
    {
        transform.position = position;
        _character = character;
        _hitBoxData = hitBoxData;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(ColliderTriggerCoroutine());
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.2f);
        Sound.SoundManager.Instance.PlayEFF("se_common_offset_sword");
        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Dirty_02, position);

        StartCoroutine(DeleteObejct());
    }

    private IEnumerator DeleteObejct()
    {
        yield return new WaitForSeconds(20f);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Sound.SoundManager.Instance.PlayEFF("se_common_offset_sword");
            Pool.PoolManager.AddObjToPool("Assets/Prefabs/SkillSpike_Jaeby.prefab", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _character.gameObject || other.gameObject.CompareTag("Invincibility"))
            return;

        if (!other.gameObject.CompareTag(_character.tag) && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")))
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

            gameObject.SetActive(false);
            Sound.SoundManager.Instance.PlayEFF("se_common_offset_sword");
            Pool.PoolManager.AddObjToPool("Assets/Prefabs/SkillSpike_Jaeby.prefab", gameObject);
        }

    }

    private void OnEnable()
    {
        StartCoroutine(ColliderTriggerCoroutine());
    }

    private void OnDisable()
    {
        GetComponent<Collider>().enabled = false;
    }

    private IEnumerator ColliderTriggerCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider>().enabled = true;
    }
}