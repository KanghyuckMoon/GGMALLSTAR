using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : MonoBehaviour
{
	private int choiceP1;
	private int choiceP2;

	private int choiceStage;

	[SerializeField] private GameObject stageSelectUI;
	[SerializeField] private GameObject characterSelectUI;
	private bool isChoiceP2 = false;


	public void ChoiceCharacter(int character)
	{
		if (isChoiceP2)
		{
			ChoiceP2(character);
			stageSelectUI.SetActive(true);
			characterSelectUI.SetActive(false);
		}
		else
		{
			isChoiceP2 = true;
			ChoiceP1(character);
		}
	}

	public void ChoiceP1(int p1)
	{
		choiceP1 = p1;
	}
	public void ChoiceP2(int p2)
	{
		choiceP2 = p2;
	}

	public void ChoiceStage(int stage)
	{
		choiceStage = stage;
	}
}
