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

				//ÀÌµ¿ ½ÃÄö½º
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
		float randomDistance = Random.Range(0.3f, 0.6f);

		if (Mathf.Abs( opCharacter.transform.position.x - mainCharacter.transform.position.x) < randomDistance)
		{
			return true;
		}
		else
		{
			return false;
		}
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
