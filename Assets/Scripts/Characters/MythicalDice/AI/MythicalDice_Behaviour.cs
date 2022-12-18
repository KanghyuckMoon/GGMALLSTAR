using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;


public class MythicalDice_Behaviour : BehaviourTree
{
	private CharacterSkill_MythicalDice characterSkill_MythicalDice;
	private CharacterSkill_MythicalDice CharacterSkill_MythicalDice
	{
		get
		{
			characterSkill_MythicalDice ??= mainCharacter.GetCharacterComponent<CharacterSkill_MythicalDice>(ComponentType.Skill1);
			return characterSkill_MythicalDice;
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

				//�̵� ������
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove)
					//IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level2()
	{
		//NodeSetting
		return Selector
			(
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				//IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//�̵� ������
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level3()
	{
		//NodeSetting
		return Selector
			(
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//�̵� ������
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level4()
	{
		//NodeSetting
		return Selector
			(
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//�̵� ������
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove),
					IfAction(MoveCondition, Jump)
				)
			);
	}
	private INode Level5()
	{
		//NodeSetting
		return Selector
			(
				IfAction(Skill1Condition, UseSkill1),
				IfAction(Skill2Condition, UseSkill2),
				IfAction(AllStarSkillCondition, UseAllStarSkill),
				IfAction(AttackJCondition, AttackJ),
				IfAction(DodgeCondition, Dodge),

				IgnoreAction(IsHitFalse),
				IgnoreAction(IsComboFalse),

				//�̵� ������
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


		//�Ÿ�
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

		//����

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
		return CharacterSkill_MythicalDice.IsCanUseSkill1;
	}
	protected bool Skill2Condition()
	{
		return CharacterSkill_MythicalDice.IsCanUseSkill2;
	}
	protected bool AllStarSkillCondition()
	{
		return CharacterSkill_MythicalDice.IsCanUseSkill3;
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
