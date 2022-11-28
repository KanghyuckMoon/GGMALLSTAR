using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Pool;

public class ComboCountEffect : MonoBehaviour
{
	private TextMeshPro textMeshPro;

	public void SetComboCount(int count, float stunTime, Vector3 pos)
	{
		textMeshPro ??= GetComponent<TextMeshPro>();

		gameObject.SetActive(true);
		textMeshPro.text = $"{count}";
		pos.z += 1;
		transform.position = pos;

		float scale = Mathf.Log(count) + 5;
		transform.localScale = Vector3.one * scale;

		transform.DOScale(Vector3.zero, stunTime);
		transform.DOShakePosition(stunTime, scale / 100).OnComplete(() =>
		{
			gameObject.SetActive(false);
			PoolManager.AddObjToPool("ComboCountEff", gameObject);
		});
	}
}
