using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CharacterAIInput_Agent : CharacterAIInput
{
    public CharacterAIInput_Agent(Character character) : base(character)
    {
    }
    
    protected override void SetBehaviourTree(bool isPlayer1)
    {
        _behaviourTree = new Agent_Behaviour();
        _behaviourTree.Init(opponentCharacter, Character, this, isPlayer1 ? SelectDataSO.aiLevelP1 : SelectDataSO.aiLevelP2);
    }
}

