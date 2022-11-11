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
        _rigidbody = Character.GetComponent<Rigidbody>();
    }

    private Rigidbody _rigidbody = null;
    protected Rigidbody Rigidbody => _rigidbody;

    private float _jumpPower = 0f;
}
