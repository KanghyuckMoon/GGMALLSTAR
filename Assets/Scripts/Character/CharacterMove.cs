using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : CharacterComponent
{
    public CharacterMove(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.LEFT, Move);
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.RIGHT, Move);
    }

    public void Move()
    {
        Debug.Log("Move");
    }
}
