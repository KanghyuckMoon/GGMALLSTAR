using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

namespace Inventory
{
	public class ItemBox : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("model")] 
		private GameObject _model;
		private bool _isGet = false;

		private void OnEnable()
		{
			_model.transform.localScale = Vector3.zero;
			_model.transform.DORotate(new Vector3(0,360,0), 0.3f, RotateMode.FastBeyond360).SetLoops(3, LoopType.Incremental);
			_model.transform.DOScale(Vector3.one, 1f).OnComplete(() => _model.transform.DOShakePosition(999f, 0.01f, 1).SetEase(Ease.InSine));
		}

		private void OnTriggerEnter(Collider other)
		{
			if(_isGet)
			{
				return;
			}

			_isGet = true;
			_model.transform.DOKill();
			_model.transform.DOScale(Vector3.zero, 1f).OnComplete(() => gameObject.SetActive(false)).SetEase(Ease.InElastic);
			Sound.SoundManager.Instance.PlayEFF("se_item_genesis_get");
			InventoryManager.Instance.RandomGetItem();
		}
	}

}