using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character) : base(character)
    {
        CharacterEvent characterEvent = character.GetCharacterComponent<CharacterEvent>();


    }

    protected override void Awake()
    {
        _rigidbody = Character.GetComponent<Rigidbody>();
    }

    private Rigidbody _rigidbody = null;

    private float _jumpPower = 100f;

    private bool _isJumpable = true;
    private bool _isGround = false;

    private float _maxAcceleration = 1f;
    private float _accelerationPower = 0.06f;

    private LayerMask _groundLayerMask = default(LayerMask);

    public override void Update()
    {
        GroundCheck();
    }

    private void OnJump()
    {

    }

    private void GroundCheck()
    {
        Debug.DrawRay(Character.transform.position, Vector3.down, Color.red);
        _isGround = Physics.Raycast(Character.transform.position, Vector3.down, -0.1f, LayerMask.GetMask("Ground"));
    }
}