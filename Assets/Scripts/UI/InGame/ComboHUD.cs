using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace UI.InGame
{
	public class ComboHUD : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("comboTextP1")] 
		private TextMeshProUGUI _comboTextP1;
		[SerializeField, FormerlySerializedAs("comboTextP2")] 
		private TextMeshProUGUI _comboTextP2;

		private Vector2 _originPosP1;
		private Vector2 _originPosP2;

		private CharacterDamage _characterDamageP1;
		private CharacterDamage _characterDamageP2;

		private void Start()
		{
			CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
			var characterP1 = characterSpawner.Player1.GetComponent<Character>();
			var characterP2 = characterSpawner.Player2.GetComponent<Character>();

			_characterDamageP1 = characterP1.GetCharacterComponent<CharacterDamage>(ComponentType.Damage);
			_characterDamageP2 = characterP2.GetCharacterComponent<CharacterDamage>(ComponentType.Damage);

			_originPosP1 = _comboTextP1.rectTransform.anchoredPosition;
			_originPosP2 = _comboTextP2.rectTransform.anchoredPosition;

			_characterDamageP1.DamagedAction += SetComboP2;
			_characterDamageP2.DamagedAction += SetComboP1;
		}
		private void SetComboP1()
		{
			_comboTextP1.rectTransform.DOKill();
			_comboTextP1.gameObject.SetActive(true);
			_comboTextP1.text = $"<size=100%>{_characterDamageP2.HitCount}<size=60%>Hits";
			_comboTextP1.rectTransform.anchoredPosition = _originPosP1;

			float scale = Mathf.Log((float)_characterDamageP2.HitCount / 2) + 1;
			_comboTextP1.rectTransform.localScale = Vector3.one * scale;

			float doScaleTime = _characterDamageP2.SturnTime - 0.3f > 0f ? _characterDamageP2.SturnTime - 0.3f : 0.1f;
			_comboTextP1.rectTransform.DOScale(Vector3.one * 0.5f, doScaleTime).SetDelay(0.3f);
			_comboTextP1.rectTransform.DOShakePosition(_characterDamageP2.SturnTime, scale / 100).OnComplete(() =>
			{
				_comboTextP1.gameObject.SetActive(false);
			});
		}
		private void SetComboP2()
		{
			_comboTextP2.rectTransform.DOKill();
			_comboTextP2.gameObject.SetActive(true);
			_comboTextP2.text = $"<size=100%>{_characterDamageP1.HitCount}<size=60%>Hits";
			_comboTextP2.rectTransform.anchoredPosition = _originPosP2;

			float scale = Mathf.Log((float)_characterDamageP1.HitCount / 2) + 1;
			_comboTextP2.rectTransform.localScale = Vector3.one * scale;

			float doScaleTime = _characterDamageP1.SturnTime - 0.3f > 0f ? _characterDamageP1.SturnTime - 0.3f : 0.1f;
			_comboTextP2.rectTransform.DOScale(Vector3.one * 0.5f, doScaleTime).SetDelay(0.3f);
			_comboTextP2.rectTransform.DOShakePosition(_characterDamageP1.SturnTime, scale / 100).OnComplete(() =>
			{
				_comboTextP2.gameObject.SetActive(false);
			});
		}

	}

}