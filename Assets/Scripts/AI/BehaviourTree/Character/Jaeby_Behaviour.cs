using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;


public class Jaeby_Behaviour : BehaviourTree
{
	public override void SetNode()
	{
		ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("JaebyComboSO");
		//NodeSetting
		_rootNode =
			Selector
			(
				new ConditionCheckNode(IsAttackCombo, new ComboNode(comboSO, HoldKey, UpKey, TapKey)),
				IfAction(AttackJCondition, AttackJ),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					//IfAction(MoveCondition, FerMove),
					IfAction(MoveCondition, Jump)
				)

			);
	}

	protected bool AttackJCondition()
	{
		float randomDistance = Random.Range(0.1f, 1f);
		bool distanceCondition = false;
		bool directionCondition = false;


		//거리
		if (Mathf.Abs( opCharacter.transform.position.x - mainCharacter.transform.position.x) < randomDistance)
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

		return distanceCondition && directionCondition;
	}

	protected void AttackJ()
	{
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
		if(AttackCondition("Attack"))
		{
			isComboOn = true;
			return true;
		}
		return false;
	}
}
