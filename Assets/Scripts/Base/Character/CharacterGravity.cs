using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : CharacterComponent
{
    public CharacterGravity(Character character) : base(character)
    {

    }

    private Rigidbody _rigidbody = null;
    private float _hitTime = 0f;

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
        _rigidbody.useGravity = false;
    }

    public void SetHitTime(float time)
	{
        this._hitTime = time;
    }

    public override void FixedUpdate()
    {
        if (_hitTime > 0f)
		{
            _hitTime -= Time.fixedDeltaTime;
            return;
		}

        base.FixedUpdate();
        Vector3 gravity = Character.CharacterSO.GravityScale * Physics.gravity;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
