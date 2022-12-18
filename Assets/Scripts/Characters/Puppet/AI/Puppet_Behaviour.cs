using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;


public class Puppet_Behaviour : BehaviourTree
{
	private CharacterSkill_Puppet characterSkill_Puppet;
	private CharacterSkill_Puppet CharacterSkill_Puppet
	{
		get
		{
			characterSkill_Puppet ??= mainCharacter.GetCharacterComponent<CharacterSkill_Puppet>(ComponentType.Skill1);
			return characterSkill_Puppet;
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
				//IfAction(Skill1Condition, UseSkill1),
				//IfAction(Skill2Condition, UseSkill2),
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
		//NodeSetting
		return Selector
			(
				//IfAction(Skill1Condition, UseSkill1),
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
	private INode Level3()
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

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove)
					//IfAction(MoveCondition, Jump)
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

				//이동 시퀀스
				RandomChoice
				(
					IfAction(MoveCondition, CloseMove)
					//IfAction(MoveCondition, Jump)
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

	private float skill1time = 0f;
	protected bool Skill1Condition()
	{
		bool timeCondition = false;

		if(skill1time < 10f)
		{
			skill1time += Time.deltaTime;
		}
		else
		{
			skill1time = 0f;
			timeCondition = true;
		}


		return timeCondition && CharacterSkill_Puppet.IsCanUseSkill1;
	}
	protected bool Skill2Condition()
	{
		float randomDistance = 0.6f; //Random.Range(0.1f, 1f);
		bool distanceCondition = false;


		//거리
		if (Mathf.Abs(opCharacter.transform.position.x - mainCharacter.transform.position.x) < randomDistance)
		{
			distanceCondition = true;
		}
		else
		{
			distanceCondition = false;
		}

		return distanceCondition && CharacterSkill_Puppet.IsCanUseSkill2;
	}
	protected bool AllStarSkillCondition()
	{
		return CharacterSkill_Puppet.IsCanUseSkill3;
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
