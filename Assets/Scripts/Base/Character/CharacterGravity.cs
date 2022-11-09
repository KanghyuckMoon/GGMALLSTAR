using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : CharacterComponent
{
    public CharacterGravity(Character character, float gravityScale = 17.5f) : base(character)
    {
        _gravityScale = gravityScale;
    }

    private Rigidbody _rigidbody = null;
    private float _gravityScale = 17.5f;

    protected override void Awake()
    {
        _rigidbody = Character.GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 gravity = _gravityScale * Physics.gravity;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
