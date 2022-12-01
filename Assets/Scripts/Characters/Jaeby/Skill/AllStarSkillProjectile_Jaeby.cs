using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllStarSkillProjectile_Jaeby : MonoBehaviour
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
        Debug.Log(other.gameObject);
        Debug.Log(_character.gameObject);

        if (other.gameObject == _character.gameObject)
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
    }

    private IEnumerator Move()
    {
        while (true)
        {
            switch (_direction)
            {
                case Direction.LEFT:
                    transform.localScale = new Vector3(-5, 5, 1);
                    transform.Translate(Vector3.left * 2f * Time.deltaTime);
                    break;
                case Direction.RIGHT:
                    transform.localScale = new Vector3(5, 5, 1);
                    transform.Translate(Vector3.right * 2f * Time.deltaTime);
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
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/ALL_STAR_SKILL_Projectile_Jaeby.prefab", gameObject);
    }
}
