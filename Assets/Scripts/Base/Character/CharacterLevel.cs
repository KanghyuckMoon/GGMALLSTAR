using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterLevel : CharacterComponent
{
	public int Level
	{
		get
		{
			return _level;
		}
	}
	public int Exp
	{
		get
		{
			return _exp;
		}
	}

	private System.Action changeExpEvent;
	private System.Action changeLevelEvent;

	private int _previousLevel = 1;
	private int _level = 1;
	private int _exp = 0;

	public CharacterLevel(Character character) : base(character)
	{

	}

	public void AddChangeExpEvent(System.Action action)
	{
		changeExpEvent += action;
	}

	public void AddChangeLevelEvent(System.Action action)
	{
		changeLevelEvent += action;
	}

	public void AddExp(int addExp)
	{
		_exp += addExp;
		if (_exp >= Character.CharacterLevelSO.NeedExpLevelMax)
		{
			_exp = Character.CharacterLevelSO.NeedExpLevelMax;
		}
		CheckLevel();
	}

	public void ReturnPreviousLevel()
	{
		int removeExp = 0;
		switch(_level)
		{
			default:
			case 1:
				_exp = 0;
				CheckLevel();
				return;
			case 2:
				removeExp = Character.CharacterLevelSO.NeedExpLevel2;
				break;
			case 3:
				removeExp = Character.CharacterLevelSO.NeedExpLevel3 - Character.CharacterLevelSO.NeedExpLevel2;
				break;
			case 4:
				removeExp = Character.CharacterLevelSO.NeedExpLevelMax - Character.CharacterLevelSO.NeedExpLevel3;
				break;
		}
		_exp -= removeExp;

		CheckLevel();
	}

	/// <summary>
	/// 현재 exp량에 따라 레벨 수치를 변경함
	/// </summary>
	/// <returns></returns>
	public void CheckLevel()
	{
		if(_exp >= Character.CharacterLevelSO.NeedExpLevelMax)
		{
			_level = 4;
		}
		else if (_exp >= Character.CharacterLevelSO.NeedExpLevel3)
		{
			_level = 3;
		}
		else if (_exp >= Character.CharacterLevelSO.NeedExpLevel2)
		{
			_level = 2;
		}
		else
		{
			_level = 1;
		}
		changeExpEvent?.Invoke();

		if(_level != _previousLevel)
		{
			changeLevelEvent?.Invoke();
			_previousLevel = _level;
		}
	}


}
