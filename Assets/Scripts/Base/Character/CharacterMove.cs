using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : CharacterComponent
{
    public CharacterMove(Character character) : base(character)
    {
        CharacterEvent characterEvent = character.GetCharacterComponent<CharacterEvent>();

        characterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = -1;
        }, EventType.KEY_DOWN);

        characterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 1;
        }, EventType.KEY_DOWN);

        characterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = -1;
        }, EventType.KEY_HOLD);

        characterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 1;
        }, EventType.KEY_HOLD);

        characterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = 0;
        }, EventType.KEY_UP);

        characterEvent.AddEvent(EventKeyWord.RIGHT, () =>
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
    private Vector2 _moveDirection = Vector2.zero;

    public override void Update()
    {
        _moveDirection.y = _rigidbody.velocity.y;
    }

    public override void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveDirection.x * 10, 0, 0);
        _moveDirection = Vector2.zero;
    }
}
