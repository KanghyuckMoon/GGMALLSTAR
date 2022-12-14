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

    public void SetEarthGolem(Character character, Vector3 position, Direction direction)
    {
        _animator = GetComponent<Animator>();

        transform.position = position;
        _character = character;

        _direction = direction;

        if (_direction == Direction.LEFT)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_direction == Direction.RIGHT)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        _character.CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (_character.GetCharacterComponent<CharacterSkill_Puppet>().CurrentElementalType == CharacterSkill_Puppet.ElementalType.Earth)
            {
                _animator.SetTrigger("Attack");
            }
        }, EventType.KEY_DOWN);
    }

    private void OnDisable()
    {
        _character.GetCharacterComponent<CharacterSkill_Puppet>().IsEarthGolemSpawn = false;
    }
}
