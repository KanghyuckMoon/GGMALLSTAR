using System.Collections;
using System.Collections.Generic;
using KeyWord;
using UnityEngine;

public class CharacterAnimation_MythicalDice : CharacterAnimation
{
    public CharacterAnimation_MythicalDice(Character character) : base(character)
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        AddAnimationHash(AnimationType.Run, AnimationKeyWord.RUN);
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.LEFT, ()=>SetAnimationBool(AnimationHash[AnimationType.Run], true), EventType.KEY_DOWN);
        CharacterEvent.AddEvent(EventKeyWord.LEFT, ()=>SetAnimationBool(AnimationHash[AnimationType.Run], true), EventType.KEY_HOLD);
        CharacterEvent.AddEvent(EventKeyWord.LEFT, ()=>SetAnimationBool(AnimationHash[AnimationType.Run], false), EventType.KEY_UP);
        
        CharacterEvent.AddEvent(EventKeyWord.RIGHT, ()=>SetAnimationBool(AnimationHash[AnimationType.Run], true), EventType.KEY_DOWN);
        CharacterEvent.AddEvent(EventKeyWord.RIGHT, ()=>SetAnimationBool(AnimationHash[AnimationType.Run], true), EventType.KEY_HOLD);
        CharacterEvent.AddEvent(EventKeyWord.RIGHT, ()=>SetAnimationBool(AnimationHash[AnimationType.Run], false), EventType.KEY_UP);
    }
}
