using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Agent : Character
{
    protected override void SetComponent()
    {
        AddComponent(ComponentType.Input, new CharacterInput(this));
        AddComponent(ComponentType.Animation, new CharacterAnimation_Agent(this));
        AddComponent(ComponentType.Debug, new CharacterDebug(this));
        AddComponent(ComponentType.Stat, new CharacterStat(this));
        AddComponent(ComponentType.Sprite, new CharacterSprite(this));
        AddComponent(ComponentType.Move, new CharacterMove(this));
        AddComponent(ComponentType.Attack, new CharacterAttack(this));
        AddComponent(ComponentType.Damage, new CharacterDamage(this));
        AddComponent(ComponentType.Jump, new CharacterJump(this));
        AddComponent(ComponentType.Gravity, new CharacterGravity(this));
        AddComponent(ComponentType.Skill1, new CharacterSkill_Agent(this));
        AddComponent(ComponentType.Color, new CharacterColor_Jaeby(this));
    }
}
