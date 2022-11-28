using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : CharacterComponent
{
    public CharacterStat(Character character) : base(character)
    {
    }

    protected override void SetEvent()
    {

    }

    private System.Action hpChangeEvent;

    protected float _hp = 0;
    protected float _maxHP = 0;
    public float MaxHP => _maxHP;
    public float HP => _hp;
    public bool IsAlive
	{
        get
		{
            return _hp > 0f;
		}
	}
	protected override void Awake()
    {
        CharacterSO characterSO = Character.CharacterSO;

        _maxHP = characterSO.MaxHP;
        SetHP(characterSO.MaxHP);
    }

    public void AddHPEvent(System.Action action)
    {
        hpChangeEvent += action;
    }

    public void SetHP(float hp)
	{
        _hp = hp;

        hpChangeEvent?.Invoke();
    }
    public void AddHP(float hp)
    {
        _hp += hp;

        hpChangeEvent?.Invoke();
    }
}
