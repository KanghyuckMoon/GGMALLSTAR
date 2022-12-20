using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

namespace Inventory
{
	public class ItemPopUpManager : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("popUpImage")]
		private GameObject _popUpImage;
		[SerializeField, FormerlySerializedAs("nameText")]
		private TextMeshProUGUI _nameText;

		private bool _isShow = false;
		private Queue<string> _getItemQueue = new Queue<string>();

		public void SetItemPopUp(string itemName)
		{
			_getItemQueue.Enqueue(itemName);
			if (!_isShow)
			{
				ShowGetItem();
			}
		}

		private void ShowGetItem()
		{
			if (_getItemQueue.Count > 0)
			{
				_nameText.text = $"æ∆¿Ã≈€ »πµÊ : {_getItemQueue.Dequeue()}";
				_popUpImage.gameObject.SetActive(true);

				_isShow = true;
				Invoke("ShowGetItem", 1f);
			}
			else
			{
				_isShow = false;
				_popUpImage.gameObject.SetActive(false);
			}
		}

	}
}
