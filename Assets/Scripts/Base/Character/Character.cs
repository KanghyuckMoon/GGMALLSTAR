using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected CharacterSO _characterSO = null;

    [SerializeField]
    protected InputDataBaseSO _inputDataBaseSO = null;
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

    protected CharacterEvent _characterEvent = null;
    public CharacterEvent CharacterEvent => _characterEvent;

    #region Unity Methods

    private void Awake()
    {
        _characterComponents = new();
        _characterEvent = new CharacterEvent();
    }

    private void Start()
    {
        SetComponent();
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

    private void OnCollisionEnter(Collision other)
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent.OnCollisionEnter(other);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent.OnCollisionExit(other);
        }
    }
    #endregion

    protected void AddComponent(ComponentType componentType, CharacterComponent characterComponent)
    {
        _characterComponents.Add(componentType, characterComponent);
    }

    protected virtual void SetComponent()
    {
        _characterComponents.Add(ComponentType.Input, new CharacterInput(this));
    }
}
