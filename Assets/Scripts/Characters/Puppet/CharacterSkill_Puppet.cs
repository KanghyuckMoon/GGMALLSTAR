using System.Diagnostics.Tracing;
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
                    _character.Rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
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

        CharacterEvent.AddEvent(EventKeyWord.ALL_STAR_SKILL, () =>
        {

        }, EventType.KEY_DOWN);
    }
}
