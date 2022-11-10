using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Frog : Character
{
    protected override void SetComponent()
    {
        AddComponent(ComponentType.Stat, new CharacterStat(this));
        AddComponent(ComponentType.Damage, new CharacterDamage(this));
    }
}
