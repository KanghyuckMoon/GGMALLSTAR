using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : CharacterComponent
{
    public CharacterStat(Character character) : base(character)
    {
    }

    // HP�� ����� �� ȣ��Ǵ� �̺�Ʈ
    private System.Action hpChangeEvent;

    // ĳ������ ���ݵ�
    protected float _hp = 0;
    protected float _maxHP = 0;
    protected bool _isPlayerP1 = false;
    public float MaxHP => _maxHP;
    public float HP => _hp;

    /// <summary>
    /// ����ִ��� Ȯ��
    /// </summary>
    /// <value></value>
    public bool IsAlive
    {
        get
        {
            return _hp > 0f;
        }
    }

    /// <summary>
    /// 1P ĳ������ Ȯ��
    /// </summary>
    /// <value></value>
    public bool IsPlayerP1
    {
        get
        {
            return _isPlayerP1;
        }
        set
        {
            _isPlayerP1 = value;
        }
    }

    protected override void Awake()
    {
        // ���� ����
        CharacterSO characterSO = Character.CharacterSO;

        _maxHP = characterSO.MaxHP;
        SetHP(characterSO.MaxHP);
    }

    /// <summary>
    /// ü�� ���� �� ����Ǵ� �̺�Ʈ �߰�
    /// </summary>
    /// <param name="action"></param>
    public void AddHPEvent(System.Action action)
    {
        hpChangeEvent += action;
    }

    /// <summary>
    /// ü�� ����
    /// </summary>
    /// <param name="hp"></param>
    public void SetHP(float hp)
    {
        _hp = hp;

        hpChangeEvent?.Invoke();
    }

    /// <summary>
    /// ü�� �߰�
    /// </summary>
    /// <param name="hp"></param>
    public void AddHP(float hp)
    {
        _hp += hp;

        hpChangeEvent?.Invoke();
    }
}
