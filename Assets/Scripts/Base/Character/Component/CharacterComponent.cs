using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

/// <summary>
/// Character�� ������Ʈ���� �θ� Ŭ����
/// </summary>
public abstract class CharacterComponent
{
    // Component�� ������ �ִ� Character
    protected Character _character = null;
    // Character Getter
    public Character Character => _character;

    // CharacterEvent ĳ��
    private CharacterEvent _characterEvent = null;
    // CharacterEvent Getter
    public CharacterEvent CharacterEvent => _characterEvent;

    /// <summary>
    /// CharacterComponent ������
    /// </summary>
    /// <param name="character"></param>
    public CharacterComponent(Character character)
    {
        // Character �Ҵ�
        _character = character;
        // CharacterEvent �Ҵ�
        _characterEvent = character.CharacterEvent;
        // Awake() ȣ��
        Awake();
        // SetEvent() ȣ��
        SetEvent();
    }

    /// <summary>
    /// CharacterComponent ������
    /// </summary>
    /// <param name="character"></param>
    /// <param name="characterEvent"></param>
    public CharacterComponent(Character character, CharacterEvent characterEvent)
    {
        // Character �Ҵ�
        _character = character;
        // CharacterEvent �Ҵ�
        _characterEvent = characterEvent;
        // Awake() ȣ��
        Awake();
        // SetEvent() ȣ��
        SetEvent();
    }

    /// <summary>
    /// CharacterComponent�� Event ����� ��Ƶδ� �Լ�
    /// </summary>
    protected virtual void SetEvent() { }

    /// <summary>
    /// CharacterComponent�� ������ ���� �������� ȣ��Ǵ� �Լ�
    /// </summary>
    protected virtual void Awake() { }

    /// <summary>
    /// Awake() ���Ŀ� Character���� ȣ��Ǵ� �Լ�
    /// </summary>
    public virtual void Start() { }

    /// <summary>
    /// Character�� Update()�� ȣ��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Character�� FixedUpdate()�� ȣ��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public virtual void FixedUpdate() { }

    /// <summary>
    /// Character�� LateUpdate()�� ȣ��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public virtual void LateUpdate() { }

    /// <summary>
    /// Character�� OnColliderEnter()�� ȣ��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnCollisionEnter(Collision other) { }

    /// <summary>
    /// Character�� OnColliderStay()�� ȣ��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnCollisionStay(Collision other) { }

    /// <summary>
    /// Character�� OnColliderExit()�� ȣ��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnCollisionExit(Collision other) { }
}
