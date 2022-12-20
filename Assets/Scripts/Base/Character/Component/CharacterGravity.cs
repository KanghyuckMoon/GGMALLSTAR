using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : CharacterComponent
{
    public CharacterGravity(Character character) : base(character)
    {

    }

    // Character�� Rigidbody ĳ��
    private Rigidbody _rigidbody = null;

    // hitTime ����ϴ� ����
    private float _hitTime = 0f;

    protected override void Awake()
    {
        // Character�� Rigidbody ĳ��
        _rigidbody = Character.Rigidbody;
        // Character�� Rigidbody�� Gravity ��� ���θ� false�� ����
        _rigidbody.useGravity = false;
    }

    /// <summary>
    /// hitTime�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="time"></param>
    public void SetHitTime(float time)
    {
        this._hitTime = time;
    }

    /// <summary>
    /// �߷� ó��
    /// </summary>
    public override void FixedUpdate()
    {
        if (_hitTime > 0f)
        {
            // �´� ���̶�� �߷� �̿��� ���� �����ϱ� ������ ���� ����
            _hitTime -= Time.fixedDeltaTime;
            return;
        }

        base.FixedUpdate();
        // Character�� GravityScale�� ���� �߷��� ����
        Vector3 gravity = Character.CharacterSO.GravityScale * Physics.gravity;
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
