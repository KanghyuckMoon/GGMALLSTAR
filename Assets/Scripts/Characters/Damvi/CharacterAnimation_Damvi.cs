using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterAnimation_Damvi : CharacterAnimation
{
    public CharacterAnimation_Damvi(Character character) : base(character)
    {
    }

    protected override void Awake()
    {
        base.Awake();
        AddAnimationHash(AnimationType.Run, AnimationKeyWord.RUN);
        AddAnimationHash(AnimationType.Jump, AnimationKeyWord.JUMP);
        AddAnimationHash(AnimationType.Attack, AnimationKeyWord.ATTACK);
        AddAnimationHash(AnimationType.Damage, AnimationKeyWord.DAMAGE);
        AddAnimationHash(AnimationType.Dodge, AnimationKeyWord.DODGE);
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
