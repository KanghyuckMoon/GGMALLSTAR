using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterAnimation_Puppet : CharacterAnimation
{
    public CharacterAnimation_Puppet(Character character) : base(character)
    {
    }

    protected override void Awake()
    {
        AddAnimationHash(AnimationType.Run, AnimationKeyWord.RUN);
    }
}
