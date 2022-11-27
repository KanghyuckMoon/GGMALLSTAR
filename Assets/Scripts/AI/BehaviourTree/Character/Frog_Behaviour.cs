using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;

public class Frog_Behaviour : BehaviourTree
{
	public override void SetNode()
	{
		//ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("JaebyComboSO");
		//NodeSetting
		_rootNode =
			Selector
			(
				//new ConditionCheckNode(IsAttackCombo, new ComboNode(comboSO, HoldKey, UpKey, TapKey)),
				IfAction(FoolActionCondition, FoolAction),
				IfAction(JumpCondition, Jump),
				IfAction(AttackJCondition, AttackJ),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//이동 시퀀스
				RandomChoice
				(
					Action(FixedMove),
					Action(Jump)
				)
			);
	}

	protected bool AttackJCondition()
	{
		//float randomDistance = Random.Range(0.1f, 1f);
		float attackDistance = 0.5f;
		bool distanceCondition = false;
		bool directionCondition = false;


		//거리
		if (Mathf.Abs(opCharacter.transform.position.x - mainCharacter.transform.position.x) < attackDistance)
		{
			distanceCondition = true;
		}
		else
		{
			distanceCondition = false;
		}


		if (opCharacter.transform.position.x < mainCharacter.transform.position.x && !isRight)
		{
			directionCondition = true;
		}
		else if (opCharacter.transform.position.x > mainCharacter.transform.position.x && isRight)
		{
			directionCondition = true;
		}

		//방향

		return distanceCondition;
	}

	protected bool JumpCondition()
	{
		if (Mathf.Abs(opCharacter.transform.position.y - mainCharacter.transform.position.y) > 0.5f)
		{
			return true;
		}
		return false;
	}

	protected void FixedMove()
	{
		float fixedDistance = 0.5f;
		float closeDistance = 1f;

		if (Mathf.Abs(opCharacter.transform.position.x - mainCharacter.transform.position.x) > fixedDistance)
		{
			CloseMove();
		}
		else if (Mathf.Abs(opCharacter.transform.position.x - mainCharacter.transform.position.x) < closeDistance)
		{
			FerMove();
		}
	}


	protected void AttackJ()
	{
		if (opCharacter.transform.position.x < mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.A);
		}
		else if (opCharacter.transform.position.x > mainCharacter.transform.position.x)
		{
			aiTestInput.SingleHoldInputKey(KeyCode.D);
		}

		TapKey(KeyCode.J);
	}

	protected bool IsAttackCombo()
	{
		if (isComboOn || IsAttackHit())
		{
			return true;
		}
		return false;
	}

	protected bool IsAttackHit()
	{
		if (AttackCondition("Attack"))
		{
			isComboOn = true;
			return true;
		}
		return false;
	}

	protected bool FoolActionCondition()
	{
		int random = Random.Range(0, 15);

		return random == 0;
	}

	protected void FoolAction()
	{
		AttackJ();
	}

}
