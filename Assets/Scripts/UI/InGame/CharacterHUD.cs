using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using Addressable;

namespace UI.InGame
{
	public class CharacterHUD : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("characterImageP1")] 
		private Image _characterImageP1;
		[SerializeField, FormerlySerializedAs("characterImageP2")] 
		private Image _characterImageP2;
		[SerializeField, FormerlySerializedAs("characterTextP1")] 
		private TextMeshProUGUI _characterTextP1;
		[SerializeField, FormerlySerializedAs("characterTextP2")] 
		private TextMeshProUGUI _characterTextP2;

		private void Start()
		{
			_characterImageP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");
			_characterImageP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_CImage");

			_characterTextP1.text = SelectDataSO.characterSelectP1.ToString();
			_characterTextP2.text = SelectDataSO.characterSelectP2.ToString();
		}
	}

}