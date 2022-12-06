using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Jaeby : MonoBehaviour
{
    private Character _character = null;
    private Direction _direction = Direction.NONE;
    private HitBoxData _hitBoxData;

    public void SetSkillProjectile(Character character, Direction direction, Vector3 position, HitBoxData hitBoxData)
    {
        _character = character;
        _direction = direction;
        _hitBoxData = hitBoxData;
        transform.position = position;

        if (_hitBoxData.atkEffSoundName != "")
        {
            Sound.SoundManager.Instance.PlayEFF(_hitBoxData.atkEffSoundName);
        }

        StartCoroutine(Move());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _character.gameObject || other.gameObject.CompareTag("Invincibility"))
            return;

        if (!other.gameObject.CompareTag(_character.tag))
        {
            CharacterAttack characterAttack = _character.GetCharacterComponent<CharacterAttack>();
            characterAttack.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>();
            characterAttack.TargetCharacterDamage?.OnAttcked(null, _hitBoxData, other.ClosestPoint(transform.position), characterAttack.IsRight);

            //AI
            CharacterAIInput aiInput = characterAttack.Character.GetCharacterComponent<CharacterAIInput>();
            if (aiInput is not null)
            {
                aiInput.IsHit(_hitBoxData.actionName);
            }

            //Exp

            int expCount = (_hitBoxData.addExp / 5) + 1;

            for (int i = 0; i < expCount; ++i)
            {
                StarEffect starEffect = Pool.PoolManager.GetItem("StarEff").GetComponent<StarEffect>();
                starEffect.SetEffect(transform.position, _character.GetCharacterComponent<CharacterLevel>(), _hitBoxData.addExp / expCount);
            }
        }

        gameObject.SetActive(false);
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/SkillProjectile_Jaeby.prefab", gameObject);
    }

    private IEnumerator Move()
    {
        while (true)
        {
            switch (_direction)
            {
                case Direction.LEFT:
                    transform.localScale = new Vector3(-1, 1, 1);
                    transform.Translate(Vector3.left * 5f * Time.deltaTime);
                    break;
                case Direction.RIGHT:
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.Translate(Vector3.right * 5f * Time.deltaTime);
                    break;
            }
            yield return null;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SetActiveCoroutine());
    }

    private void OnDisable()
    {
        _character = null;
        _direction = Direction.NONE;
    }

    private IEnumerator SetActiveCoroutine(bool active = false)
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(active);
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/SkillProjectile_Jaeby.prefab", gameObject);
    }
}
