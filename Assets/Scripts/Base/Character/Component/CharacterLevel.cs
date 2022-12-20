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

    // AllStarSkill ��� ���� getter
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

    // ����ġ ������ ȣ���� �̺�Ʈ
    private System.Action changeExpEvent;
    // �������� ȣ���� �̺�Ʈ
    private System.Action changeLevelEvent;

    // ���� ����
    private int _previousLevel = 1;
    // �� ����
    private int _level = 1;
    // �� ����ġ
    private int _exp = 0;
    // AllStarSkill ��� ����
    private bool isAllStarSkillUse = false;

    // ���� ������ ���� �ʿ��� ����ġ
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

    // �� ������ �Ǳ���� �ʿ��ߴ� ����ġ
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

    // ����ġ ������ ȣ���� �̺�Ʈ �߰�
    public void AddChangeExpEvent(System.Action action)
    {
        changeExpEvent += action;
    }

    // �������� ȣ���� �̺�Ʈ �߰�
    public void AddChangeLevelEvent(System.Action action)
    {
        changeLevelEvent += action;
    }

    // ����ġ ȹ�� �Լ�
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

    // ����ġ ���� �Լ�
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
    /// ���� exp���� ���� ���� ��ġ�� ������
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
