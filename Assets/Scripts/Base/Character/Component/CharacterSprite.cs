using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSprite : CharacterComponent
{
    public SpriteRenderer SpriteRenderer
	{
        get
		{
            return _spriteRenderer;
        }
	}

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

	public virtual void ResetModelPosition()
	{
        _model.transform.localPosition = _originPosition;
    }

    protected override void Awake()
    {
        _spriteRenderer = Character.GetComponentInChildren<SpriteRenderer>();
        _model = Character.transform.GetChild(0).gameObject;
        _originPosition = _spriteRenderer.transform.localPosition;
        _originScale = _spriteRenderer.transform.localScale;
        _originRotation = _spriteRenderer.transform.localEulerAngles;
    }

    protected Vector3 _originPosition;
    protected Vector3 _originRotation;
    protected Vector3 _originScale;
    protected SpriteRenderer _spriteRenderer = null;
    protected GameObject _model = null;
    protected Direction _direction = Direction.RIGHT;
    public Direction Direction => _direction;
}
