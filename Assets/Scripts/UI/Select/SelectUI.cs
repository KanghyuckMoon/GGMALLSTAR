using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Addressable;
using TMPro;

public class SelectUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI aiTextP1;
	[SerializeField] private TextMeshProUGUI aiTextP2;

	[SerializeField] private Image characterP1;
	[SerializeField] private Image characterP2;
	[SerializeField] private GameObject stageSelectUI;
	[SerializeField] private GameObject characterSelectUI;
	private bool isChoiceP2 = false;

	private void Start()
	{
		AIChangButtonSettingP1();
		AIChangButtonSettingP2();
	}

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

		SelectDataSO.isArcade = false;
	}

	public void ArcadeChoiceCharacter(int character)
	{
		ChoiceP1(character);

		SelectDataSO.winCount = 0;
		SelectDataSO.isArcade = true;
		SelectDataSO.isAICharacterP2 = true;
	}

	private void ArcadeCharacterChoice(int p1)
	{
		SelectDataSO.characterSelectP1 = (CharacterSelect)p1;
		characterP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{((CharacterSelect)p1).ToString()}_CImage");

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

	public void ChangeAIP1()
	{
		SelectDataSO.isAICharacterP1 = !SelectDataSO.isAICharacterP1;
		AIChangButtonSettingP1();
	}
	public void ChangeAIP2()
	{
		SelectDataSO.isAICharacterP2 = !SelectDataSO.isAICharacterP2;
		AIChangButtonSettingP2();
	}

	public void AIChangButtonSettingP1()
	{
		if(SelectDataSO.isAICharacterP1)
		{
			aiTextP1.text = "AI ON";
		}
		else
		{
			aiTextP1.text = "AI OFF";
		}
	}
	public void AIChangButtonSettingP2()
	{
		if (SelectDataSO.isAICharacterP2)
		{
			aiTextP2.text = "AI ON";
		}
		else
		{
			aiTextP2.text = "AI OFF";
		}
	}

	public void ChoiceStage(int stage)
	{
		SelectDataSO.stageSelect = (StageSelect)stage;
	}
}
