using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation_Jaeby : CharacterAnimation
{
    public CharacterAnimation_Jaeby(Character character) : base(character) { }
    int _attackHash;
    int _isRunHash;
    int _jumpHash;

    protected override void Awake()
    {
        base.Awake();
        _attackHash = Animator.StringToHash("Attack");
        _isRunHash = Animator.StringToHash("IsRun");
        _jumpHash = Animator.StringToHash("Jump");
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.ATTACK, () => _animator.SetTrigger(_attackHash), EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.UP, () => _animator.SetTrigger(_jumpHash), EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () => _animator.SetBool(_isRunHash, true), EventType.KEY_DOWN);
        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () => _animator.SetBool(_isRunHash, true), EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () => _animator.SetBool(_isRunHash, true), EventType.KEY_HOLD);
        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () => _animator.SetBool(_isRunHash, true), EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () => _animator.SetBool(_isRunHash, false), EventType.KEY_UP);
        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () => _animator.SetBool(_isRunHash, false), EventType.KEY_UP);
    }
}
