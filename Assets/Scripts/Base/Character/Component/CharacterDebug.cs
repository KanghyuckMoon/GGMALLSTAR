using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDebug : CharacterComponent
{
    // CharacterDebugData ĳ��
    private CharacterDebugData characterDebugData = new CharacterDebugData();
    // CharacterDebugData Getter
    public CharacterDebugData CharacterDebugData => characterDebugData;

    /// <summary>
    /// CharacterDebug ������
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterDebug(Character character) : base(character)
    {
    }

    /// <summary>
    /// ���� ������ �߰�
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        CharacterDebugData.damage += damage;
    }

    /// <summary>
    /// ���� ������ �߰�
    /// </summary>
    /// <param name="damaged"></param>
    public void AddDamaged(float damaged)
    {
        CharacterDebugData.damaged += damaged;
    }

    /// <summary>
    /// �¸��� ���� �� ����
    /// </summary>
    /// <param name="winCount"></param>
    public void SetWinRoundCount(int winCount)
    {
        CharacterDebugData.winRoundCount = winCount;
    }

    /// <summary>
    /// �й��� ���� �� ����
    /// </summary>
    /// <param name="loseCount"></param>
    public void SetLoseRoundCount(int loseCount)
    {
        CharacterDebugData.loseRoundCount += loseCount;
    }

    /// <summary>
    /// ���� �ִ� �ð� �߰�
    /// </summary>
    /// <param name="groundTime"></param>
    public void AddGroundTime(float groundTime)
    {
        CharacterDebugData.groundTime += groundTime;
    }

    /// <summary>
    /// ���߿� �ִ� �ð� �߰�
    /// </summary>
    /// <param name="airTime"></param>
    public void AddAirTime(float airTime)
    {
        CharacterDebugData.airTime += airTime;
    }

    /// <summary>
    /// ���� Ƚ�� �߰�
    /// </summary>
    /// <param name="jumpCount"></param>
    public void AddJumpCount(int jumpCount)
    {
        CharacterDebugData.jumpCount += jumpCount;
    }

    /// <summary>
    /// ������ ������ Ƚ�� �߰�
    /// </summary>
    /// <param name="attackCount"></param>
    public void AddGroundAttackCount(int attackCount)
    {
        CharacterDebugData.groundAttackCount += attackCount;
    }

    /// <summary>
    /// ���߿��� ������ Ƚ�� �߰�
    /// </summary>
    /// <param name="attackCount"></param>
    public void AddAirAttackCount(int attackCount)
    {
        CharacterDebugData.airAttackCount += attackCount;
    }

    /// <summary>
    /// ��ų1 ��� Ƚ�� �߰�
    /// </summary>
    /// <param name="skill1Count"></param>
    public void AddSkill1Count(int skill1Count)
    {
        CharacterDebugData.skill1Count += skill1Count;
    }

    /// <summary>
    /// ��ų2 ��� Ƚ�� �߰�
    /// </summary>
    /// <param name="skill2Count"></param>
    public void AddSkill2Count(int skill2Count)
    {
        CharacterDebugData.skill2Count += skill2Count;
    }

    /// <summary>
    /// AllStarSkill ��� ���� ����
    /// </summary>
    /// <param name="allStarSkillRound"></param>
    public void SetAllStarSkillRound(int allStarSkillRound)
    {
        CharacterDebugData.allStarSkill = allStarSkillRound;
    }

    /// <summary>
    /// ȹ���� �� ���� �߰�
    /// </summary>
    /// <param name="star"></param>
    public void AddStar(int star)
    {
        CharacterDebugData.starAmount += star;
    }

    /// <summary>
    /// ���� �ð� �߰�
    /// </summary>
    /// <param name="time"></param>
    public void AddGameTime(float time)
    {
        CharacterDebugData.gameTime += time;
    }

    /// <summary>
    /// ���� �÷��� �ð�, ���߿� �ִ� �ð�, ���� �ִ� �ð� ó��
    /// </summary>
    public override void Update()
    {
        base.Update();
        // ���� �÷��� �ð� �߰�
        AddGameTime(Time.deltaTime);
        // ĳ���Ͱ� ���� �ִٸ�?
        if (Character.GetCharacterComponent<CharacterMove>(ComponentType.Move).IsGround)
        {
            // ���� �ִ� �ð� �߰�
            AddGroundTime(Time.deltaTime);
        }
        else
        {
            // �ƴϸ� ���߿� �ִ� �ð� �߰�
            AddAirTime(Time.deltaTime);
        }
    }

}

/// <summary>
/// Character�� Ȱ���̷��� �����ϴ� Ŭ����
/// </summary>
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
