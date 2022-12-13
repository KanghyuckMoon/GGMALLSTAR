using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;

public class BehaviourTree
{
	protected INode _rootNode;
	protected Character opCharacter;
	protected Character mainCharacter;
	protected CharacterAIInput aiTestInput;
	protected Dictionary<string, bool> isHitBoxHit = new Dictionary<string, bool>();
	protected List<string> isHitBoxActionNames = new List<string>();
	protected bool isComboOn = false;
	protected bool isRight;


	public virtual void Init(Character opCh, Character mainCh, CharacterAIInput aiTestInput, int level = 1)
	{
		this.aiTestInput = aiTestInput;
		opCharacter = opCh;
		mainCharacter = mainCh;
		SetNode(level);
	}

	public virtual void SetNode(int level = 1)
	{
		//NodeSetting
		_rootNode =
			Selector
			(
				//�̵� ������
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, FerMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}

	public void IsHit(string actionName)
	{
		if (isHitBoxHit.TryGetValue(actionName, out bool value))
		{
			isHitBoxHit[actionName] = true;
		}
		else
		{
			isHitBoxHit.Add(actionName, false);
			isHitBoxActionNames.Add(actionName);
		}
	}

	public void Update()
	{
		_rootNode.Run();
	}

	protected bool MoveCondition()
	{
		if(Vector2.Distance(opCharacter.transform.position, mainCharacter.transform.position) < 0.5f)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	protected void MoveFalse()
	{
		aiTestInput.FalseInputKey(KeyCode.D);
		aiTestInput.FalseInputKey(KeyCode.A);
	}

	
	protected void IsComboFalse()
	{
		isComboOn = false;
	}

	protected void IsHitFalse()
	{
		for (int i = 0; i < isHitBoxActionNames.Count; ++i)
		{
			isHitBoxHit[isHitBoxActionNames[i]] = false;
		}
	}

	protected bool AttackCondition(string actionName)
	{
		if (isHitBoxHit.TryGetValue(actionName, out bool value))
		{
			return value;
		}
		else
		{
			isHitBoxHit.Add(actionName, false);
			isHitBoxActionNames.Add(actionName);
		}
		return false;
	}

	protected void CloseMove()
	{
		if (opCharacter.transform.position.x < mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.A);
			isRight = false;
		}
		else if(opCharacter.transform.position.x > mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.D);
			isRight = true;
		}
	}
	protected void FerMove()
	{
		if (opCharacter.transform.position.x < mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.D);
			isRight = true;
		}
		else if (opCharacter.transform.position.x > mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.A);
			isRight = false;
		}
	}
	protected void Jump()
	{
		aiTestInput.TapInputKey(KeyCode.W);
	}

	protected float _delay = 0.1f;

	protected void Attack()
	{
		_delay -= Time.deltaTime;
		if (_delay < 0f)
		{
			_delay = 0.1f;
		}
		else
		{
			return;
		}
		aiTestInput.TapInputKey(KeyCode.J);
	}

	protected void TapKey(KeyCode keyCode)
	{
		aiTestInput.TapInputKey(keyCode);
	}
	protected void UpKey(KeyCode keyCode)
	{
		aiTestInput.FalseInputKey(keyCode);
	}
	protected void HoldKey(KeyCode keyCode)
	{
		aiTestInput.MultipleHoldInputKey(keyCode);
	}
}
