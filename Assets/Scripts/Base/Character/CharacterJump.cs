using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterJump : CharacterComponent
{
    public CharacterJump(Character character) : base(character)
    {
        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            isTap = true;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            //if (Character.transform.position.y < 2f&&_jumpCount<=2)
            //{
            //    Debug.Log("Jumping");
            //    // TODO : 좌표 말고 시간으로 하는게 좋을 듯 체공 시간 구하기 필요 
            //    _rigidbody.AddForce(Vector3.up * _jumpingPower, ForceMode.Impulse);
            //}
            
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            isTap = false;
            isHold = false;
        }, EventType.KEY_UP);
    }

    private bool isTap;
    private bool isHold;
    private CharacterAnimation characterAnimation = null;

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
        characterAnimation = Character.GetCharacterComponent<CharacterAnimation>();
    }

    public override void FixedUpdate()
    {
        Vector3 pos = Character.transform.position + Character.Collider.center;
        pos.y += (-Character.Collider.size.y * 0.5f);

        if (Physics.Raycast(pos, Vector3.down, 0.1f, LayerMask.GetMask("Ground")) && _rigidbody.velocity.y <= 0)
        {
            _jumpCount = 0;
        }

        if (isTap && !isHold)
        {
            isHold = true;
            if (_rigidbody && _jumpCount == 0)
            {
                var vel = _rigidbody.velocity;
                vel.y = 0;
                _rigidbody.velocity = vel;
                _rigidbody.AddForce(Vector3.up * Character.CharacterSO.FirstJumpPower, ForceMode.Impulse);
                _jumpCount++;
                characterAnimation.SetAnimationTrigger(AnimationType.Jump);
            }
        }
    }

    private Rigidbody _rigidbody = null;

    private float _jumpingPower = 10f;
    
    private float _maxFirstJumpPower = 35f;
    private float _maxSecondJumpPower = 20f;
    
    private uint _jumpCount = 0;
}

/*

땅에 쿹어있다.
공중에 떠있다
    점프하여 공중에 뜨

isJump(bool)
isJumping(bool)
isGround(bool)
isFall(bool)

jumpCurrent(int)
*/