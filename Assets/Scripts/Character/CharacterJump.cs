using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.UP, Jump);
    }

    private bool _isJump = false;

    protected override void Awake()
    {
        _isJump = false;
    }

    private void Jump()
    {
        _isJump = !_isJump;
        if (_isJump)
        {
            return;
        }
        Debug.Log("Jump");
    }
}