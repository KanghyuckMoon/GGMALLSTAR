using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Addressable;

public class CharacterHUD : MonoBehaviour
{
	[SerializeField] private Image characterImageP1;
	[SerializeField] private Image characterImageP2;
	[SerializeField] private TextMeshProUGUI characterTextP1;
	[SerializeField] private TextMeshProUGUI characterTextP2;

	private void Start()
	{
		characterImageP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");
		characterImageP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_CImage");

		characterTextP1.text = SelectDataSO.characterSelectP1.ToString();
		characterTextP2.text = SelectDataSO.characterSelectP2.ToString();
	}
}
