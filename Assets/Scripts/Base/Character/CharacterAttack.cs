using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

public class CharacterAttack : CharacterComponent
{
    public CharacterAttack(Character character) : base(character)
    {

    }

    private Direction _direction = Direction.RIGHT;

    protected override void Awake()
    {
        _direction = Character.GetCharacterComponent<CharacterSprite>().Direction;
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, OnAttack, EventType.KEY_DOWN);
    }

    protected virtual void OnAttack()
    {
        HitBox hitBox = PoolManager.GetItem<HitBox>("HitBox");
        hitBox.Owner = Character.gameObject;
        hitBox.OnHit = () =>
        {
            Debug.Log("Hit");
        };

        switch (_direction)
        {
            case Direction.LEFT:
                hitBox.transform.position = Character.transform.position + new Vector3(-0.1f, 0.1f, 0);
                break;
            case Direction.RIGHT:
                hitBox.transform.position = Character.transform.position + new Vector3(0.1f, 0.1f, 0);
                break;
            default:
                Debug.LogWarning("Not Define Direction");
                break;
        }
        GameObject.Destroy(hitBox.gameObject, 0.1f);
    }
}
