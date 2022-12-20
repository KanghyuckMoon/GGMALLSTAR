using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSprite : CharacterComponent
{
    // SpriteRenderer Getter
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return _spriteRenderer;
        }
    }

    // 캐릭터 모델 Getter
    public GameObject Model
    {
        get
        {
            return _model;
        }
    }

    public CharacterSprite(Character character) : base(character)
    {
    }

    protected override void SetEvent()
    {
        // 이벤트 설정
        base.SetEvent();
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _spriteRenderer.flipX = true;
            _direction = Direction.LEFT;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _spriteRenderer.flipX = false;
            _direction = Direction.RIGHT;
        }, EventType.KEY_DOWN);
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _spriteRenderer.flipX = true;
            _direction = Direction.LEFT;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _spriteRenderer.flipX = false;
            _direction = Direction.RIGHT;
        }, EventType.KEY_HOLD);
    }

    /// <summary>
    /// 캐릭터 모델 위치 초기화
    /// </summary>
	public virtual void ResetModelPosition()
    {
        _model.transform.localPosition = _originPosition;
    }

    protected override void Awake()
    {
        // 변수 할당
        _spriteRenderer = Character.GetComponentInChildren<SpriteRenderer>();
        _model = Character.transform.GetChild(0).gameObject;
        _originPosition = _spriteRenderer.transform.localPosition;
        _originScale = _spriteRenderer.transform.localScale;
        _originRotation = _spriteRenderer.transform.localEulerAngles;
    }

    // 기존 위치
    protected Vector3 _originPosition;
    // 기존 각도
    protected Vector3 _originRotation;
    // 기존 크기
    protected Vector3 _originScale;
    // SpriteRenderer 캐싱 변수
    protected SpriteRenderer _spriteRenderer = null;
    // 캐릭터 모델 캐싱 변수
    protected GameObject _model = null;
    // 캐릭터의 방향
    protected Direction _direction = Direction.RIGHT;
    // 캐릭터의 방향 Getter
    public Direction Direction => _direction;
}
