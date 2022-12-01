using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterSO _characterSO = null;
    public CharacterSO CharacterSO => _characterSO;

    [SerializeField]
    private CharacterLevelSO _characterLevelSO = null;

    public CharacterLevelSO CharacterLevelSO => _characterLevelSO;


    [SerializeField]
    private InputDataBaseSO _inputDataBaseSO = null;
    public InputDataBaseSO InputDataBaseSO
    {
        get
        {
            return _inputDataBaseSO;
        }
        set
        {
            _inputDataBaseSO = value;
        }
    }


    [SerializeField]
    private HitBoxDataSO _hitBoxDataSO = null;
    public HitBoxDataSO HitBoxDataSO => _hitBoxDataSO;

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

    private CharacterEvent _characterEvent = null;
    public CharacterEvent CharacterEvent => _characterEvent;

    private Animator _animator = null;
    public Animator Animator => _animator;

    private Rigidbody _rigidbody = null;
    public Rigidbody Rigidbody => _rigidbody;

    private BoxCollider _collider = null;
    public BoxCollider Collider => _collider;


    #region Unity Methods

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
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
            characterComponent?.Update();
        }
    }

    private void LateUpdate()
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent?.LateUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent?.FixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent?.OnCollisionEnter(other);
        }
    }
    private void OnCollisionStay(Collision other)
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent?.OnCollisionStay(other);
        }
    }


    private void OnCollisionExit(Collision other)
    {
        foreach (CharacterComponent characterComponent in _characterComponents.Values)
        {
            characterComponent?.OnCollisionExit(other);
        }
    }
    #endregion

    protected void AddComponent(ComponentType componentType, CharacterComponent characterComponent)
    {
        _characterComponents.Add(componentType, characterComponent);
    }

    protected virtual void SetComponent() { }

    public void OnAttack(int hitBoxIndex)
    {
        CharacterAttack characterAttack = _characterComponents[ComponentType.Attack] as CharacterAttack;
        characterAttack.OnAttack(hitBoxIndex);
    }
}
