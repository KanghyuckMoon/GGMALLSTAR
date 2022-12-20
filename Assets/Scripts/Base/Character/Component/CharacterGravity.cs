using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : CharacterComponent
{
    public CharacterGravity(Character character) : base(character)
    {

    }

    // Character의 Rigidbody 캐싱
    private Rigidbody _rigidbody = null;

    // hitTime 기억하는 변수
    private float _hitTime = 0f;

    protected override void Awake()
    {
        // Character의 Rigidbody 캐싱
        _rigidbody = Character.Rigidbody;
        // Character의 Rigidbody의 Gravity 사용 여부를 false로 설정
        _rigidbody.useGravity = false;
    }

    /// <summary>
    /// hitTime을 설정하는 함수
    /// </summary>
    /// <param name="time"></param>
    public void SetHitTime(float time)
    {
        this._hitTime = time;
    }

    /// <summary>
    /// 중력 처리
    /// </summary>
    public override void FixedUpdate()
    {
        if (_hitTime > 0f)
        {
            // 맞는 중이라면 중력 이외의 힘이 적용하기 때문에 적용 안함
            _hitTime -= Time.fixedDeltaTime;
            return;
        }

        base.FixedUpdate();
        // Character의 GravityScale에 따라 중력을 적용
        Vector3 gravity = Character.CharacterSO.GravityScale * Physics.gravity;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
