using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class EarthGolem : Skill
{
    private Character _character = null;

    private Animator _animator = null;

    private Direction _direction = Direction.RIGHT;

    private HitBoxData _hitBoxData = null;

    public void SetEarthGolem(Character character, HitBoxData hitBoxData, Vector3 position, Direction direction)
    {
        _animator = GetComponent<Animator>();

        transform.position = position;
        _character = character;

        _direction = direction;
        _hitBoxData = hitBoxData;

        if (_direction == Direction.LEFT)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_direction == Direction.RIGHT)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        _character.CharacterEvent.AddEvent(EventKeyWord.SKILL_2, Attack, EventType.KEY_DOWN);
    }

    private void Attack()
    {
        if (_character.GetCharacterComponent<CharacterSkill_Puppet>().CurrentElementalType == CharacterSkill_Puppet.ElementalType.Earth)
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        Pool.PoolManager.GetItem("HitBox").GetComponent<HitBox>().SetHitBox(_hitBoxData, _character.GetCharacterComponent<CharacterAttack>(), null, _hitBoxData._attackSize, _direction == Direction.RIGHT ? _hitBoxData._attackOffset : -_hitBoxData.atkEffectOffset);
    }

    private void OnDisable()
    {
        _character.GetCharacterComponent<CharacterSkill_Puppet>().IsEarthGolemSpawn = false;
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/EarthGolem.prefab", gameObject);
        _character.CharacterEvent.RemoveEvent(EventKeyWord.SKILL_2, Attack, EventType.KEY_DOWN);
    }
}
