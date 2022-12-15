using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class Maggellan_Damvi : MonoBehaviour
{
    [SerializeField] private Maggellan_HitBox maggellan_HitBox;
    private Character _character = null;
    private Vector3 _direction = Vector3.zero;
    private HitBoxData _hitBoxData;

    public void SetMaggellan(Character character, Vector3 direction, Vector3 position, HitBoxData hitBoxData)
    {
        _character = character;
        _hitBoxData = hitBoxData;
        _direction = direction;
        transform.position = position + hitBoxData._attackOffset;
        if (_hitBoxData.atkEffSoundName != "")
        {
            Sound.SoundManager.Instance.PlayEFF(_hitBoxData.atkEffSoundName);
        }

        Vector3 offset = _hitBoxData.atkEffectOffset;

        if (hitBoxData.atkEffectType != Effect.EffectType.None)
        {
            Effect.EffectManager.Instance.SetEffect(hitBoxData.atkEffectType, transform.position, hitBoxData.atkEffectDirectionType, offset, _direction.x > 0 ? true : false);
        }

        Sound.SoundManager.Instance.PlayEFF("se_item_revengeshooter_shot");
        maggellan_HitBox.SetMaggellan(_character, _hitBoxData);
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
        yield return new WaitForSeconds(5f);
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(active);
            Pool.PoolManager.AddObjToPool("Maggellan_Damvi", gameObject);
        }
    }
}
