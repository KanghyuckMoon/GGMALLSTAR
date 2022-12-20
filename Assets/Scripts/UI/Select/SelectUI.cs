using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Addressable;
using TMPro;

namespace UI
{

	public class SelectUI : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("aiTextP1")]
		private TextMeshProUGUI _aiTextP1;
		[SerializeField, FormerlySerializedAs("aiTextP2")]
		private TextMeshProUGUI _aiTextP2;
		[SerializeField, FormerlySerializedAs("aiLevelTextP1")]
		private TextMeshProUGUI _aiLevelTextP1;
		[SerializeField, FormerlySerializedAs("aiLevelTextP2")]
		private TextMeshProUGUI _aiLevelTextP2;


		[SerializeField, FormerlySerializedAs("characterP1")]
		private Image _characterP1;
		[SerializeField, FormerlySerializedAs("characterP2")]
		private Image _characterP2;
		[SerializeField, FormerlySerializedAs("stageSelectUI")]
		private GameObject _stageSelectUI;
		[SerializeField, FormerlySerializedAs("characterSelectUI")]
		private GameObject _characterSelectUI;


		[SerializeField, FormerlySerializedAs("aiLevelP1")]
		private GameObject _aiLevelP1;
		[SerializeField, FormerlySerializedAs("aiLevelP2")]
		private GameObject _aiLevelP2;

		private bool isChoiceP2 = false;

		private void Start()
		{
			AIChangButtonSettingP1();
			AIChangButtonSettingP2();
		}

		/// <summary>
		/// 캐릭터 선택 선택 순서에 따라 P1, P2를 선택함
		/// </summary>
		/// <param name="character"></param>
		public void ChoiceCharacter(int character)
		{
			if (isChoiceP2)
			{
				ChoiceP2(character);

				//선택이 끝나면 스테이지 선택창 활성화
				_stageSelectUI.SetActive(true);
				_characterSelectUI.SetActive(false);
			}
			else
			{
				isChoiceP2 = true;
				ChoiceP1(character);
			}

			SelectDataSO.isArcade = false;
			SelectDataSO.isTutorial = false;
		}

		/// <summary>
		/// 아케이드 캐릭터 선택
		/// </summary>
		/// <param name="character"></param>
		public void ArcadeChoiceCharacter(int character)
		{
			ChoiceP1(character);

			SelectDataSO.winCount = 0;
			SelectDataSO.isTutorial = false;
			SelectDataSO.isArcade = true;
			SelectDataSO.isAICharacterP2 = true;
		}

		/// <summary>
		/// 캐릭터 P1 선택
		/// </summary>
		public void ChoiceP1(int p1)
		{
			SelectDataSO.characterSelectP1 = (CharacterSelect)p1;
			_characterP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{((CharacterSelect)p1).ToString()}_CImage");
		}
		/// <summary>
		/// 캐릭터 P2 선택
		/// </summary>
		/// <param name="p2"></param>
		public void ChoiceP2(int p2)
		{
			SelectDataSO.characterSelectP2 = (CharacterSelect)p2;
			_characterP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{((CharacterSelect)p2).ToString()}_CImage");
		}

		/// <summary>
		/// AI P1 On/Off
		/// </summary>
		public void ChangeAIP1()
		{
			SelectDataSO.isAICharacterP1 = !SelectDataSO.isAICharacterP1;
			AIChangButtonSettingP1();
		}
		/// <summary>
		/// AI P2 On/Off
		/// </summary>
		public void ChangeAIP2()
		{
			SelectDataSO.isAICharacterP2 = !SelectDataSO.isAICharacterP2;
			AIChangButtonSettingP2();
		}

		/// <summary>
		/// AI P1 On/Off에 따른 설정
		/// </summary>
		public void AIChangButtonSettingP1()
		{
			if (SelectDataSO.isAICharacterP1)
			{
				_aiTextP1.text = "AI ON";
				_aiLevelTextP1?.gameObject.SetActive(true);
				_aiLevelP1?.gameObject.SetActive(true);
			}
			else
			{
				_aiTextP1.text = "AI OFF";
				_aiLevelTextP1?.gameObject.SetActive(false);
				_aiLevelP1?.gameObject.SetActive(false);
			}
		}
		/// <summary>
		/// AI P2 On/Off에 따른 설정
		/// </summary>
		public void AIChangButtonSettingP2()
		{
			if (SelectDataSO.isAICharacterP2)
			{
				_aiTextP2.text = "AI ON";
				_aiLevelTextP2?.gameObject.SetActive(true);
				_aiLevelP2?.gameObject.SetActive(true);
			}
			else
			{
				_aiTextP2.text = "AI OFF";
				_aiLevelTextP2?.gameObject.SetActive(false);
				_aiLevelP2?.gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// AI P1 레벨 선택
		/// </summary>
		/// <param name="level"></param>
		public void AILevelChangeP1(float level)
		{
			SelectDataSO.aiLevelP1 = (int)level;
			_aiLevelTextP1.text = $"AI Level : {(int)level}";
		}
		/// <summary>
		/// AI P2 레벨 선택
		/// </summary>
		/// <param name="level"></param>
		public void AILevelChangeP2(float level)
		{
			SelectDataSO.aiLevelP2 = (int)level;
			_aiLevelTextP2.text = $"AI Level : {(int)level}";
		}

		/// <summary>
		/// 스테이지 선택
		/// </summary>
		/// <param name="stage"></param>
		public void ChoiceStage(int stage)
		{
			SelectDataSO.stageSelect = (StageSelect)stage;
		}
	}

}