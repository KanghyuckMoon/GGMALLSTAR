using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterP2Color : CharacterComponent
{
    public CharacterP2Color(Character character) : base(character)
    {

    }

    public abstract void SetP2Color();
}
