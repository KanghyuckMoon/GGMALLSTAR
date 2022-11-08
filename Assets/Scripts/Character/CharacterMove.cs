using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : CharacterComponent
{
    public CharacterMove(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveLeft = _moveLeft ? false : true;
            _moveRight = false;
        });
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveRight = _moveRight ? false : true;
            _moveLeft = false;
        });
    }

    protected override void Awake()
    {
        _rigidbody = Character.GetComponent<Rigidbody>();
        _transform = Character.GetComponent<Transform>();
    }

    private Transform _transform = null;
    private Rigidbody _rigidbody = null;
    private Vector2 _moveDirection = Vector2.zero;

    private bool _moveRight = false;
    private bool _moveLeft = false;

    public override void Update()
    {
        if (_moveRight)
        {
            _moveDirection.x = 1;
        }
        else if (_moveLeft)
        {
            _moveDirection.x = -1;
        }
        else
        {
            _moveDirection.x = 0;
        }
        _moveDirection.y = _rigidbody.velocity.y;
    }

    public override void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveDirection.x, 0, 0);
        _moveDirection = Vector2.zero;
    }
}
