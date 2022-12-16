using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

public class SlashSkill : Skill
{
    private float _speed = 1f;
    private Character _character = null;
    private Direction _direction = Direction.RIGHT;

    private HitBoxData _hitBoxData = null;

    public void SetSlashSkill(Character character, HitBoxData hitBoxData, Direction direction, float speed, Vector3 offset)
    {
        _character = character;
        _direction = direction;
        _speed = speed;
        _hitBoxData = hitBoxData;
        transform.position = _character.transform.position + offset;

        character.transform.localScale = new Vector3(_direction == Direction.RIGHT ? 0.5f : -0.5f, 0.5f, 1f);
    }

    private void Update()
    {
        transform.Translate((_direction == Direction.RIGHT ? Vector3.right : Vector3.left) * _speed * Time.deltaTime);
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
            characterAttack.TargetCharacterDamage?.OnAttcked(null, _hitBoxData, other.ClosestPoint(transform.position), characterAttack.IsRight);

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

    private void OnEnable()
    {
        StartCoroutine(SetActiveCoroutine(true));
    }

    private void OnDisable()
    {
        PoolManager.AddObjToPool("Assets/Prefabs/Agent_Slash.prefab", gameObject);
    }

    private IEnumerator SetActiveCoroutine(bool isEnable = false)
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(isEnable);
    }
}
