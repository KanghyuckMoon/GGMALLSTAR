using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterJump : CharacterComponent
{

    private Rigidbody _rigidbody = null;

    private float _jumpingPower = 10f;

    private float _maxFirstJumpPower = 35f;
    private float _maxSecondJumpPower = 20f;

    private uint _jumpCount = 0;

    private bool isTap;
    private bool isGround;
    private bool isHold;
    private CharacterAnimation characterAnimation = null;


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
            //isTap = false;
            //isHold = false;
        }, EventType.KEY_UP);
    }

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
        characterAnimation = Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
    }

    public override void FixedUpdate()
    {
        Vector3 pos = Character.transform.position + Character.Collider.center;
        pos.y += (-Character.Collider.size.y * 0.5f);

        //if (Physics.Raycast(pos, Vector3.down, 0.1f, LayerMask.GetMask("Ground")) && _rigidbody.velocity.y <= 0)
        //{
        //    _jumpCount = 0;
        //}

        if (isTap)
        {
            isTap = false;
            if(!Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
            {
                _rigidbody.velocity = Vector3.zero;
            }
            else if (_rigidbody && _jumpCount == 0)
            {
                Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddJumpCount(1);
                var vel = _rigidbody.velocity;
                vel.y = 0;
                _rigidbody.velocity = vel;
                _rigidbody.AddForce(Vector3.up * Character.CharacterSO.FirstJumpPower, ForceMode.Impulse);
                _jumpCount++;
                characterAnimation.SetAnimationTrigger(AnimationType.Jump);
            }
        }
    }
    public override void OnCollisionStay(Collision other)
    {
        //Ground Check
        if (other.gameObject.layer == 6 && _jumpCount > 0 && _rigidbody.velocity.y <= 0)
        {
            _jumpCount = 0;
        }
    }
}
