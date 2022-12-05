using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_MythicalDice : Character
{
    protected override void SetComponent()
    {
        AddComponent(ComponentType.Input, new CharacterInput(this));
        AddComponent(ComponentType.Animation, new CharacterAnimation_MythicalDice(this));
        AddComponent(ComponentType.Move, new CharacterMove(this));
        AddComponent(ComponentType.Jump, new CharacterJump(this));
        AddComponent(ComponentType.Sprite, new CharacterSprite(this));
        AddComponent(ComponentType.Attack, new CharacterAttack(this));
        AddComponent(ComponentType.Skill1, new CharacterSkill_MythicalDice(this));
    }
    
    [Header("MythicalDice")]
    [SerializeField]
    private Transform _dicePosition;

    public Transform DicePosition => _dicePosition;
}
