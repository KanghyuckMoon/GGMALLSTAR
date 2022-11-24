using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;

public class BehaviourTree
{
	private INode _rootNode;

	private Character opCharacter;
	private Character mainCharacter;
	private CharacterAIInput aiTestInput;
	private int random = 0;

	public void Init(Character opCh, Character mainCh, CharacterAIInput aiTestInput)
	{
		this.aiTestInput = aiTestInput;
		opCharacter = opCh;
		mainCharacter = mainCh;

		ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("TestComboSO");
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
				),

				new ConditionCheckNode(AttackCondition, new ComboNode(comboSO, HoldKey, UpKey, TapKey))
			);
	}

	public void Update()
	{
		_rootNode.Run();
	}

	private bool MoveCondition()
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

	private bool AttackCondition()
	{
		if (Vector2.Distance(opCharacter.transform.position, mainCharacter.transform.position) < 0.5f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void CloseMove()
	{
		if (opCharacter.transform.position.x < mainCharacter.transform.position.x)
		{
			aiTestInput.HoldInputKey(KeyCode.A);
		}
		else if(opCharacter.transform.position.x > mainCharacter.transform.position.x)
		{
			aiTestInput.HoldInputKey(KeyCode.D);
		}
		Debug.Log("AI Move");
	}
	private void FerMove()
	{
		if (opCharacter.transform.position.x < mainCharacter.transform.position.x)
		{
			aiTestInput.HoldInputKey(KeyCode.D);
		}
		else if (opCharacter.transform.position.x > mainCharacter.transform.position.x)
		{
			aiTestInput.HoldInputKey(KeyCode.A);
		}
		Debug.Log("AI Move");
	}
	private void Jump()
	{
		aiTestInput.HoldInputKey(KeyCode.W);
		Debug.Log("AI Jump");
	}

	private float _delay = 0.1f;

	private void Attack()
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

	private void TapKey(KeyCode keyCode)
	{
		aiTestInput.TapInputKey(keyCode);
		Debug.Log($"AI Input{keyCode}");
	}
	private void UpKey(KeyCode keyCode)
	{
		aiTestInput.FalseInputKey(keyCode);
	}
	private void HoldKey(KeyCode keyCode)
	{
		aiTestInput.HoldInputKey(keyCode);
	}
}
