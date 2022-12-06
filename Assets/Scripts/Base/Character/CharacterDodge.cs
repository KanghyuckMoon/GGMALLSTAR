using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterDodge : CharacterComponent
{
	private float _sturnTime = 0f;

	private bool _isRight = false;
	private Vector2 _inputDirection = Vector2.zero;
	public CharacterDodge(Character character) : base(character)
	{
		CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
		{
			_inputDirection.x = -1;
		}, EventType.KEY_DOWN);

		CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
		{
			_inputDirection.x = 1;
		}, EventType.KEY_DOWN);

		CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
		{
			_inputDirection.x = -1;
		}, EventType.KEY_HOLD);

		CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
		{
			_inputDirection.x = 1;
		}, EventType.KEY_HOLD);

		CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
		{
			_inputDirection.x = 0;
		}, EventType.KEY_UP);

		CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
		{
			_inputDirection.x = 0;
		}, EventType.KEY_UP);
	}


	protected override void SetEvent()
	{
		base.SetEvent();
		CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
		{
			_isRight = false;
		}, EventType.KEY_DOWN);

		CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
		{
			_isRight = true;
		}, EventType.KEY_DOWN);
		CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
		{
			_isRight = false;
		}, EventType.KEY_HOLD);

		CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
		{
			_isRight = true;
		}, EventType.KEY_HOLD);
		CharacterEvent.AddEvent(EventKeyWord.DODGE, () =>
		{
			Dodge();
		}, EventType.KEY_DOWN);
	}
	public void SetSturnTime(float time)
	{
		_sturnTime = time;
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();

		if (_sturnTime > 0f)
		{
			_sturnTime -= Time.fixedDeltaTime;
			return;
		}
	}

	public void Dodge()
	{
		if(_sturnTime > 0)
		{
			return;
		}

		Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Dirty_02, Character.transform.position);
		Character.StartCoroutine(ReturnState(Character.tag));
		Character.tag = "Invincibility";
		Character.GetCharacterComponent<CharacterInput>().SetInputDelayTime(0.2f);
		Character.GetCharacterComponent<CharacterColor>().SetWhiteMaterial();
		Character.GetCharacterComponent<CharacterMove>().SetMoveDirection(_inputDirection);
		SetSturnTime(0.5f);

		var velocity = Character.Rigidbody.velocity;
		velocity.x = 0f;
		Character.Rigidbody.velocity = velocity;
		Character.Rigidbody.AddForce(_inputDirection * Character.CharacterSO.DodgeSpeed, ForceMode.Impulse);
	}

	private IEnumerator ReturnState(string originTag)
	{
		yield return new WaitForSeconds(0.2f);
		Character.GetCharacterComponent<CharacterColor>().SetOriginMaterial();
		Character.tag = originTag;
	}

}
