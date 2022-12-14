using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;


public class Jaeby_Behaviour : BehaviourTree
{
	private CharacterSkill_Jaeby characterSkill_Jaeby;
	private CharacterSkill_Jaeby CharacterAIInput_Jaeby
	{
		get
		{
			characterSkill_Jaeby ??= mainCharacter.GetCharacterComponent<CharacterSkill_Jaeby>(ComponentType.Skill1);
			return characterSkill_Jaeby;
		}
	}

	public override void Init(Character opCh, Character mainCh, CharacterAIInput aiTestInput, int level)
	{
		base.Init(opCh, mainCh, aiTestInput, level);
	}

	public override void SetNode(int level)
	{
		switch (level)
		{
			default:
				_rootNode = Level1();
				break;
			case 2:
				_rootNode = Level2();
				break;
			case 3:
				_rootNode = Level3();
				break;
			case 4:
				_rootNode = Level4();
				break;
			case 5:
				_rootNode = Level5();
				break;
		}
	}

	private INode Level1()
	{
		return 	Selector
			(
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				//IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove)
					//IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level2()
	{
		//ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("JaebyComboSO");
		//NodeSetting
		return Selector
			(
				//new ConditionCheckNode(IsAttackCombo, new ComboNode(comboSO, HoldKey, UpKey, TapKey)),
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				//IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level3()
	{
		//ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("JaebyComboSO");
		//NodeSetting
		return Selector
			(
				//new ConditionCheckNode(IsAttackCombo, new ComboNode(comboSO, HoldKey, UpKey, TapKey)),
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level4()
	{
		ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("JaebyComboSO");
		//NodeSetting
		return Selector
			(
				new ConditionCheckNode(IsAttackCombo, new ComboNode(comboSO, HoldKey, UpKey, TapKey)),
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level5()
	{
		ComboSO comboSO = Addressable.AddressablesManager.Instance.GetResource<ComboSO>("JaebyComboSO");
		//NodeSetting
		return Selector
			(
				new ConditionCheckNode(IsAttackCombo, new ComboNode(comboSO, HoldKey, UpKey, TapKey)),
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}


	protected bool AttackJCondition()
	{
		float randomDistance = 0.3f; //Random.Range(0.1f, 1f);
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
	protected bool DodgeCondition()
	{
		int random = Random.Range(0, 15);

		return random == 0 && !MoveCondition();
	}
	protected void Dodge()
	{
		TapKey(KeyCode.K);
	}

	protected bool Skill1Condition()
	{
		return CharacterAIInput_Jaeby.IsCanUseSkill1;
	}
	protected bool Skill2Condition()
	{
		return CharacterAIInput_Jaeby.IsCanUseSkill2;
	}
	protected bool AllStarSkillCondition()
	{
		return CharacterAIInput_Jaeby.IsCanUseSkill3;
	}

	protected void UseSkill1()
	{
		TapKey(KeyCode.U);
	}
	protected void UseSkill2()
	{
		TapKey(KeyCode.I);
	}
	protected void UseAllStarSkill()
	{
		TapKey(KeyCode.O);
	}
}
