using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Inventory
{
	public class ItemBox : MonoBehaviour
	{
		[SerializeField] private GameObject model;

		private void OnEnable()
		{
			model.transform.localScale = Vector3.zero;
			model.transform.DORotate(new Vector3(0,360,0), 0.3f, RotateMode.FastBeyond360).SetLoops(3, LoopType.Incremental);
			model.transform.DOScale(Vector3.one, 1f).OnComplete(() => model.transform.DOShakePosition(999f, 0.01f, 1).SetEase(Ease.InSine));
		}

		private void OnTriggerEnter(Collider other)
		{
			model.transform.DOKill();
			model.transform.DOScale(Vector3.zero, 1f).OnComplete(() => gameObject.SetActive(false)).SetEase(Ease.InElastic);
			InventoryManager.Instance.RandomGetItem();
		}
	}

}