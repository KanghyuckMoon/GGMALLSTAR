
public abstract class CharacterSkill : CharacterComponent
{
    // 스킬 사용 가능 여부
    protected bool isCanUseSkill1;
    protected bool isCanUseSkill2;
    protected bool isCanUseSkill3;
    public bool IsCanUseSkill1 => isCanUseSkill1;
    public bool IsCanUseSkill2 => isCanUseSkill2;
    public bool IsCanUseSkill3 => isCanUseSkill3;

    // 캐릭터 레벨 캐싱
    protected CharacterLevel characterLevel;
    // 스킬 쿨 타임 캐싱
    protected float skillCoolTime1 = 0f;
    protected float skillCoolTime2 = 0f;

    // 스킬 사용까지 남은시간 Getter
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

    // 스킬 쿨 비율 Getter
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

    // 스킬 쿨 타임 변경 이벤트
    protected System.Action skill1CoolTimeChange;
    protected System.Action skill2CoolTimeChange;

    // AllStar 스킬 사용 이벤트
    protected System.Action allStarSkillUse;

    public System.Action Skill1CoolTimeChange => skill1CoolTimeChange;
    public System.Action Skill2CoolTimeChange => skill2CoolTimeChange;
    protected System.Action AllStarSkillUse => allStarSkillUse;

    public CharacterSkill(Character character) : base(character)
    {

    }

    /// <summary>
    /// 스킬1 쿨 타임 변경 시 실행되는 이벤트 추가
    /// </summary>
    /// <param name="action"></param>
    public void AddSkill1CoolTimeChange(System.Action action)
    {
        skill1CoolTimeChange += action;
    }

    /// <summary>
    /// 스킬2 쿨 타임 변경 시 실행되는 이벤트 추가
    /// </summary>
    /// <param name="action"></param>
    public void AddSkill2CoolTimeChange(System.Action action)
    {
        skill2CoolTimeChange += action;
    }

    /// <summary>
    /// 올스타 스킬 사용시 실행되는 이벤트 추가
    /// </summary>
    /// <param name="action"></param>
    public void AddAllStarSkillUse(System.Action action)
    {
        allStarSkillUse += action;
    }

    /// <summary>
    /// 스킬1 사용시 실행되는 함수
    /// </summary>
    public virtual void Skill1Action()
    {
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddSkill1Count(1);
    }

    /// <summary>
    /// 스킬2 사용시 실행되는 함수
    /// </summary>
    public virtual void Skill2Action()
    {
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddSkill2Count(1);
    }

    /// <summary>
    /// 스킬3 사용시 실행되는 함수
    /// </summary>
    public virtual void AllStarSkillAction()
    {
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).SetAllStarSkillRound(RoundManager.GetRoundNumber());
    }

}
