using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Loading
{
	public class TipText : MonoBehaviour
	{
		public TipSO tipSO;
		private int tipTextIndex;
		private TextMeshProUGUI tipText;

		public void Start()
		{
			tipText = GetComponent<TextMeshProUGUI>();
			tipTextIndex = Random.Range(0, tipSO.tiptexts.Length - 1);
			StartCoroutine(TipTextChange());
		}

		private IEnumerator TipTextChange()
		{
			while(true)
			{
				string text = tipSO.tiptexts[tipTextIndex];
				tipText.text = tipSO.tiptexts[tipTextIndex];

				yield return new WaitForSeconds(text.Length);
				
				tipTextIndex = (tipTextIndex + 1) % tipSO.tiptexts.Length;
			}
		}

	}
}
