using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterComponent
{
    private Character _character = null;
    protected Character Character => _character;

    private CharacterEvent _characterEvent = null;
    protected CharacterEvent CharacterEvent => _characterEvent;

    public CharacterComponent(Character character)
    {
        _character = character;
        _characterEvent = character.CharacterEvent;
        Awake();
        SetEvent();
    }

    public CharacterComponent(Character character, CharacterEvent characterEvent)
    {
        _character = character;
        _characterEvent = characterEvent;
        Awake();
        SetEvent();
    }

    protected virtual void SetEvent() { }

    protected virtual void Awake() { }
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }

    public virtual void OnCollisionEnter(Collision other) { }
    public virtual void OnCollisionExit(Collision other) { }
}
