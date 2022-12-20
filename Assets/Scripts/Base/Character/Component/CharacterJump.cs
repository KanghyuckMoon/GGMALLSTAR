using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterJump : CharacterComponent
{
    // Rigidbody 캐싱
    private Rigidbody _rigidbody = null;

    // 점프 횟수
    private uint _jumpCount = 0;

    // 키 입력 체크 변수
    private bool isTap;

    // Animation
    private CharacterAnimation characterAnimation = null;


    public CharacterJump(Character character) : base(character)
    {
        // 키 입력 처리
        CharacterEvent.AddEvent(EventKeyWord.UP, () =>
        {
            isTap = true;
        }, EventType.KEY_DOWN);
    }

    protected override void Awake()
    {
        // Rigidbody 캐싱
        _rigidbody = Character.Rigidbody;
        // Animation 캐싱
        characterAnimation = Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
    }

    public override void FixedUpdate()
    {
        // 점프 입력 받았을때
        if (isTap)
        {
            // 키 입력 체크 변수 초기화
            isTap = false;
            // 생존 체크
            if (!Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
            {
                _rigidbody.velocity = Vector3.zero;
            }
            else if (_rigidbody && _jumpCount == 0)
            {
                // 생존중이며 _rigidbody가 있고 점프 횟수가 0일때 점프 처리
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
