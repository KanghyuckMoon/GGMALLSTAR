using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDebug : CharacterComponent
{
    // CharacterDebugData 캐싱
    private CharacterDebugData characterDebugData = new CharacterDebugData();
    // CharacterDebugData Getter
    public CharacterDebugData CharacterDebugData => characterDebugData;

    /// <summary>
    /// CharacterDebug 생성자
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterDebug(Character character) : base(character)
    {
    }

    /// <summary>
    /// 입힌 데미지 추가
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        CharacterDebugData.damage += damage;
    }

    /// <summary>
    /// 입은 데미지 추가
    /// </summary>
    /// <param name="damaged"></param>
    public void AddDamaged(float damaged)
    {
        CharacterDebugData.damaged += damaged;
    }

    /// <summary>
    /// 승리한 라운드 수 설정
    /// </summary>
    /// <param name="winCount"></param>
    public void SetWinRoundCount(int winCount)
    {
        CharacterDebugData.winRoundCount = winCount;
    }

    /// <summary>
    /// 패배한 라운드 수 설정
    /// </summary>
    /// <param name="loseCount"></param>
    public void SetLoseRoundCount(int loseCount)
    {
        CharacterDebugData.loseRoundCount += loseCount;
    }

    /// <summary>
    /// 땅에 있는 시간 추가
    /// </summary>
    /// <param name="groundTime"></param>
    public void AddGroundTime(float groundTime)
    {
        CharacterDebugData.groundTime += groundTime;
    }

    /// <summary>
    /// 공중에 있는 시간 추가
    /// </summary>
    /// <param name="airTime"></param>
    public void AddAirTime(float airTime)
    {
        CharacterDebugData.airTime += airTime;
    }

    /// <summary>
    /// 점프 횟수 추가
    /// </summary>
    /// <param name="jumpCount"></param>
    public void AddJumpCount(int jumpCount)
    {
        CharacterDebugData.jumpCount += jumpCount;
    }

    /// <summary>
    /// 땅에서 공격한 횟수 추가
    /// </summary>
    /// <param name="attackCount"></param>
    public void AddGroundAttackCount(int attackCount)
    {
        CharacterDebugData.groundAttackCount += attackCount;
    }

    /// <summary>
    /// 공중에서 공격한 횟수 추가
    /// </summary>
    /// <param name="attackCount"></param>
    public void AddAirAttackCount(int attackCount)
    {
        CharacterDebugData.airAttackCount += attackCount;
    }

    /// <summary>
    /// 스킬1 사용 횟수 추가
    /// </summary>
    /// <param name="skill1Count"></param>
    public void AddSkill1Count(int skill1Count)
    {
        CharacterDebugData.skill1Count += skill1Count;
    }

    /// <summary>
    /// 스킬2 사용 횟수 추가
    /// </summary>
    /// <param name="skill2Count"></param>
    public void AddSkill2Count(int skill2Count)
    {
        CharacterDebugData.skill2Count += skill2Count;
    }

    /// <summary>
    /// AllStarSkill 사용 라운드 설정
    /// </summary>
    /// <param name="allStarSkillRound"></param>
    public void SetAllStarSkillRound(int allStarSkillRound)
    {
        CharacterDebugData.allStarSkill = allStarSkillRound;
    }

    /// <summary>
    /// 획득한 별 개수 추가
    /// </summary>
    /// <param name="star"></param>
    public void AddStar(int star)
    {
        CharacterDebugData.starAmount += star;
    }

    /// <summary>
    /// 게임 시간 추가
    /// </summary>
    /// <param name="time"></param>
    public void AddGameTime(float time)
    {
        CharacterDebugData.gameTime += time;
    }

    /// <summary>
    /// 게임 플레이 시간, 공중에 있는 시간, 땅에 있는 시간 처리
    /// </summary>
    public override void Update()
    {
        base.Update();
        // 게임 플레이 시간 추가
        AddGameTime(Time.deltaTime);
        // 캐릭터가 땅에 있다면?
        if (Character.GetCharacterComponent<CharacterMove>(ComponentType.Move).IsGround)
        {
            // 땅에 있는 시간 추가
            AddGroundTime(Time.deltaTime);
        }
        else
        {
            // 아니면 공중에 있는 시간 추가
            AddAirTime(Time.deltaTime);
        }
    }

}

/// <summary>
/// Character의 활동이력을 저장하는 클래스
/// </summary>
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
