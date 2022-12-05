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
	public float damage; //입힌 데미지
	public float damaged; //입은 데미지
	public int winRoundCount; //이긴 라운드
	public int loseRoundCount; //패배한 라운드
	public float groundTime; //지상에 있었던 시간
	public float airTime; //공중에 있었던 시간
	public int jumpCount; //점프 횟수
	public int groundAttackCount; //지상 공격 횟수
	public int airAttackCount; //공중 공격 횟수
	public int skill1Count; //스킬1 사용 횟수
	public int skill2Count; //스킬2 사용 횟수
	public int allStarSkill; //올스타 스킬 사용한 라운드
	public int starAmount; //모은 스타 갯수 
	public float gameTime; //총 게임 시간
}
