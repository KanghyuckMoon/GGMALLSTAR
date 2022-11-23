using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Frog : Character
{
    protected override void SetComponent()
    {
        AddComponent(ComponentType.Input, new CharacterInput(this));
        AddComponent(ComponentType.Stat, new CharacterStat(this));
        AddComponent(ComponentType.Sprite, new CharacterSprite(this));
        //AddComponent(ComponentType.Attack, new CharacterAttack(this));
        AddComponent(ComponentType.Gravity, new CharacterGravity(this));
        AddComponent(ComponentType.Animation, new CharacterAnimation_Frog(this));
        AddComponent(ComponentType.Move, new CharacterMove(this));
        AddComponent(ComponentType.Damage, new CharacterDamage(this));
        AddComponent(ComponentType.Jump, new CharacterJump(this));
    }
}
