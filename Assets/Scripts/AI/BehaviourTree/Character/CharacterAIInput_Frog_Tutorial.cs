using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CharacterAIInput_Frog_Tutorial : CharacterAIInput
{
    public CharacterAIInput_Frog_Tutorial(Character character) : base(character)
    {
    }

	protected override void SetBehaviourTree(bool isPlayer1)
    {
        _behaviourTree = new Frog_Behaviour();
        _behaviourTree.Init(opponentCharacter, Character, this, isPlayer1 ? SelectDataSO.aiLevelP1 : SelectDataSO.aiLevelP2);
    }
}

