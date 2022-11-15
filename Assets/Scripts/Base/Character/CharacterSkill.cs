using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : CharacterComponent
{
    public CharacterSkill(Character character):base(character)
    {
        Debug.Log("CharacterSkill");
    }
}
