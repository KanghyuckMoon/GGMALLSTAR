using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Addressable;
using Utill;
using TMPro;

public class ResultManager : MonoBehaviour
{
	[SerializeField] private Image winCharacterImage;
	[SerializeField] private Image stageImage;
	[SerializeField] private TextMeshProUGUI winPlayerText;
	[SerializeField] private TextMeshProUGUI[] debugTextsP1;
	[SerializeField] private TextMeshProUGUI[] debugTextsP2;

	private void Start()
	{
		if( RoundManager.characterDebugDataP1.winRoundCount == 3)
		{
			winPlayerText.text = SelectDataSO.characterSelectP1.ToString();
			winCharacterImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");
			PlayWinBGM(SelectDataSO.characterSelectP1);
		}
		else
		{
			winPlayerText.text = SelectDataSO.characterSelectP2.ToString();
			winCharacterImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_CImage");
			PlayWinBGM(SelectDataSO.characterSelectP2);
		}

		stageImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.stageSelect.ToString()}_SImage");

		SetField(debugTextsP1, RoundManager.characterDebugDataP1);
		SetField(debugTextsP2, RoundManager.characterDebugDataP2);
	}

	private void PlayWinBGM(CharacterSelect characterSelect)
	{
		switch (characterSelect)
		{
			case CharacterSelect.Jaeby:
				Sound.SoundManager.Instance.PlayBGM(Sound.AudioBGMType.Jaeby_Win);
				break;
			case CharacterSelect.Frog:
				Sound.SoundManager.Instance.PlayBGM(Sound.AudioBGMType.Frog_Win);
				break;
			case CharacterSelect.Dice:
				Sound.SoundManager.Instance.PlayBGM(Sound.AudioBGMType.Dice_Win);
				break;
		}
	}

	private void SetField(TextMeshProUGUI[] textMeshProUGUIs, CharacterDebugData characterDebugData)
	{
		textMeshProUGUIs[0].text = $"�̱� ���� �� : {characterDebugData.winRoundCount}";
		textMeshProUGUIs[1].text = $"�� ���� �� : {characterDebugData.loseRoundCount}";
		textMeshProUGUIs[2].text = $"���� ������ : {characterDebugData.damaged}";
		textMeshProUGUIs[3].text = $"���� ������ : {characterDebugData.damage}";
		textMeshProUGUIs[4].text = $"���� �־��� �ð� : {characterDebugData.groundTime}";
		textMeshProUGUIs[5].text = $"���߿� �־��� �ð� : {characterDebugData.airTime}";
		textMeshProUGUIs[6].text = $"������ �� Ƚ�� : {characterDebugData.jumpCount}";
		textMeshProUGUIs[7].text = $"���󿡼� ������ Ƚ�� : {characterDebugData.groundAttackCount}";
		textMeshProUGUIs[8].text = $"���߿��� ������ Ƚ�� : {characterDebugData.airAttackCount}";
		textMeshProUGUIs[9].text = $"��ų 1�� ����� Ƚ�� : {characterDebugData.skill1Count}";
		textMeshProUGUIs[10].text = $"��ų 2�� ����� Ƚ�� : {characterDebugData.skill2Count}";
		textMeshProUGUIs[11].text = $"�ý�Ÿ ��ų�� ����� ���� : {characterDebugData.allStarSkill}";
		textMeshProUGUIs[12].text = $"���� ��Ÿ�� ���� : {characterDebugData.starAmount}";
		textMeshProUGUIs[13].text = $"�� ���� �ð� : {characterDebugData.gameTime}";
	}
}
