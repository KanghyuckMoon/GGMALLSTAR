using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character) : base(character)
    {
        character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.UP, null);
    }

    private bool _isJump = false;

    protected override void Awake()
    {
        _isJump = false;
        _rigidbody = Character.GetComponent<Rigidbody>();
    }

    #region Variables
    protected Rigidbody _rigidbody = null;

    [SerializeField]
    protected float _jumpPower = 10f;

    [SerializeField]
    protected float _secondJumpPower = 0.8f;

    [SerializeField]
    private float _rayLength = 0.5f;

    [SerializeField]
    protected int _jumpCount = 2;
    protected int _currentJumpCnt = 0;

    private bool _isJumpable = true;
    protected bool _isDoubleJump = false;
    protected bool _isFirstJump = true;

    protected bool _isGround = false;

    [SerializeField]
    private LayerMask _groundMask = 0;
    #endregion

    public override void Update()
    {
        GroundCheck();
    }

    // private void Jump()
    // {
    //     _isJump = !_isJump;
    //     if (_isJump)
    //     {
    //         return;
    //     }

    //     if (_isJumpable == false)
    //         return;
    //     if (_isGround == false && _currentJumpCnt >= _jumpCount)
    //         return;

    //     _isFirstJump = false;
    //     float jumpPow = _currentJumpCnt > 0 ? _jumpPower * _secondJumpPower * accelerationJumpPower : _jumpPower * accelerationJumpPower;
    //     if (_isDoubleJump)
    //     {
    //         jumpPow = _jumpPower * _secondJumpPower;
    //     }

    //     if (_currentJumpCnt == 0)
    //     {
    //         PaticleObj p = PoolManager.Instance.Pop("FirstJumpParticle") as PaticleObj;
    //         p.transform.position = transform.position + Vector3.down * 0.1f;
    //     }
    //     else
    //     {
    //         PaticleObj p = PoolManager.Instance.Pop("JumpParticle") as PaticleObj;
    //         p.transform.position = transform.position + Vector3.up * 0.1f;
    //     }

    //     OnJumpPress?.Invoke();

    //     _rigid.velocity = new Vector2(_rigid.velocity.x, 0f);
    //     _rigid.AddForce(Vector3.up * jumpPow, ForceMode2D.Impulse);

    //     _isDoubleJump = false;
    // }

    private void GroundCheck()
    {
        _isGround = Physics.Raycast(Character.transform.position, Vector3.down, _rayLength, _groundMask);
        _isFirstJump = _isGround;
        if (_isGround)
        {
            _currentJumpCnt = 0;
        }
    }
}