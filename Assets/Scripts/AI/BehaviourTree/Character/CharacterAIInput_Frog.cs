using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CharacterAIInput_Frog : CharacterAIInput
{
    public CharacterAIInput_Frog(Character character) : base(character)
    {
    }

	protected override void SetBehaviourTree()
    {
        _behaviourTree = new Frog_Behaviour();
        _behaviourTree.Init(opponentCharacter, Character, this);
    }
}

