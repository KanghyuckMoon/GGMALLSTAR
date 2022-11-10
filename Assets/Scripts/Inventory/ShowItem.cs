using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sound;

namespace Inventory
{
	public class ShowItem : MonoBehaviour
	{
		public Transform pedestal;
		public InventorySO inventorySO;
		public TextMeshProUGUI nameText;
		public TextMeshProUGUI captionText;
		public int testShowItemIndex;
		public Transform contents;
		public GameObject itemNamePanel;
		public float namePanelHeightSize;
		public float viewPortHeightSize;
		public string effName;

		private int testPreviousItemIndex;
		private List<GameObject> namePanelObjectList = new List<GameObject>();
		private GameObject itemObj = null;
		private float posY;

		#region TestCode

		public void Start()
		{
			InitItemList();
			SetItem(inventorySO.itemDatas[testShowItemIndex]);
		}

		public void Update()
		{
			InputToShowItem();
		}

		private void InitItemList()
		{
			foreach (var itemData in inventorySO.itemDatas)
			{
				GameObject namePanel = Instantiate(itemNamePanel, contents);
				namePanel.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
				namePanelObjectList.Add(namePanel);
			}
		}

		private void InputToShowItem()
		{
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				testShowItemIndex = (testShowItemIndex + 1) % inventorySO.itemDatas.Count;
				SetItem(inventorySO.itemDatas[testShowItemIndex]);
			}
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				testShowItemIndex--;
				if (testShowItemIndex < 0)
				{
					testShowItemIndex = inventorySO.itemDatas.Count - 1;
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
			if (itemObj != null)
			{
				Destroy(itemObj);
			}
			itemObj = Instantiate(itemDataSO.prefeb, pedestal);
			nameText.text = itemDataSO.itemName;
			captionText.text = itemDataSO.explanation;

			//테스트코드
			{
				namePanelObjectList[testShowItemIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
				namePanelObjectList[testPreviousItemIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
				SoundManager.Instance.PlayEFF(effName);

				var contentRect = contents.GetComponent<RectTransform>();
				float addHeight = (testShowItemIndex - testPreviousItemIndex) * namePanelHeightSize;

				if(Mathf.Abs(testShowItemIndex - testPreviousItemIndex) > 1)
				{
					if (testShowItemIndex > testPreviousItemIndex)
					{
						posY = namePanelObjectList.Count * 100 - viewPortHeightSize;
					}
					else
					{
						posY = 0;
					}
					var vec2 = contentRect.anchoredPosition;
					vec2.y = posY;
					contentRect.anchoredPosition = vec2;
				}
				if(posY + addHeight >= 0 && posY + addHeight <= contentRect.sizeDelta.y - viewPortHeightSize)
				{
					posY += addHeight;
					var vec2 = contentRect.anchoredPosition;
					vec2.y = posY;
					contentRect.anchoredPosition = vec2;
				}

				testPreviousItemIndex = testShowItemIndex;
			}
		}
	}
}
