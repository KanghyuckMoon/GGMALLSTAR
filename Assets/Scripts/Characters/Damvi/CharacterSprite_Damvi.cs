using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterSprite_Damvi : CharacterSprite
{
	public CharacterSprite_Damvi(Character character) : base(character)
	{
	}

	protected override void SetEvent()
	{
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _model.transform.localEulerAngles = new Vector3(0, -140, 0);
            _direction = Direction.LEFT;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _model.transform.localEulerAngles = new Vector3(0, 140, 0);
            _direction = Direction.RIGHT;
        }, EventType.KEY_DOWN);
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _model.transform.localEulerAngles = new Vector3(0, -140, 0);
            _direction = Direction.LEFT;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _model.transform.localEulerAngles = new Vector3(0, 140, 0);
            _direction = Direction.RIGHT;
        }, EventType.KEY_HOLD);
    }


    protected override void Awake()
    {
        _model = Character.transform.GetChild(0).gameObject;
        _originPosition = _model.transform.localPosition;
        _originScale = _model.transform.localScale;
        _originRotation = _model.transform.localEulerAngles;
    }
}
