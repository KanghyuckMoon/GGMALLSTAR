using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

namespace Loading
{
	/// <summary>
	/// ∆¡ ≈ÿΩ∫∆Æ
	/// </summary>
	public class TipText : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("tipSO")]
		public TipSO _tipSO;
		private int _tipTextIndex;
		private TextMeshProUGUI _tipText;

		public void Start()
		{
			_tipText = GetComponent<TextMeshProUGUI>();
			_tipTextIndex = Random.Range(0, _tipSO.tiptexts.Length - 1);
			StartCoroutine(TipTextChange());
		}

		private IEnumerator TipTextChange()
		{
			while(true)
			{
				string text = _tipSO.tiptexts[_tipTextIndex];
				_tipText.text = $"TIP : {_tipSO.tiptexts[_tipTextIndex]}";

				yield return new WaitForSeconds(text.Length);
				
				_tipTextIndex = (_tipTextIndex + 1) % _tipSO.tiptexts.Length;
			}
		}

	}
}
