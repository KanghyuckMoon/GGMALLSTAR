using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Addressable;

public class SelectUI : MonoBehaviour
{
	private int choiceStage;

	[SerializeField] private Image characterP1;
	[SerializeField] private Image characterP2;
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
		SelectDataSO.characterSelectP1 = (CharacterSelect)p1;
		characterP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{((CharacterSelect)p1).ToString()}_CImage");
	}
	public void ChoiceP2(int p2)
	{
		SelectDataSO.characterSelectP2 = (CharacterSelect)p2;
		characterP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{((CharacterSelect)p2).ToString()}_CImage");
	}

	public void ChoiceStage(int stage)
	{
		SelectDataSO.stageSelect = (StageSelect)stage;
	}
}
