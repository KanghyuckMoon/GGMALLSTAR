using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDebug : CharacterComponent
{
	private CharacterDebugData characterDebugData;
	public CharacterDebugData CharacterDebugData => characterDebugData;

	public CharacterDebug(Character character) : base(character)
	{
	}

	public void AddDamage(float damage)
	{
		CharacterDebugData.damage += damage;
	}

	public void AddDamaged(float damaged)
	{
		CharacterDebugData.damaged += damaged;
	}

	public void SetWinRoundCount(int winCount)
	{
		CharacterDebugData.winRoundCount = winCount;
	}
	public void SetLoseRoundCount(int loseCount)
	{
		CharacterDebugData.loseRoundCount += loseCount;
	}
	public void AddGroundTime(float groundTime)
	{
		CharacterDebugData.groundTime += groundTime;
	}
	public void AddAirTime(float airTime)
	{
		CharacterDebugData.airTime += airTime;
	}
	public void AddJumpCount(int jumpCount)
	{
		CharacterDebugData.jumpCount += jumpCount;
	}
	public void AddGroundAttackCount(int attackCount)
	{
		CharacterDebugData.groundAttackCount += attackCount;
	}
	public void AddAirAttackCount(int attackCount)
	{
		CharacterDebugData.airAttackCount += attackCount;
	}
	public void AddSkill1Count(int skill1Count)
	{
		CharacterDebugData.skill1Count += skill1Count;
	}
	public void AddSkill2Count(int skill2Count)
	{
		CharacterDebugData.skill2Count += skill2Count;
	}
	public void SetAllStarSkillRound(int allStarSkillRound)
	{
		CharacterDebugData.allStarSkill = allStarSkillRound;
	}
	public void AddGameTime(float time)
	{
		CharacterDebugData.gameTime += time;
	}

}

public class CharacterDebugData
{
	public float damage; //���� ������
	public float damaged; //���� ������
	public int winRoundCount; //�̱� ����
	public int loseRoundCount; //�й��� ����
	public float groundTime; //���� �־��� �ð�
	public float airTime; //���߿� �־��� �ð�
	public int jumpCount; //���� Ƚ��
	public int groundAttackCount; //���� ���� Ƚ��
	public int airAttackCount; //���� ���� Ƚ��
	public int skill1Count; //��ų1 ��� Ƚ��
	public int skill2Count; //��ų2 ��� Ƚ��
	public int allStarSkill; //�ý�Ÿ ��ų ����� ����
	public int starAmount; //���� ��Ÿ ���� 
	public float gameTime; //�� ���� �ð�
}
