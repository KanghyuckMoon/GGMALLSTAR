using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CharacterAIInput_Jaeby : CharacterAIInput
{
    public CharacterAIInput_Jaeby(Character character) : base(character)
    {
    }
    protected override void SetBehaviourTree()
    {
        _behaviourTree = new Jaeby_Behaviour();
        _behaviourTree.Init(opponentCharacter, Character, this);
    }
}

