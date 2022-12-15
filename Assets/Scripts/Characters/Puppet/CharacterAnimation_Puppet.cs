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
        base.Awake();
        AddAnimationHash(AnimationType.Run, AnimationKeyWord.RUN);
        AddAnimationHash(AnimationType.Attack, AnimationKeyWord.ATTACK);
        AddAnimationHash(AnimationType.Jump, AnimationKeyWord.JUMP);
    }
}
