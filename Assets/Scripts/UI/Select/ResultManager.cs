using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Addressable;
using Utill;
using TMPro;


namespace UI
{
	public class ResultManager : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("winCharacterImage")]
		private Image _winCharacterImage;
		[SerializeField, FormerlySerializedAs("winPlayerText")]
		private TextMeshProUGUI _winPlayerText;
		[SerializeField, FormerlySerializedAs("debugTextsP1")]
		private TextMeshProUGUI[] _debugTextsP1;
		[SerializeField, FormerlySerializedAs("debugTextsP2")]
		private TextMeshProUGUI[] _debugTextsP2;

		private void Start()
		{
			if (RoundManager.characterDebugDataP1.winRoundCount == 3)
			{
				_winPlayerText.text = SelectDataSO.characterSelectP1.ToString();
				_winCharacterImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");
				PlayWinBGM(SelectDataSO.characterSelectP1);
			}
			else
			{
				_winPlayerText.text = SelectDataSO.characterSelectP2.ToString();
				_winCharacterImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_CImage");
				PlayWinBGM(SelectDataSO.characterSelectP2);
			}

			//stageImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.stageSelect.ToString()}_SImage");

			SetField(_debugTextsP1, RoundManager.characterDebugDataP1);
			SetField(_debugTextsP2, RoundManager.characterDebugDataP2);
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
				case CharacterSelect.Puppet:
					Sound.SoundManager.Instance.PlayBGM(Sound.AudioBGMType.Puppet_Win);
					break;
				case CharacterSelect.Agent:
					Sound.SoundManager.Instance.PlayBGM(Sound.AudioBGMType.Agent_Win);
					break;
				case CharacterSelect.Damvi:
					Sound.SoundManager.Instance.PlayBGM(Sound.AudioBGMType.Damvi_Win);
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

}