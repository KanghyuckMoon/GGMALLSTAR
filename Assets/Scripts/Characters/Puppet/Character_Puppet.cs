using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Puppet : Character
{
    protected override void SetComponent()
    {
        base.SetComponent();
        AddComponent(ComponentType.Input, new CharacterInput(this));
        AddComponent(ComponentType.Animation, new CharacterAnimation_Puppet(this));
        AddComponent(ComponentType.Stat, new CharacterStat(this));
        AddComponent(ComponentType.Move, new CharacterMove(this));
        AddComponent(ComponentType.Sprite, new CharacterSprite(this));
        AddComponent(ComponentType.Attack, new CharacterAttack(this));
    }
}
