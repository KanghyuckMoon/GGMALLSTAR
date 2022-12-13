using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ComboHUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI comboTextP1;
	[SerializeField] private TextMeshProUGUI comboTextP2;

	private Vector2 originPosP1;
	private Vector2 originPosP2;

	private CharacterDamage characterDamageP1;
	private CharacterDamage characterDamageP2;

	private void Start()
	{
		CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
		var characterP1 = characterSpawner.Player1.GetComponent<Character>();
		var characterP2 = characterSpawner.Player2.GetComponent<Character>();

		characterDamageP1 = characterP1.GetCharacterComponent<CharacterDamage>();
		characterDamageP2 = characterP2.GetCharacterComponent<CharacterDamage>();

		originPosP1 = comboTextP1.rectTransform.anchoredPosition;
		originPosP2 = comboTextP2.rectTransform.anchoredPosition;

		characterDamageP1.DamagedAction += SetComboP2;
		characterDamageP2.DamagedAction += SetComboP1;
	}

	private void SetComboP1()
	{
		comboTextP1.rectTransform.DOKill();
		comboTextP1.gameObject.SetActive(true);
		comboTextP1.text = $"<size=100%>{characterDamageP2.HitCount}<size=60%>Hits";
		comboTextP1.rectTransform.anchoredPosition = originPosP1;

		float scale = Mathf.Log((float)characterDamageP2.HitCount / 2) + 1;
		comboTextP1.rectTransform.localScale = Vector3.one * scale;

		float doScaleTime = characterDamageP2.SturnTime - 0.3f > 0f ? characterDamageP2.SturnTime - 0.3f : 0.1f;
		comboTextP1.rectTransform.DOScale(Vector3.one * 0.5f, doScaleTime).SetDelay(0.3f);
		comboTextP1.rectTransform.DOShakePosition(characterDamageP2.SturnTime, scale / 100).OnComplete(() =>
		{
			comboTextP1.gameObject.SetActive(false);
		});
	}
	private void SetComboP2()
	{
		comboTextP2.rectTransform.DOKill();
		comboTextP2.gameObject.SetActive(true);
		comboTextP2.text = $"<size=100%>{characterDamageP1.HitCount}<size=60%>Hits";
		comboTextP2.rectTransform.anchoredPosition = originPosP2;

		float scale = Mathf.Log((float)characterDamageP1.HitCount / 2) + 1;
		comboTextP2.rectTransform.localScale = Vector3.one * scale;

		float doScaleTime = characterDamageP1.SturnTime - 0.3f > 0f ? characterDamageP1.SturnTime - 0.3f : 0.1f;
		comboTextP2.rectTransform.DOScale(Vector3.one * 0.5f, doScaleTime).SetDelay(0.3f);
		comboTextP2.rectTransform.DOShakePosition(characterDamageP1.SturnTime, scale / 100).OnComplete(() =>
		{
			comboTextP2.gameObject.SetActive(false);
		});
	}

}
