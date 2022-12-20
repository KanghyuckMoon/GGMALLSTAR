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

    // ĳ���� �� Getter
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
        // �̺�Ʈ ����
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
    /// ĳ���� �� ��ġ �ʱ�ȭ
    /// </summary>
	public virtual void ResetModelPosition()
    {
        _model.transform.localPosition = _originPosition;
    }

    protected override void Awake()
    {
        // ���� �Ҵ�
        _spriteRenderer = Character.GetComponentInChildren<SpriteRenderer>();
        _model = Character.transform.GetChild(0).gameObject;
        _originPosition = _spriteRenderer.transform.localPosition;
        _originScale = _spriteRenderer.transform.localScale;
        _originRotation = _spriteRenderer.transform.localEulerAngles;
    }

    // ���� ��ġ
    protected Vector3 _originPosition;
    // ���� ����
    protected Vector3 _originRotation;
    // ���� ũ��
    protected Vector3 _originScale;
    // SpriteRenderer ĳ�� ����
    protected SpriteRenderer _spriteRenderer = null;
    // ĳ���� �� ĳ�� ����
    protected GameObject _model = null;
    // ĳ������ ����
    protected Direction _direction = Direction.RIGHT;
    // ĳ������ ���� Getter
    public Direction Direction => _direction;
}
