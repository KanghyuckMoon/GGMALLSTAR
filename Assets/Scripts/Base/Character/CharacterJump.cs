using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character, float jumpPower = 50f) : base(character)
    {
        _jumpPower = jumpPower;

        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            Debug.Log("Jump Start");

        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            Debug.Log("Jumping");
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            Debug.Log("Jump End");
        }, EventType.KEY_UP);
    }

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
    }

    private Rigidbody _rigidbody = null;

    private float _jumpPower = 0f;
    private float _jumpTime = 0f;
    private float _jumpTimeLimit = 0.5f;
}
