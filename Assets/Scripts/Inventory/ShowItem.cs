using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Inventory
{
	public class ShowItem : MonoBehaviour
	{
		public Transform pedestal;
		public InventorySO inventorySO;
		public TextMeshProUGUI captionText;
		public int testShowItemIndex;

		private GameObject itemObj = null;

		#region TestCode

		public void Start()
		{
			SetItem(inventorySO.itemDatas[testShowItemIndex]);
		}

		public void Update()
		{
			InputToShowItem();
		}
		private void InputToShowItem()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				testShowItemIndex = (testShowItemIndex + 1) % inventorySO.itemDatas.Length;
				SetItem(inventorySO.itemDatas[testShowItemIndex]);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				testShowItemIndex--;
				if (testShowItemIndex < 0)
				{
					testShowItemIndex = inventorySO.itemDatas.Length - 1;
				}
				SetItem(inventorySO.itemDatas[testShowItemIndex]);
			}
		}

		#endregion

		/// <summary>
		/// Instantiate Item Prefeb and delete previous Item prefeb
		/// </summary>
		/// <param name="itemDataSO"></param>
		public void SetItem(ItemDataSO itemDataSO)
		{
			if(itemObj != null)
			{
				Destroy(itemObj);
			}
			itemObj = Instantiate(itemDataSO.prefeb, pedestal);
			captionText.text = itemDataSO.explanation;
		}
	}
}
