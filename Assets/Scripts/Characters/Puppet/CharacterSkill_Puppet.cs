using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Pool;

public class CharacterSkill_Puppet : CharacterSkill
{
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
        // Elemental Transform Caching
        _elementalTransform = (character as Character_Puppet)?.ElementalTransform;

        // Elemental variable init
        _elemental = new GameObject[(uint)ElementalType.Count];

        // Elemental Prefab Caching
        _elemental[(uint)ElementalType.Wind] = PoolManager.GetItem("Assets/Prefabs/Wind.prefab");
        _elemental[(uint)ElementalType.Fire] = PoolManager.GetItem("Assets/Prefabs/Fire.prefab");
        _elemental[(uint)ElementalType.Earth] = PoolManager.GetItem("Assets/Prefabs/Earth.prefab");
        _elemental[(uint)ElementalType.Water] = PoolManager.GetItem("Assets/Prefabs/Water.prefab");


        // Elemental Default Setting
        for (int i = 0; i < _elemental.Length; i++)
        {
            _elemental[i].transform.SetParent(_elementalTransform);
            _elemental[i].transform.localPosition = Vector3.zero;
            _elemental[i].SetActive(false);
        }

        CharacterEvent.AddEvent(EventKeyWord.SKILL_1, () =>
        {
            _elementalType = _elementalType == ElementalType.Count ? ElementalType.Wind : (ElementalType)(((uint)_elementalType + 1) % (uint)ElementalType.Count);

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

        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.SKILL_2, () =>
        {
            switch (_elementalType)
            {
                case ElementalType.Wind:
                    var wind = PoolManager.GetItem("Assets/Prefabs/WindSkill.prefab").GetComponent<WindSkill>();
                    wind.SetWindSkill(_character.transform.position);

                    Vector3 dir = new Vector3(_character.GetCharacterComponent<CharacterMove>().InputDirection.x * 5f, _character.GetCharacterComponent<CharacterMove>().InputDirection.y + 5f, 0f);

                    _character.Rigidbody.velocity = Vector3.zero;
                    _character.Rigidbody.AddForce(dir, ForceMode.Impulse);

                    break;
                case ElementalType.Fire:
                    PoolManager.GetItem("Assets/Prefabs/FireBite.prefab").GetComponent<FireBiteSkill>().SetFireBiteSkill(_character, _character.transform.position + (_character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? new Vector3(0.1f, 0.2f, 0f) : new Vector3(-0.1f, 0.2f, 0f)), _character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? Vector3.right : Vector3.left);
                    break;
                case ElementalType.Earth:
                    if (!_isEarthGolemSpawn)
                    {
                        _isEarthGolemSpawn = true;
                        PoolManager.GetItem("Assets/Prefabs/EarthGolem.prefab").GetComponent<EarthGolem>().SetEarthGolem(_character, _character.transform.position + (_character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? new Vector3(0.1f, 0.25f, 0f) : new Vector3(-0.1f, 0.25f, 0f)), _character.GetCharacterComponent<CharacterSprite>().Direction);
                    }
                    break;
                case ElementalType.Water:
                    PoolManager.GetItem("Assets/Prefabs/WaterBeam.prefab").GetComponent<WaterBeam>().SetWaterBeam(_character, _character.transform.position + (_character.GetCharacterComponent<CharacterSprite>().Direction == Direction.RIGHT ? new Vector3(0.5f, 0.125f, 0f) : new Vector3(-0.5f, 0.125f, 0f)), _character.GetCharacterComponent<CharacterSprite>().Direction);
                    break;
                default:
                    Debug.LogError("ElementalType Error");
                    break;
            }
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {

        }, EventType.KEY_DOWN);
    }
}
