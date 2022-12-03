using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterAnimation_Frog : CharacterAnimation
{
    public CharacterAnimation_Frog(Character character) : base(character)
    {
    }

    protected override void Awake()
    {
        base.Awake();
        AddAnimationHash(AnimationType.Run, AnimationKeyWord.RUN);
        AddAnimationHash(AnimationType.Jump, AnimationKeyWord.JUMP);
        AddAnimationHash(AnimationType.Attack, AnimationKeyWord.ATTACK);
        AddAnimationHash(AnimationType.Skill2, AnimationKeyWord.SKILL2);
    }

    protected override void SetEvent()
    {
        base.SetEvent();
        //CharacterEvent.AddEvent(EventKeyWord.LEFT, () => SetAnimationBool(AnimationHash[AnimationType.Walk], true), EventType.KEY_DOWN);
        //CharacterEvent.AddEvent(EventKeyWord.RIGHT, () => SetAnimationBool(AnimationHash[AnimationType.Walk], true), EventType.KEY_DOWN);
        //
        //CharacterEvent.AddEvent(EventKeyWord.LEFT, () => SetAnimationBool(AnimationHash[AnimationType.Walk], true), EventType.KEY_HOLD);
        //CharacterEvent.AddEvent(EventKeyWord.RIGHT, () => SetAnimationBool(AnimationHash[AnimationType.Walk], true), EventType.KEY_HOLD);
        //
        //CharacterEvent.AddEvent(EventKeyWord.LEFT, () => SetAnimationBool(AnimationHash[AnimationType.Walk], false), EventType.KEY_UP);
        //CharacterEvent.AddEvent(EventKeyWord.RIGHT, () => SetAnimationBool(AnimationHash[AnimationType.Walk], false), EventType.KEY_UP);
        //
        //CharacterEvent.AddEvent(EventKeyWord.ATTACK, () => SetAnimationTrigger(AnimationHash[AnimationType.Attack]), EventType.KEY_DOWN);
    }
}
