using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : CharacterComponent
{
    public CharacterMove(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.LEFT, MoveLeft);
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.RIGHT, MoveRight);
    }

    public override void Awake()
    {
        _rigidbody = Character.GetComponent<Rigidbody>();
        _transform = Character.GetComponent<Transform>();
    }

    private Transform _transform = null;
    private Rigidbody _rigidbody = null;
    private Vector2 _moveDirection = Vector2.zero;

    private void MoveRight()
    {
        _moveDirection.x = 1;
    }

    private void MoveLeft()
    {
        _moveDirection.x = -1;
    }

    public override void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveDirection.x, 0, 0);
        _moveDirection = Vector2.zero;
    }
}
