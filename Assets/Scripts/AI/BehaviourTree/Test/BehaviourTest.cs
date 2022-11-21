using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class BehaviourTest
{
	private INode _rootNode;

	private Character opCharacter;
	private Character mainCharacter;
	private AITestInput aiTestInput;

	public BehaviourTest(Character opCh, Character mainCh, AITestInput aiTestInput)
	{
		this.aiTestInput = aiTestInput;
		opCharacter = opCh;
		mainCharacter = mainCh;
		  INode[] nodes = { new IfActionNode(MoveCondition, Move), new IfActionNode(AttackCondition, Attack) };
		_rootNode = new SelectorNode(nodes);
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

	private void Move()
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
		aiTestInput.FalseInputKey();
		aiTestInput.TapInputKey(KeyCode.J);
		Debug.Log("AI Attack");
	}
}
