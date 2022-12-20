using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using Effect;
using DG.Tweening;

namespace UI.InGame
{
	public class LevelHUD : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("levelTextP1")] 
		private TextMeshProUGUI _levelTextP1;
		[SerializeField, FormerlySerializedAs("levelTextP2")] 
		private TextMeshProUGUI _levelTextP2;


		[SerializeField, FormerlySerializedAs("expImageP1")] 
		private Image _expImageP1;
		[SerializeField, FormerlySerializedAs("expImageP2")] 
		private Image _expImageP2;

		private CharacterLevel _characterLevelP1;
		private CharacterLevel _characterLevelP2;

		public RectTransform LevelTextP1
		{
			get
			{
				return _levelTextP1.rectTransform;
			}
		}
		public RectTransform LevelTextP2
		{
			get
			{
				return _levelTextP2.rectTransform;
			}
		}

		private void Start()
		{
			CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
			var characterP1 = characterSpawner.Player1.GetComponent<Character>();
			var characterP2 = characterSpawner.Player2.GetComponent<Character>();

			_characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
			_characterLevelP2 = characterP2.GetCharacterComponent<CharacterLevel>(ComponentType.Level);

			_characterLevelP1.AddChangeExpEvent(ChangeExpHUDP1);
			_characterLevelP1.AddChangeLevelEvent(ChangeLevelHUDP1);

			_characterLevelP2.AddChangeExpEvent(ChangeExpHUDP2);
			_characterLevelP2.AddChangeLevelEvent(ChangeLevelHUDP2);

			ChangeLevelHUDP1();
			ChangeLevelHUDP2();
			ChangeExpHUDP1();
			ChangeExpHUDP2();
		}

		private void ChangeLevelHUDP1()
		{
			if (_characterLevelP1.Level == 4)
			{
				_levelTextP1.text = "ALL STAR";
			}
			else
			{
				_levelTextP1.text = $"{_characterLevelP1.Level}STAR";
			}
		}
		private void ChangeExpHUDP1()
		{
			if (_characterLevelP1.Level == 4)
			{
				_expImageP1.fillAmount = 1f;
			}
			else
			{
				_expImageP1.fillAmount = ((float)_characterLevelP1.Exp - _characterLevelP1.PreviouseExp) / _characterLevelP1.NeedExp;
			}
		}

		private void ChangeLevelHUDP2()
		{
			if (_characterLevelP2.Level == 4)
			{
				_levelTextP2.text = "ALL STAR";
			}
			else
			{
				_levelTextP2.text = $"{_characterLevelP2.Level}STAR";
			}
		}
		private void ChangeExpHUDP2()
		{
			if (_characterLevelP2.Level < 4)
			{
				_expImageP2.fillAmount = ((float)_characterLevelP2.Exp - _characterLevelP2.PreviouseExp) / _characterLevelP2.NeedExp;
			}
			else
			{
				_expImageP2.fillAmount = 1f;
			}
		}
	}

}