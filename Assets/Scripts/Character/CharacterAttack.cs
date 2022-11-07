using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : CharacterComponent
{
    public CharacterAttack(Character character) : base(character)
    {

    }

    private void Attack()
    {
        Debug.Log("Attack");
    }


}
