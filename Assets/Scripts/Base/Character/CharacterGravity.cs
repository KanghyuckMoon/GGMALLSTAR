using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : CharacterComponent
{
    public CharacterGravity(Character character, float gravityScale = 9.8f) : base(character)
    {
        _gravityScale = gravityScale;
    }

    private Rigidbody _rigidbody = null;
    private readonly float _gravityScale = 0f;

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
        _rigidbody.useGravity = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 gravity = _gravityScale * Physics.gravity;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
