using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Json;
using Addressable;
using System;

namespace Inventory
{
	public class InventoryManager : MonoSingleton<InventoryManager>
	{
		private InventoryData _inventoryData = new InventoryData();
		private AllItemSO _allItemSO;
		private bool _isInit = false;
		public void Start()
		{
			if (!_isInit)
			{
				if (Instance == this)
				{
					Init();
				}
				else
				{
					Instance.Init();
				}
			}
		}

		private void Init()
		{
			_isInit = true;

			//inventorySO = AddressablesManager.Instance.GetResource<InventorySO>("InventorySO");
			_allItemSO = AddressablesManager.Instance.GetResource<AllItemSO>("AllItemSO");
			_allItemSO.ItemDataSOToAllItemAddressName();
			SaveManager.Load<InventoryData>(ref _inventoryData);

			foreach(string itemAddressName in _inventoryData.itemAddressNames)
			{
				InventoryStaticSO.itemDatas.Add(AddressablesManager.Instance.GetResource<ItemDataSO>(itemAddressName));
				_allItemSO.allItemStaticSO.allItemAddressNames.Remove(itemAddressName);
			}
			_allItemSO.allItemStaticSO.allItemAddressNames = _allItemSO.allItemStaticSO.allItemAddressNames.OrderBy(a => Guid.NewGuid()).ToList();
		}

		/// <summary>
		/// æÓµÂ∑πΩ∫∞™¿∏∑Œ æ∆¿Ã≈€ »πµÊ
		/// </summary>
		/// <param name="itemAdressName"></param>
		public void GetItem(string itemAdressName)
		{ 
			if(!_isInit)
			{
				Init();
			}

			if(!_inventoryData.itemAddressNames.Contains(itemAdressName))
			{
				_inventoryData.itemAddressNames.Add(itemAdressName);
				ItemDataSO itemDataSO = AddressablesManager.Instance.GetResource<ItemDataSO>(itemAdressName);
				InventoryStaticSO.itemDatas.Add(itemDataSO);
				FindObjectOfType<ItemPopUpManager>(true).SetItemPopUp(itemDataSO.itemName);
				SaveManager.Save<InventoryData>(ref _inventoryData);
			}
		}

		/// <summary>
		/// ∑£¥˝ æ∆¿Ã≈€ »πµÊ
		/// </summary>
		public void RandomGetItem()
		{
			if (!_isInit)
			{
				Init();
			}

			if (_allItemSO.allItemStaticSO.allItemAddressNames.Count > 0)
			{
				string addressName = _allItemSO.allItemStaticSO.allItemAddressNames[0];
				_allItemSO.allItemStaticSO.allItemAddressNames.Remove(addressName);
				GetItem(addressName);
			}
			else if (!_inventoryData.itemAddressNames.Contains("I_GGMALLSTAR"))
			{
				_inventoryData.itemAddressNames.Add("I_GGMALLSTAR");
				ItemDataSO itemDataSO = AddressablesManager.Instance.GetResource<ItemDataSO>("I_GGMALLSTAR");
				InventoryStaticSO.itemDatas.Add(itemDataSO);
				FindObjectOfType<ItemPopUpManager>(true).SetItemPopUp(itemDataSO.itemName);
				SaveManager.Save<InventoryData>(ref _inventoryData);
			}

		}

	}
}
