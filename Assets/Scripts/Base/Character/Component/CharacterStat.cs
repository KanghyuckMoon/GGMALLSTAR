using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : CharacterComponent
{
    public CharacterStat(Character character) : base(character)
    {
    }

    // HP가 변경될 때 호출되는 이벤트
    private System.Action hpChangeEvent;

    // 캐릭터의 스텟들
    protected float _hp = 0;
    protected float _maxHP = 0;
    protected bool _isPlayerP1 = false;
    public float MaxHP => _maxHP;
    public float HP => _hp;

    /// <summary>
    /// 살아있는지 확인
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
    /// 1P 캐릭인지 확인
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
        // 스텟 설정
        CharacterSO characterSO = Character.CharacterSO;

        _maxHP = characterSO.MaxHP;
        SetHP(characterSO.MaxHP);
    }

    /// <summary>
    /// 체력 변경 시 실행되는 이벤트 추가
    /// </summary>
    /// <param name="action"></param>
    public void AddHPEvent(System.Action action)
    {
        hpChangeEvent += action;
    }

    /// <summary>
    /// 체력 설정
    /// </summary>
    /// <param name="hp"></param>
    public void SetHP(float hp)
    {
        _hp = hp;

        hpChangeEvent?.Invoke();
    }

    /// <summary>
    /// 체력 추가
    /// </summary>
    /// <param name="hp"></param>
    public void AddHP(float hp)
    {
        _hp += hp;

        hpChangeEvent?.Invoke();
    }
}
