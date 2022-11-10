using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character, float jumpPower = 50f) : base(character)
    {
        _jumpPower = jumpPower;

        CharacterEvent.AddEvent(EventKeyWord.UP, OnJump, EventType.KEY_DOWN);
    }

    protected override void Awake()
    {
        _rigidbody = Character.GetComponent<Rigidbody>();
        _groundLayerMask = LayerMask.GetMask("Ground");
    }

    private Rigidbody _rigidbody = null;

    private float _jumpPower = 50f;

    private bool _isGround = false;

    private LayerMask _groundLayerMask = default(LayerMask);
    public override void Update()
    {
        GroundCheck();
    }

    private void OnJump()
    {
        if (_isGround)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    private void GroundCheck()
    {
        _isGround = Physics.Raycast(Character.transform.position + new Vector3(0, 0.5f, 0), Vector3.down, 0.6f, _groundLayerMask);
    }
}