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
		private InventoryData inventoryData = new InventoryData();
		private InventorySO inventorySO;
		private AllItemSO allItemSO;
		private bool isInit = false;
		public void Awake()
		{
			Instance.Init();
		}

		private void Init()
		{
			isInit = true;

			inventorySO = AddressablesManager.Instance.GetResource<InventorySO>("InventorySO");
			allItemSO = AddressablesManager.Instance.GetResource<AllItemSO>("AllItemSO");
			SaveManager.Load<InventoryData>(ref inventoryData);

			foreach(string itemAddressName in inventoryData.itemAddressNames)
			{
				inventorySO.itemDatas.Add(AddressablesManager.Instance.GetResource<ItemDataSO>(itemAddressName));
				allItemSO.allItemAddressNames.Remove(itemAddressName);
			}
			allItemSO.allItemAddressNames = allItemSO.allItemAddressNames.OrderBy(a => Guid.NewGuid()).ToList();
		}

		public void GetItem(string itemAdressName)
		{ 
			if(!isInit)
			{
				Init();
			}

			if(!inventoryData.itemAddressNames.Contains(itemAdressName))
			{
				inventoryData.itemAddressNames.Add(itemAdressName);
				inventorySO.itemDatas.Add(AddressablesManager.Instance.GetResource<ItemDataSO>(itemAdressName));
				SaveManager.Save<InventoryData>(ref inventoryData);
			}
		}

		/// <summary>
		/// ∑£¥˝ æ∆¿Ã≈€ »πµÊ
		/// </summary>
		public void RandomGetItem()
		{
			if(allItemSO.allItemAddressNames.Count > 0)
			{
				string addressName = allItemSO.allItemAddressNames[0];
				allItemSO.allItemAddressNames.Remove(addressName);
				GetItem(addressName);
			}

		}

	}
}
