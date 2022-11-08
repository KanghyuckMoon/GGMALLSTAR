using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComponentType
{
    Attack,
    Move,
    Event,
    Input
}

public class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterSO _characterSO = null;

    [SerializeField]
    private InputDataBaseSO _inputDataBaseSO = null;
    public InputDataBaseSO InputDataBaseSO => _inputDataBaseSO;

    private Dictionary<ComponentType, CharacterComponent> _characterComponents = null;

    public T GetCharacterComponent<T>() where T : CharacterComponent
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            if (characterComponent is T)
            {
                return characterComponent as T;
            }
        }

        return null;
    }

    private void Start()
    {
        _characterComponents = new();

        _characterComponents.Add(ComponentType.Event, new CharacterEvent(this));
        _characterComponents.Add(ComponentType.Input, new CharacterInput(this));
        _characterComponents.Add(ComponentType.Move, new CharacterMove(this));
        _characterComponents.Add(ComponentType.Attack, new CharacterAttack(this));
    }

    private void Update()
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent.Update();
        }
    }

    private void LateUpdate()
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent.LateUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent.FixedUpdate();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent.OnCollisionExit(other);
        }
    }
}
