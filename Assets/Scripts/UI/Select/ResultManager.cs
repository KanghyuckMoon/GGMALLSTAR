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
			textMeshProUGUIs[0].text = $"이긴 라운드 수 : {characterDebugData.winRoundCount}";
			textMeshProUGUIs[1].text = $"진 라운드 수 : {characterDebugData.loseRoundCount}";
			textMeshProUGUIs[2].text = $"입은 데미지 : {characterDebugData.damaged}";
			textMeshProUGUIs[3].text = $"입힌 데미지 : {characterDebugData.damage}";
			textMeshProUGUIs[4].text = $"지상에 있었던 시간 : {characterDebugData.groundTime}";
			textMeshProUGUIs[5].text = $"공중에 있었던 시간 : {characterDebugData.airTime}";
			textMeshProUGUIs[6].text = $"점프를 한 횟수 : {characterDebugData.jumpCount}";
			textMeshProUGUIs[7].text = $"지상에서 공격한 횟수 : {characterDebugData.groundAttackCount}";
			textMeshProUGUIs[8].text = $"공중에서 공격한 횟수 : {characterDebugData.airAttackCount}";
			textMeshProUGUIs[9].text = $"스킬 1을 사용한 횟수 : {characterDebugData.skill1Count}";
			textMeshProUGUIs[10].text = $"스킬 2를 사용한 횟수 : {characterDebugData.skill2Count}";
			textMeshProUGUIs[11].text = $"올스타 스킬을 사용한 라운드 : {characterDebugData.allStarSkill}";
			textMeshProUGUIs[12].text = $"모은 스타의 갯수 : {characterDebugData.starAmount}";
			textMeshProUGUIs[13].text = $"총 게임 시간 : {characterDebugData.gameTime}";
		}
	}

}