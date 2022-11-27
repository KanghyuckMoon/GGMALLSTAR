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
	protected int random = 0;
	protected bool isComboOn = false;

	public virtual void Init(Character opCh, Character mainCh, CharacterAIInput aiTestInput)
	{
		this.aiTestInput = aiTestInput;
		opCharacter = opCh;
		mainCharacter = mainCh;
		SetNode();
	}

	public virtual void SetNode()
	{
		//ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("TestComboSO");
		//NodeSetting
		_rootNode =
			Selector
			(
				//ÀÌµ¿ ½ÃÄö½º
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, FerMove),
					IfAction(MoveCondition, Jump)
				)

				//new ConditionCheckNode(AttackCondition, new ComboNode(comboSO, HoldKey, UpKey, TapKey))
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
		}
		else if(opCharacter.transform.position.x > mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.D);
		}
		Debug.Log("AI Close Move");
	}
	protected void FerMove()
	{
		if (opCharacter.transform.position.x < mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.D);
		}
		else if (opCharacter.transform.position.x > mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.A);
		}
		Debug.Log("AI Fer Move");
	}
	protected void Jump()
	{
		aiTestInput.TapInputKey(KeyCode.W);
		Debug.Log("AI Jump");
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
		//aiTestInput.FalseInputKey();
		aiTestInput.TapInputKey(KeyCode.J);
		Debug.Log("AI Attack");
	}

	protected void TapKey(KeyCode keyCode)
	{
		aiTestInput.TapInputKey(keyCode);
		Debug.Log($"AI Input{keyCode}");
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
