using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterLevel : CharacterComponent
{
    // Level getter
    public int Level
    {
        get
        {
            return _level;
        }
    }

    // Exp getter
    public int Exp
    {
        get
        {
            return _exp;
        }
    }

    // AllStarSkill 사용 여부 getter
    public bool IsAllStarSkillUse
    {
        get
        {
            return isAllStarSkillUse;
        }
        set
        {
            isAllStarSkillUse = value;
        }
    }

    // 경험치 증가시 호출할 이벤트
    private System.Action changeExpEvent;
    // 레벨업시 호출할 이벤트
    private System.Action changeLevelEvent;

    // 이전 레벨
    private int _previousLevel = 1;
    // 현 레벨
    private int _level = 1;
    // 현 경험치
    private int _exp = 0;
    // AllStarSkill 사용 여부
    private bool isAllStarSkillUse = false;

    // 다음 레벨업 까지 필요한 경험치
    public int NeedExp
    {
        get
        {
            switch (_level)
            {
                default:
                case 1:
                    return Character.CharacterLevelSO.NeedExpLevel2;
                case 2:
                    return Character.CharacterLevelSO.NeedExpLevel3 - Character.CharacterLevelSO.NeedExpLevel2;
                case 3:
                    return Character.CharacterLevelSO.NeedExpLevelMax - Character.CharacterLevelSO.NeedExpLevel3;
                case 4:
                    return 0;
            }
        }
    }

    // 현 레벨이 되기까지 필요했던 경험치
    public int PreviouseExp
    {
        get
        {
            switch (_level)
            {
                default:
                case 1:
                    return 0;
                case 2:
                    return Character.CharacterLevelSO.NeedExpLevel2;
                case 3:
                    return Character.CharacterLevelSO.NeedExpLevel3;
                case 4:
                    return Character.CharacterLevelSO.NeedExpLevelMax;
            }
        }
    }


    public CharacterLevel(Character character) : base(character)
    {

    }

    // 경험치 증가시 호출할 이벤트 추가
    public void AddChangeExpEvent(System.Action action)
    {
        changeExpEvent += action;
    }

    // 레벨업시 호출할 이벤트 추가
    public void AddChangeLevelEvent(System.Action action)
    {
        changeLevelEvent += action;
    }

    // 경험치 획득 함수
    public void AddExp(int addExp)
    {
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddStar(addExp);
        _exp += addExp;
        if (_exp >= Character.CharacterLevelSO.NeedExpLevelMax)
        {
            _exp = Character.CharacterLevelSO.NeedExpLevelMax;
        }
        CheckLevel();
    }

    // 경험치 감소 함수
    public void ReturnPreviousLevel()
    {
        int removeExp = 0;
        switch (_level)
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
        if (_exp >= Character.CharacterLevelSO.NeedExpLevelMax)
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

        if (_level != _previousLevel)
        {
            if (_level > _previousLevel)
            {
                Sound.SoundManager.Instance.PlayEFF("se_common_final_cutin");
            }

            changeLevelEvent?.Invoke();
            _previousLevel = _level;
        }
    }
}
