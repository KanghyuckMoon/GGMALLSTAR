
public abstract class CharacterSkill : CharacterComponent
{
    // ��ų ��� ���� ����
    protected bool isCanUseSkill1;
    protected bool isCanUseSkill2;
    protected bool isCanUseSkill3;
    public bool IsCanUseSkill1 => isCanUseSkill1;
    public bool IsCanUseSkill2 => isCanUseSkill2;
    public bool IsCanUseSkill3 => isCanUseSkill3;

    // ĳ���� ���� ĳ��
    protected CharacterLevel characterLevel;
    // ��ų �� Ÿ�� ĳ��
    protected float skillCoolTime1 = 0f;
    protected float skillCoolTime2 = 0f;

    // ��ų ������ �����ð� Getter
    public float Skill1RemainCoolTime
    {
        get
        {
            return Character.CharacterSO.skill1Delay - skillCoolTime1;
        }
    }
    public float Skill2RemainCoolTime
    {
        get
        {
            return Character.CharacterSO.skill2Delay - skillCoolTime2;
        }
    }

    // ��ų �� ���� Getter
    public float Skill1CoolTimeRatio
    {
        get
        {
            return skillCoolTime1 / Character.CharacterSO.skill1Delay;
        }
    }
    public float Skill2CoolTimeRatio
    {
        get
        {
            return skillCoolTime2 / Character.CharacterSO.skill2Delay;
        }
    }

    // ��ų �� Ÿ�� ���� �̺�Ʈ
    protected System.Action skill1CoolTimeChange;
    protected System.Action skill2CoolTimeChange;

    // AllStar ��ų ��� �̺�Ʈ
    protected System.Action allStarSkillUse;

    public System.Action Skill1CoolTimeChange => skill1CoolTimeChange;
    public System.Action Skill2CoolTimeChange => skill2CoolTimeChange;
    protected System.Action AllStarSkillUse => allStarSkillUse;

    public CharacterSkill(Character character) : base(character)
    {

    }

    /// <summary>
    /// ��ų1 �� Ÿ�� ���� �� ����Ǵ� �̺�Ʈ �߰�
    /// </summary>
    /// <param name="action"></param>
    public void AddSkill1CoolTimeChange(System.Action action)
    {
        skill1CoolTimeChange += action;
    }

    /// <summary>
    /// ��ų2 �� Ÿ�� ���� �� ����Ǵ� �̺�Ʈ �߰�
    /// </summary>
    /// <param name="action"></param>
    public void AddSkill2CoolTimeChange(System.Action action)
    {
        skill2CoolTimeChange += action;
    }

    /// <summary>
    /// �ý�Ÿ ��ų ���� ����Ǵ� �̺�Ʈ �߰�
    /// </summary>
    /// <param name="action"></param>
    public void AddAllStarSkillUse(System.Action action)
    {
        allStarSkillUse += action;
    }

    /// <summary>
    /// ��ų1 ���� ����Ǵ� �Լ�
    /// </summary>
    public virtual void Skill1Action()
    {
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddSkill1Count(1);
    }

    /// <summary>
    /// ��ų2 ���� ����Ǵ� �Լ�
    /// </summary>
    public virtual void Skill2Action()
    {
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddSkill2Count(1);
    }

    /// <summary>
    /// ��ų3 ���� ����Ǵ� �Լ�
    /// </summary>
    public virtual void AllStarSkillAction()
    {
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).SetAllStarSkillRound(RoundManager.GetRoundNumber());
    }

}
