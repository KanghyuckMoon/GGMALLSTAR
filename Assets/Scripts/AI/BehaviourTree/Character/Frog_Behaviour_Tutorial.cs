using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Addressable;
using static NodeUtill;

public class Frog_Behaviour_Tutorial : BehaviourTree
{
	private TutorialManager tutorialManager;
	public TutorialManager TutorialManager
	{
		get
		{
			tutorialManager = GameObject.FindObjectOfType<TutorialManager>();
			return tutorialManager;
		}
	}
	private CharacterSkill_Frog characterSkill_Frog;
	private CharacterSkill_Frog CharacterSkill_Frog
	{
		get
		{
			characterSkill_Frog ??= mainCharacter.GetCharacterComponent<CharacterSkill_Frog>();
			return characterSkill_Frog;
		}
	}
	public override void Init(Character opCh, Character mainCh, CharacterAIInput aiTestInput, int level)
	{
		base.Init(opCh, mainCh, aiTestInput, level);
	}
	public override void SetNode(int level)
	{
		_rootNode = Selector
		(
			IfAction(Skill2Condition, UseSkill2)
		);
	}
	protected bool AttackJCondition()
	{
		//float randomDistance = Random.Range(0.1f, 1f);
		float attackDistance = 0.5f;
		bool distanceCondition = false;
		bool directionCondition = false;


		//�Ÿ�
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

		//����

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

	protected bool DodgeCondition()
	{
		int random = Random.Range(0, 15);

		return random == 0 && MoveCondition();
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

	protected void Dodge()
	{
		TapKey(KeyCode.K);
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

	protected bool FoolActionConditionLevel1()
	{
		int random = Random.Range(0, 5);

		return random == 0;
	}
	protected bool FoolActionConditionLevel2()
	{
		int random = Random.Range(0, 10);

		return random == 0;
	}
	protected bool FoolActionConditionLevel3()
	{
		int random = Random.Range(0, 30);

		return random == 0;
	}

	protected void FoolAction()
	{
		AttackJ();
	}

	protected bool Skill1Condition()
	{
		return CharacterSkill_Frog.IsCanUseSkill1;
	}
	protected bool Skill2Condition()
	{
		return TutorialManager.TutorialStrp == 4 && CharacterSkill_Frog.IsCanUseSkill2;
	}
	protected bool AllStarSkillCondition()
	{
		return CharacterSkill_Frog.IsCanUseSkill3;
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
