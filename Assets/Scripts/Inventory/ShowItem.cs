using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Sound;

namespace Inventory
{
	public class ShowItem : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("pedestal")]
		private Transform _pedestal;
		[SerializeField, FormerlySerializedAs("nameText")]
		private TextMeshProUGUI _nameText;
		[SerializeField, FormerlySerializedAs("captionText")]
		private TextMeshProUGUI _captionText;
		[SerializeField, FormerlySerializedAs("_showItemIndex")]
		private int _showItemIndex;
		[SerializeField, FormerlySerializedAs("contents")]
		private Transform _contents;
		[SerializeField, FormerlySerializedAs("itemNamePanel")]
		private GameObject _itemNamePanel;
		[SerializeField, FormerlySerializedAs("namePanelHeightSize")]
		private float _namePanelHeightSize;
		[SerializeField, FormerlySerializedAs("viewPortHeightSize")]
		private float _viewPortHeightSize;
		[SerializeField, FormerlySerializedAs("effName")]
		private string _effName;

		private int _testPreviousItemIndex;
		private List<GameObject> _namePanelObjectList = new List<GameObject>();
		private GameObject _itemObj = null;
		private float _posY;

		/// <summary>
		/// Instantiate Item Prefeb and delete previous Item prefeb
		/// </summary>
		/// <param name="itemDataSO"></param>
		public void SetItem(ItemDataSO itemDataSO)
		{
			if (_itemObj != null)
			{
				Destroy(_itemObj);
			}
			_itemObj = Instantiate(itemDataSO.prefeb, _pedestal);
			_nameText.text = itemDataSO.itemName;
			_captionText.text = itemDataSO.explanation;

			//테스트코드
			{
				_namePanelObjectList[_showItemIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
				_namePanelObjectList[_testPreviousItemIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
				SoundManager.Instance.PlayEFF(_effName);

				var contentRect = _contents.GetComponent<RectTransform>();
				float addHeight = (_showItemIndex - _testPreviousItemIndex) * _namePanelHeightSize;

				if (Mathf.Abs(_showItemIndex - _testPreviousItemIndex) > 1)
				{
					if (_showItemIndex > _testPreviousItemIndex)
					{
						_posY = _namePanelObjectList.Count * 100 - _viewPortHeightSize;
					}
					else
					{
						_posY = 0;
					}
					var vec2 = contentRect.anchoredPosition;
					vec2.y = _posY;
					contentRect.anchoredPosition = vec2;
				}
				if (_posY + addHeight >= 0 && _posY + addHeight <= contentRect.sizeDelta.y - _viewPortHeightSize)
				{
					_posY += addHeight;
					var vec2 = contentRect.anchoredPosition;
					vec2.y = _posY;
					contentRect.anchoredPosition = vec2;
				}

				_testPreviousItemIndex = _showItemIndex;
			}
		}
		private void Start()
		{
			InitItemList();
			if(InventoryStaticSO.itemDatas.Count > 0)
			{
				SetItem(InventoryStaticSO.itemDatas[_showItemIndex]);
			}
		}

		private void Update()
		{
			InputToShowItem();
		}

		private void InitItemList()
		{
			foreach (var itemData in InventoryStaticSO.itemDatas)
			{
				GameObject namePanel = Instantiate(_itemNamePanel, _contents);
				namePanel.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
				_namePanelObjectList.Add(namePanel);
			}
		}

		private void InputToShowItem()
		{
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				_showItemIndex = (_showItemIndex + 1) % InventoryStaticSO.itemDatas.Count;
				SetItem(InventoryStaticSO.itemDatas[_showItemIndex]);
			}
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				_showItemIndex--;
				if (_showItemIndex < 0)
				{
					_showItemIndex = InventoryStaticSO.itemDatas.Count - 1;
				}
				SetItem(InventoryStaticSO.itemDatas[_showItemIndex]);
			}
		}

	}
}
