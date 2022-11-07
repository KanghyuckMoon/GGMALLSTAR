using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterComponent
{
    private Character _character;
    public CharacterComponent(Character character)
    {
        _character = character;
    }

    public Character Character => _character;

    public virtual void Update() { }
}
