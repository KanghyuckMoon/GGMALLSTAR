using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Puppet : Character
{
    protected override void SetComponent()
    {
        base.SetComponent();
        AddComponent(ComponentType.Input, new CharacterInput(this));

        AddComponent(ComponentType.Move, new CharacterMove(this));

    }
}
