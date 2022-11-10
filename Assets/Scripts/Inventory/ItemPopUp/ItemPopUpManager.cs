using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory
{
	public class ItemPopUpManager : MonoBehaviour
	{
		public GameObject popUpImage;
		public TextMeshProUGUI nameText;

		private bool isShow = false;
		private Queue<string> getItemQueue = new Queue<string>();

		public void SetItemPopUp(string itemName)
		{
			getItemQueue.Enqueue(itemName);
			if (!isShow)
			{
				ShowGetItem();
			}
		}

		private void ShowGetItem()
		{
			if (getItemQueue.Count > 0)
			{
				nameText.text = $"æ∆¿Ã≈€ »πµÊ : {getItemQueue.Dequeue()}";
				popUpImage.gameObject.SetActive(true);

				isShow = true;
				Invoke("ShowGetItem", 1f);
			}
			else
			{
				isShow = false;
				popUpImage.gameObject.SetActive(false);
			}
		}

	}
}
