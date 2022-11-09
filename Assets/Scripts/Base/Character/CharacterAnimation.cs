using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : CharacterComponent
{
    public CharacterAnimation(Character character) : base(character)
    {
        CharacterEvent characterEvent = Character.GetCharacterComponent<CharacterEvent>();

        characterEvent.AddEvent(EventKeyWord.ATTACK, () => { _animator.SetTrigger(_attackHash); }, EventType.KEY_DOWN);

        characterEvent.AddEvent(EventKeyWord.LEFT, () => { _animator.SetBool(_isRunHash, true); }, EventType.KEY_DOWN);
        characterEvent.AddEvent(EventKeyWord.RIGHT, () => { _animator.SetBool(_isRunHash, true); }, EventType.KEY_DOWN);

        characterEvent.AddEvent(EventKeyWord.LEFT, () => { _animator.SetBool(_isRunHash, true); }, EventType.KEY_HOLD);
        characterEvent.AddEvent(EventKeyWord.RIGHT, () => { _animator.SetBool(_isRunHash, true); }, EventType.KEY_HOLD);

        characterEvent.AddEvent(EventKeyWord.LEFT, () => { _animator.SetBool(_isRunHash, false); }, EventType.KEY_UP);
        characterEvent.AddEvent(EventKeyWord.RIGHT, () => { _animator.SetBool(_isRunHash, false); }, EventType.KEY_UP);
    }

    private Animator _animator = null;

    int _attackHash;
    int _isRunHash;

    protected override void Awake()
    {
        _animator = Character.GetComponentInChildren<Animator>();
        _attackHash = Animator.StringToHash("Attack");
        _isRunHash = Animator.StringToHash("IsRun");
    }
}
