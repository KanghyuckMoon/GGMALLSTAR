using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterLevel
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

	public CharacterLevelSO characterLevelSO;
	public UnityEvent changeExpEvent;
	public UnityEvent changeLevelEvent;

	private int _previousLevel = 1;
	private int _level = 1;
	private int _exp = 0;

	public CharacterLevel(CharacterLevelSO levelSO, UnityEvent changeExpEvent, UnityEvent changeLevelEvent)
	{
		this.characterLevelSO = levelSO;
		this.changeExpEvent = changeExpEvent;
		this.changeLevelEvent = changeLevelEvent;
	}

	public void AddExp(int addExp)
	{
		_exp += addExp;
		if (_exp >= characterLevelSO.NeedExpLevelMax)
		{
			_exp = characterLevelSO.NeedExpLevelMax;
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
				removeExp = characterLevelSO.NeedExpLevel2;
				break;
			case 3:
				removeExp = characterLevelSO.NeedExpLevel3 - characterLevelSO.NeedExpLevel2;
				break;
			case 4:
				removeExp = characterLevelSO.NeedExpLevelMax - characterLevelSO.NeedExpLevel3;
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
		if(_exp >= characterLevelSO.NeedExpLevelMax)
		{
			_level = 4;
		}
		else if (_exp >= characterLevelSO.NeedExpLevel3)
		{
			_level = 3;
		}
		else if (_exp >= characterLevelSO.NeedExpLevel2)
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
