using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Addressable;
using Utill;
using TMPro;
public class BattleLoadingImage : MonoBehaviour
{
	[SerializeField] private Image characterImageP1;
	[SerializeField] private Image characterImageP2;
	[SerializeField] private Image stageImage;
	[SerializeField] private TextMeshProUGUI player1Text;
	[SerializeField] private TextMeshProUGUI player2Text;

	private void Start()
	{
		player1Text.text = SelectDataSO.characterSelectP1.ToString();
		player2Text.text = SelectDataSO.characterSelectP2.ToString();
		characterImageP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");
		characterImageP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_CImage");
		stageImage.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.stageSelect.ToString()}_SImage");
	}

}
