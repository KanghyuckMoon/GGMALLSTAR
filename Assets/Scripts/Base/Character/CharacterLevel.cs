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
	public UnityEvent checkLevelEvent;

	private int _level = 1;
	private int _exp = 0;

	public CharacterLevel(CharacterLevelSO levelSO, UnityEvent checkLevelEvent)
	{
		this.characterLevelSO = levelSO;
		this.checkLevelEvent = checkLevelEvent;
	}

	public void AddExp(int addExp)
	{
		_exp += addExp;
		CheckLevel();
	}

	/// <summary>
	/// 현재 exp량에 따라 레벨 수치를 변경함
	/// </summary>
	/// <returns></returns>
	public void CheckLevel()
	{
		if(_exp > characterLevelSO.NeedExpLevelMax)
		{
			_level = 4;
		}
		else if (_exp < characterLevelSO.NeedExpLevel3)
		{
			_level = 3;
		}
		else if (_exp < characterLevelSO.NeedExpLevel2)
		{
			_level = 2;
		}
		else
		{
			_level = 1;
		}
		checkLevelEvent?.Invoke();
	}


}
