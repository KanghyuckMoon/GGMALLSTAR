using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

/// <summary>
/// Character의 컴포넌트들의 부모 클래스
/// </summary>
public abstract class CharacterComponent
{
    // Component를 가지고 있는 Character
    protected Character _character = null;
    // Character Getter
    public Character Character => _character;

    // CharacterEvent 캐싱
    private CharacterEvent _characterEvent = null;
    // CharacterEvent Getter
    public CharacterEvent CharacterEvent => _characterEvent;

    /// <summary>
    /// CharacterComponent 생성자
    /// </summary>
    /// <param name="character"></param>
    public CharacterComponent(Character character)
    {
        // Character 할당
        _character = character;
        // CharacterEvent 할당
        _characterEvent = character.CharacterEvent;
        // Awake() 호출
        Awake();
        // SetEvent() 호출
        SetEvent();
    }

    /// <summary>
    /// CharacterComponent 생성자
    /// </summary>
    /// <param name="character"></param>
    /// <param name="characterEvent"></param>
    public CharacterComponent(Character character, CharacterEvent characterEvent)
    {
        // Character 할당
        _character = character;
        // CharacterEvent 할당
        _characterEvent = characterEvent;
        // Awake() 호출
        Awake();
        // SetEvent() 호출
        SetEvent();
    }

    /// <summary>
    /// CharacterComponent의 Event 등록을 모아두는 함수
    /// </summary>
    protected virtual void SetEvent() { }

    /// <summary>
    /// CharacterComponent가 생성된 이후 다음으로 호출되는 함수
    /// </summary>
    protected virtual void Awake() { }

    /// <summary>
    /// Awake() 이후에 Character에서 호출되는 함수
    /// </summary>
    public virtual void Start() { }

    /// <summary>
    /// Character에 Update()가 호출될 때 호출되는 함수
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Character에 FixedUpdate()가 호출될 때 호출되는 함수
    /// </summary>
    public virtual void FixedUpdate() { }

    /// <summary>
    /// Character에 LateUpdate()가 호출될 때 호출되는 함수
    /// </summary>
    public virtual void LateUpdate() { }

    /// <summary>
    /// Character에 OnColliderEnter()가 호출될 때 호출되는 함수
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnCollisionEnter(Collision other) { }

    /// <summary>
    /// Character에 OnColliderStay()가 호출될 때 호출되는 함수
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnCollisionStay(Collision other) { }

    /// <summary>
    /// Character에 OnColliderExit()가 호출될 때 호출되는 함수
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnCollisionExit(Collision other) { }
}
