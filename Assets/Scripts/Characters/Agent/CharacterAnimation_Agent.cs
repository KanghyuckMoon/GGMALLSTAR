using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterAnimation_Agent : CharacterAnimation
{
    public CharacterAnimation_Agent(Character character) : base(character)
    {
    }

    protected override void Awake()
    {
        base.Awake();
        AddAnimationHash(AnimationType.Run, AnimationKeyWord.RUN);
        AddAnimationHash(AnimationType.Jump, AnimationKeyWord.JUMP);
        AddAnimationHash(AnimationType.Attack, AnimationKeyWord.ATTACK);
        AddAnimationHash(AnimationType.AllStarSkill, AnimationKeyWord.ALL_STAR_SKILL);
    }
}
