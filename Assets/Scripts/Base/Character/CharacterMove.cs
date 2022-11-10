using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterMove : CharacterComponent
{
    public CharacterMove(Character character, float speed = 7.5f) : base(character)
    {
        _speed = speed;

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = -1;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 1;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = -1;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 1;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = 0;
        }, EventType.KEY_UP);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 0;
        }, EventType.KEY_UP);
    }

    protected override void Awake()
    {
        _rigidbody = Character.GetComponent<Rigidbody>();
        _transform = Character.GetComponent<Transform>();
    }

    private Transform _transform = null;
    private Rigidbody _rigidbody = null;
    private float _speed = 7.5f;
    private Vector2 _moveDirection = Vector2.zero;

    public override void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveDirection.x * _speed, 0, 0);
        _moveDirection = Vector2.zero;
    }
}
