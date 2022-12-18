using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CharacterAIInput_Puppet : CharacterAIInput
{
    public CharacterAIInput_Puppet(Character character) : base(character)
    {
    }
    
    protected override void SetBehaviourTree(bool isPlayer1)
    {
        _behaviourTree = new Puppet_Behaviour();
        _behaviourTree.Init(opponentCharacter, Character, this, isPlayer1 ? SelectDataSO.aiLevelP1 : SelectDataSO.aiLevelP2);
    }
}

