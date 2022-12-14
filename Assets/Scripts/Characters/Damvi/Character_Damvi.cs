using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Damvi : Character
{
    protected override void SetComponent()
    {
        AddComponent(ComponentType.Input, new CharacterInput(this));
        AddComponent(ComponentType.Stat, new CharacterStat(this));
        AddComponent(ComponentType.Sprite, new CharacterSprite_Damvi(this));
        AddComponent(ComponentType.Attack, new CharacterAttack(this));
        AddComponent(ComponentType.Gravity, new CharacterGravity(this));
        AddComponent(ComponentType.Animation, new CharacterAnimation_Damvi(this));
        AddComponent(ComponentType.Move, new CharacterMove(this));
        AddComponent(ComponentType.Dodge, new CharacterDodge(this));
        AddComponent(ComponentType.Damage, new CharacterDamage(this));
        AddComponent(ComponentType.Jump, new CharacterJump(this));
        AddComponent(ComponentType.Level, new CharacterLevel(this));
        AddComponent(ComponentType.Skill1, new CharacterSkill_Damvi(this));
        AddComponent(ComponentType.Color, new CharacterColor_Damvi(this));
        AddComponent(ComponentType.Debug, new CharacterDebug(this));
    }
}
