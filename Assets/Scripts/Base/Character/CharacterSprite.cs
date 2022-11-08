using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : CharacterComponent
{
    public CharacterSprite(Character character) : base(character)
    {
        Character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.LEFT, () => { _spriteRenderer.flipX = true; });
        Character.GetCharacterComponent<CharacterEvent>().AddEvent(EventKeyWord.RIGHT, () => { _spriteRenderer.flipX = false; });
    }

    protected override void Awake()
    {
        _spriteRenderer = Character.GetComponentInChildren<SpriteRenderer>();
    }

    private SpriteRenderer _spriteRenderer = null;
}
