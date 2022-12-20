using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Addressable;
using Utill;
using TMPro;

namespace Loading
{
	/// <summary>
	/// ��Ʋ �ε������� �̹������� ������ ĳ����, �������� �̹����� ��ȯ
	/// </summary>
	public class BattleLoadingImage : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("characterImageP1")]
		private Image _characterImageP1;
		[SerializeField, FormerlySerializedAs("characterImageP2")]
		private Image _characterImageP2;
		[SerializeField, FormerlySerializedAs("stageImage")]
		private Image _stageImage;
		[SerializeField, FormerlySerializedAs("player1Text")]
		private TextMeshProUGUI _player1Text;
		[SerializeField, FormerlySerializedAs("player2Text")]
		private TextMeshProUGUI _player2Text;

		private void Start()
		{
			_player1Text.text = SelectDataSO.characterSelectP1.ToString();
			_player2Text.text = SelectDataSO.characterSelectP2.ToString();
			_characterImageP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");
			_characterImageP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_CImage");
			_stageImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.stageSelect.ToString()}_SImage");
		}

	}

}
