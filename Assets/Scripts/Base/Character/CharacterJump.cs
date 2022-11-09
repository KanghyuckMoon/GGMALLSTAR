using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character) : base(character)
    {
        CharacterEvent characterEvent = character.GetCharacterComponent<CharacterEvent>();
        characterEvent.AddEvent(EventKeyWord.UP, OnJump, EventType.KEY_DOWN);
    }

    protected override void Awake()
    {
        _rigidbody = Character.GetComponent<Rigidbody>();
        _groundLayerMask = LayerMask.GetMask("Ground");
    }

    private Rigidbody _rigidbody = null;

    private float _jumpPower = 100f;

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
            Debug.Log("Jump");
            _rigidbody.AddForce(Vector3.up * _jumpPower);
        }
    }

    private void GroundCheck()
    {
        Debug.Log("GroundCheck " + _isGround);
        _isGround = Physics.Raycast(Character.transform.position + new Vector3(0, 0.5f, 0), Vector3.down, 0.6f, _groundLayerMask);
    }
}