using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character) : base(character)
    {
        CharacterEvent characterEvent = character.GetCharacterComponent<CharacterEvent>();

        characterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            _wasJump = true;
            Jump(1f + _acceleration);
            JumpRenewal();
        }, EventType.KEY_DOWN);

        characterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            _curTime += Time.deltaTime;
            if (_curTime < _timer)
            {
                return;
            }
            _acceleration += Time.deltaTime;
            _acceleration = Mathf.Clamp(_acceleration, 0f, _maxAcceleration);

            if (_acceleration < _maxAcceleration)
            {
                JumpProportion(_accelerationPower);
            }
        }, EventType.KEY_HOLD);

        characterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            _wasJump = false;
            _acceleration = 0f;
            _curTime = 0f;
        }, EventType.KEY_UP);
    }

    private bool _isJump = false;
    private bool _wasJump = false;

    protected override void Awake()
    {
        _isJump = false;
        _wasJump = false;
        _rigidbody = Character.GetComponent<Rigidbody>();
        _transform = Character.GetComponent<Transform>();
    }

    #region Variables
    private Rigidbody _rigidbody = null;
    private Transform _transform = null;

    private float _jumpPower = 100f;

    private float _secondJumpPower = 0.8f;

    private float _rayLength = 0.5f;

    private int _jumpCount = 2;
    private int _currentJumpCnt = 0;

    private float _accelerationJumpPower = 1f;

    private bool _isJumpable = true;
    private bool _isDoubleJump = false;
    private bool _isFirstJump = true;

    private bool _isGround = false;
    #endregion

    #region PlayerNormalJump

    private float _maxAcceleration = 1f;
    private float _accelerationPower = 0.06f;

    private float _timer = 0.1f;

    private float _curTime = 0f;

    private float _acceleration = 0f;

    #endregion

    public override void Update()
    {
        GroundCheck();
    }

    private void Jump(float accelerationJumpPower = 1f)
    {
        if (_isJumpable == false)
            return;
        if (_isGround == false && _currentJumpCnt >= _jumpCount)
            return;

        _isFirstJump = false;
        float jumpPow = _currentJumpCnt > 0 ? _jumpPower * _secondJumpPower * accelerationJumpPower : _jumpPower * accelerationJumpPower;
        if (_isDoubleJump)
        {
            jumpPow = _jumpPower * _secondJumpPower;
        }

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        _rigidbody.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);

        _isDoubleJump = false;
    }

    private void GroundCheck()
    {
        _isGround = Physics.Raycast(Character.transform.position, Vector3.down, _rayLength, LayerMask.GetMask("Ground"));
        _isFirstJump = _isGround;
        if (_isGround)
        {
            _currentJumpCnt = 0;
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
        if (_isFirstJump)
        {
            _currentJumpCnt = 1;
        }
    }

    public void JumpProportion(float power)
    {
        if (_isJumpable == false)
            return;
        if (_isGround == false && _currentJumpCnt > _jumpCount)
            return;

        if (_currentJumpCnt > 1)
            power *= _secondJumpPower;

        _rigidbody.AddForce(Vector3.up * (_jumpPower * power) * Time.deltaTime, ForceMode.Force);
    }

    public void JumpRenewal()
    {
        if (_isJumpable == false)
            return;
        if (_isGround == false && _currentJumpCnt >= _jumpCount)
            return;

        _currentJumpCnt++;
    }
}