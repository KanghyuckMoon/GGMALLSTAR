using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterDodge : CharacterComponent
{
	private float _sturnTime = 0f;
	private float _coolTime = 0f;
	private System.Action changeDodgeCoolTime;

	public float CoolTime
	{
		get
		{
			return _coolTime;
		}
	}
	public float CoolTimeRatio
	{
		get
		{
			return _coolTime / 0.5f;
		}
	}

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
	public void AddChangeDodgeCoolTimeAction(System.Action action)
	{
		changeDodgeCoolTime += action;
	}
	public void SetSturnTime(float time)
	{
		_sturnTime = time;
	}
	public void SetCoolTime(float time)
	{
		_coolTime = time;
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();

		if (_sturnTime > 0f)
		{
			_sturnTime -= Time.fixedDeltaTime;
			return;
		}
		if (_coolTime > 0f)
		{
			_coolTime -= Time.fixedDeltaTime;
			changeDodgeCoolTime?.Invoke();
		}
	}

	public void Dodge()
	{
		if (!Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
		{
			Character.Rigidbody.velocity = Vector3.zero;
			return;
		}
		if (_sturnTime > 0f)
		{
			return;
		}
		if (_coolTime > 0f)
		{
			return;
		}

		Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Dirty_02, Character.transform.position);
		Character.StartCoroutine(ReturnState(Character.tag));
		Character.tag = "Invincibility";

		var characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);  

		if (characterInput == null)
		{
			var aiInput = Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
			aiInput.SetInputDelayTime(0.2f);
		}
		else
		{
			characterInput.SetInputDelayTime(0.2f);
		}

		
		Character.GetCharacterComponent<CharacterColor>(ComponentType.Color).SetWhiteMaterial();
		Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation).SetAnimationTrigger(AnimationType.Dodge);
		Character.GetCharacterComponent<CharacterMove>(ComponentType.Move).SetMoveDirection(_inputDirection);
		SetCoolTime(0.5f);

		var velocity = Character.Rigidbody.velocity;
		velocity.x = 0f;
		Character.Rigidbody.velocity = velocity;
		Character.Rigidbody.AddForce(_inputDirection * Character.CharacterSO.DodgeSpeed, ForceMode.Impulse);
	}

	private IEnumerator ReturnState(string originTag)
	{
		yield return new WaitForSeconds(0.2f);
		Character.GetCharacterComponent<CharacterColor>(ComponentType.Color).SetOriginMaterial();
		Character.tag = originTag;
	}

}
