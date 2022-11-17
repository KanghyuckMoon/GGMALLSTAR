using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_MythicalDice : Character
{
    protected override void SetComponent()
    {
        AddComponent(ComponentType.Input, new CharacterInput(this));
        AddComponent(ComponentType.Move, new CharacterMove(this));
        AddComponent(ComponentType.Animation, new CharacterAnimation_MythicalDice(this));
        AddComponent(ComponentType.Sprite, new CharacterSprite(this));
        AddComponent(ComponentType.Attack, new CharacterAttack(this));
        AddComponent(ComponentType.Skill1, new CharacterSkill1_MythicalDice(this));
        AddComponent(ComponentType.Skill2, new CharacterSkill2_MythicalDice(this));
    }
}
