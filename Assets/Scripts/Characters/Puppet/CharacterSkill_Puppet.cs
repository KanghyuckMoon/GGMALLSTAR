using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Pool;

public class CharacterSkill_Puppet : CharacterSkill
{
    public CharacterLevel CharacterLevel
    {
        get
        {
            characterLevel ??= Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
            return characterLevel;
        }
    }

    // Elemental's Transform
    private Transform _elementalTransform = null;

    // Elemental GameObject
    private GameObject[] _elemental = null;

    // Elemental Type Enum
    public enum ElementalType : uint
    {
        Wind = 0,
        Fire,
        Earth,
        Water,
        Count
    }

    // Current Elemental Type
    private ElementalType _elementalType = ElementalType.Count;
    public ElementalType CurrentElementalType => _elementalType;

    private bool _isEarthGolemSpawn = false;
    public bool IsEarthGolemSpawn
    {
        get => _isEarthGolemSpawn;
        set => _isEarthGolemSpawn = value;
    }

    public CharacterSkill_Puppet(Character character) : base(character)
    {
    }

    protected override void SetEvent()
    {
        base.SetEvent();

        // Elemental Transform Caching
        if (Character is Character_Puppet)
        {
            _elementalTransform = (Character as Character_Puppet)?.ElementalTransform;
        }
        else if (Character is Character_Puppet_AI)
        {
            _elementalTransform = (Character as Character_Puppet_AI)?.ElementalTransform;
        }

        // Elemental variable init
        _elemental = new GameObject[(uint)ElementalType.Count];

        // Elemental Prefab Caching
        _elemental[(uint)ElementalType.Wind] = _elementalTransform.GetChild(0).gameObject;
        _elemental[(uint)ElementalType.Fire] = _elementalTransform.GetChild(1).gameObject;
        _elemental[(uint)ElementalType.Earth] = _elementalTransform.GetChild(2).gameObject;
        _elemental[(uint)ElementalType.Water] = _elementalTransform.GetChild(3).gameObject;


        // Elemental Default Setting
        for (int i = 0; i < _elemental.Length; i++)
        {
            _elemental[i].transform.SetParent(_elementalTransform);
            _elemental[i].transform.localPosition = Vector3.zero;
            _elemental[i].SetActive(false);
        }


        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            if (CharacterLevel.Level > 1 && skillCoolTime1 >= Character.CharacterSO.skill1Delay)
            {
                skillCoolTime1 = 0f;

                _elementalType = _elementalType == ElementalType.Count ? ElementalType.Wind : (ElementalType)(((uint)_elementalType + 1) % (uint)ElementalType.Count);
                _elementalType = _elementalType == ElementalType.Count ? ElementalType.Wind : _elementalType;

                for (int i = 0; i < _elemental.Length; i++)
                {
                    _elemental[i].SetActive(false);
                }

                _elemental[(uint)_elementalType].SetActive(true);

                // roll back stat


                // Elemental Type Effect Setting
                switch (_elementalType)
                {
                    case ElementalType.Wind:

                        break;
                    case ElementalType.Fire:

                        break;
                    case ElementalType.Earth:

                        break;
                    case ElementalType.Water:

                        break;
                    default:
                        Debug.LogError("ElementalType Error");
                        break;
                }
                Sound.SoundManager.Instance.PlayEFF("se_item_smartbomb_blink");
                Skill1Action();
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            if (CharacterLevel.Level > 2 && skillCoolTime2 >= Character.CharacterSO.skill2Delay)
            {
                skillCoolTime2 = 0f;

                _elementalType = _elementalType == ElementalType.Count ? ElementalType.Wind : _elementalType;
                _elemental[(uint)_elementalType].SetActive(true);


                switch (_elementalType)
                {
                    case ElementalType.Wind:
                        Sound.SoundManager.Instance.PlayEFF("se_common_swing_04");
                        var wind = PoolManager.GetItem("Assets/Prefabs/WindSkill.prefab").GetComponent<WindSkill>();
                        wind.SetWindSkill(_character, _character.HitBoxDataSO.hitBoxDatasList[1].hitBoxDatas[0], _character.transform.position);
                        Vector3 dir = new Vector3(_character.GetCharacterComponent<CharacterMove>().InputDirection.x * 5f, _character.GetCharacterComponent<CharacterMove>().InputDirection.y + 5f, 0f);

                        _character.Rigidbody.velocity = Vector3.zero;
                        _character.Rigidbody.AddForce(dir, ForceMode.Impulse);

                        break;
                    case ElementalType.Fire:
                        Sound.SoundManager.Instance.PlayEFF("se_item_firebar_l");
                        PoolManager.GetItem("Assets/Prefabs/FireBite.prefab").GetComponent<FireBiteSkill>().SetFireBiteSkill(_character, _character.HitBoxDataSO.hitBoxDatasList[2].hitBoxDatas[0], _character.transform.position + (_character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? new Vector3(0.1f, 0.2f, 0f) : new Vector3(-0.1f, 0.2f, 0f)), _character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? Vector3.right : Vector3.left);
                        break;
                    case ElementalType.Earth:
                        Sound.SoundManager.Instance.PlayEFF("se_item_barrel_landing");
                        if (!_isEarthGolemSpawn)
                        {
                            _isEarthGolemSpawn = true;
                            PoolManager.GetItem("Assets/Prefabs/EarthGolem.prefab").GetComponent<EarthGolem>().SetEarthGolem(_character, _character.HitBoxDataSO.hitBoxDatasList[3].hitBoxDatas[0], _character.transform.position + (_character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? new Vector3(0.1f, 0.25f, 0f) : new Vector3(-0.1f, 0.25f, 0f)), _character.GetCharacterComponent<CharacterSprite>().Direction);
                        }
                        break;
                    case ElementalType.Water:
                        Sound.SoundManager.Instance.PlayEFF("se_common_water_hit_l");
                        PoolManager.GetItem("Assets/Prefabs/WaterBeam.prefab").GetComponent<WaterBeam>().SetWaterBeam(_character, _character.HitBoxDataSO.hitBoxDatasList[4].hitBoxDatas[0], _character.transform.position + (_character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? new Vector3(0.5f, 0.125f, 0f) : new Vector3(-0.5f, 0.125f, 0f)), _character.GetCharacterComponent<CharacterSprite>().Direction);
                        break;
                    default:
                        Debug.LogError("ElementalType Error");
                        break;
                }
                Skill2Action();
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {
            if (CharacterLevel.Level > 3 && !CharacterLevel.IsAllStarSkillUse)
            {
                isCanUseSkill3 = false;
                characterLevel.IsAllStarSkillUse = true;
                Sound.SoundManager.Instance.PlayEFF("se_common_boss_core_hit");
                CameraManager.SetAllStar(Character.transform);
                RoundManager.StaticSetInputSturnTime(1f);
                RoundManager.StaticStopMove(1f);
                AllStarSkillUse?.Invoke();
                Character.StartCoroutine(AllStarSkill());
                AllStarSkillAction();
            }
        }, EventType.KEY_DOWN);
    }

    public override void Update()
    {
        base.Update();
        if (skillCoolTime1 < Character.CharacterSO.skill1Delay)
        {
            skillCoolTime1 += Time.deltaTime;
            skill1CoolTimeChange?.Invoke();
            isCanUseSkill1 = false;
        }
        else if (CharacterLevel.Level > 1)
        {
            isCanUseSkill1 = true;
        }

        if (skillCoolTime2 < Character.CharacterSO.skill2Delay)
        {
            skillCoolTime2 += Time.deltaTime;
            skill2CoolTimeChange?.Invoke();
            isCanUseSkill2 = false;
        }
        else if (CharacterLevel.Level > 2)
        {
            isCanUseSkill2 = true;
        }


        if (!CharacterLevel.IsAllStarSkillUse && CharacterLevel.Level > 3)
        {
            isCanUseSkill3 = true;
        }

    }

    private IEnumerator AllStarSkill()
    {
        yield return new WaitForSeconds(1f);

        if (RoundManager.ReturnIsSetting())
        {
            Sound.SoundManager.Instance.PlayEFF("se_common_fire_ll_damage");
            Debug.Log(_character.GetCharacterComponent<CharacterAttack>().TargetCharacterDamage.Character.transform.position);
            PoolManager.GetItem("Assets/Prefabs/FireBeamALLSTAR.prefab").GetComponent<FireBeamALLSTARSkill>().SetFireBeamALLSTARSkill(_character, _character.HitBoxDataSO.hitBoxDatasList[5].hitBoxDatas[0], _character.GetCharacterComponent<CharacterAttack>().TargetCharacterDamage.Character.transform.position);
        }
    }
}
