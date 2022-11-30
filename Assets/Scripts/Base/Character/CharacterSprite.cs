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

    public CharacterSprite(Character character) : base(character)
    {
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

    public void ResetModelPosition()
	{
        SpriteRenderer.transform.localPosition = _originVector;

    }

    protected override void Awake()
    {
        _spriteRenderer = Character.GetComponentInChildren<SpriteRenderer>();
        _originVector = _spriteRenderer.transform.localPosition;
    }

    private Vector3 _originVector;
    private SpriteRenderer _spriteRenderer = null;
    private Direction _direction = Direction.RIGHT;
    public Direction Direction => _direction;
}
