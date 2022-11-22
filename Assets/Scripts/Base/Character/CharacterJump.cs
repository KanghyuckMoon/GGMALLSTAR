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
            if (_rigidbody&&_jumpCount == 0)
            {
                _rigidbody.AddForce(Vector3.up * _firstJumpPower, ForceMode.Impulse);
                _jumpCount++;
            }else if (_rigidbody&&_jumpCount == 1)
            {
                _rigidbody.AddForce(Vector3.up * _secondJumpPower, ForceMode.Impulse);
                _jumpCount++;
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            if (Character.transform.position.y < 2f&&_jumpCount<=2)
            {
                Debug.Log("Jumping");
                // TODO : 좌표 말고 시간으로 하는게 좋을 듯 체공 시간 구하기 필요 
                _rigidbody.AddForce(Vector3.up * _jumpingPower, ForceMode.Impulse);
            }
            
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            
        }, EventType.KEY_UP);
    }

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
    }

    public override void Update()
    {
        if(Physics.Raycast(Character.transform.position, Vector3.down, 0.1f, LayerMask.GetMask("Ground")))
        {
            _jumpCount = 0;
        }
    }

    private Rigidbody _rigidbody = null;

    private float _firstJumpPower = 50f;
    private float _secondJumpPower = 25f;
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