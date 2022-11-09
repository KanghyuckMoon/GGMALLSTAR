using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterComponent
{
    private Character _character;
    protected Character Character => _character;

    public CharacterComponent(Character character)
    {
        _character = character;
        Awake();
    }

    protected virtual void Awake() { }
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }

    public virtual void OnCollisionExit(Collision collision) { }
}
