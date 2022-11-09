using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Jaeby : Character
{
    protected override void SetComponent()
    {
        base.SetComponent();
        AddComponent(ComponentType.Attack, new CharacterAttack_Jaeby(this));
        AddComponent(ComponentType.Gravity, new CharacterGravity(this));
        AddComponent(ComponentType.Animation, new CharacterAnimation_Jaeby(this));
        AddComponent(ComponentType.Move, new CharacterMove(this));
        AddComponent(ComponentType.Jump, new CharacterJump(this));
        AddComponent(ComponentType.Sprite, new CharacterSprite(this));
    }
}
