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
    protected override void SetBehaviourTree(bool isPlayer1)
    {
        _behaviourTree = new Jaeby_Behaviour();
        _behaviourTree.Init(opponentCharacter, Character, this, isPlayer1 ? SelectDataSO.aiLevelP1 : SelectDataSO.aiLevelP2);
    }
}

