using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : CharacterComponent
{
    public CharacterSprite(Character character) : base(character)
    {
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

    protected override void Awake()
    {
        _spriteRenderer = Character.GetComponentInChildren<SpriteRenderer>();
    }

    private SpriteRenderer _spriteRenderer = null;
    private Direction _direction = Direction.RIGHT;
    public Direction Direction => _direction;
}
