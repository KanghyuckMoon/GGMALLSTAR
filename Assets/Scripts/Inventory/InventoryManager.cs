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
		private AllItemSO allItemSO;
		private bool isInit = false;
		public void Start()
		{
			if (!isInit)
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
			isInit = true;

			//inventorySO = AddressablesManager.Instance.GetResource<InventorySO>("InventorySO");
			allItemSO = AddressablesManager.Instance.GetResource<AllItemSO>("AllItemSO");
			allItemSO.ItemDataSOToAllItemAddressName();
			SaveManager.Load<InventoryData>(ref inventoryData);

			foreach(string itemAddressName in inventoryData.itemAddressNames)
			{
				InventoryStaticSO.itemDatas.Add(AddressablesManager.Instance.GetResource<ItemDataSO>(itemAddressName));
				allItemSO.allItemStaticSO.allItemAddressNames.Remove(itemAddressName);
			}
			allItemSO.allItemStaticSO.allItemAddressNames = allItemSO.allItemStaticSO.allItemAddressNames.OrderBy(a => Guid.NewGuid()).ToList();
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
				ItemDataSO itemDataSO = AddressablesManager.Instance.GetResource<ItemDataSO>(itemAdressName);
				InventoryStaticSO.itemDatas.Add(itemDataSO);
				FindObjectOfType<ItemPopUpManager>(true).SetItemPopUp(itemDataSO.itemName);
				SaveManager.Save<InventoryData>(ref inventoryData);
			}
		}

		/// <summary>
		/// ∑£¥˝ æ∆¿Ã≈€ »πµÊ
		/// </summary>
		public void RandomGetItem()
		{
			if (!isInit)
			{
				Init();
			}

			if (allItemSO.allItemStaticSO.allItemAddressNames.Count > 0)
			{
				string addressName = allItemSO.allItemStaticSO.allItemAddressNames[0];
				allItemSO.allItemStaticSO.allItemAddressNames.Remove(addressName);
				GetItem(addressName);
			}
			else if (!inventoryData.itemAddressNames.Contains("I_GGMALLSTAR"))
			{
				inventoryData.itemAddressNames.Add("I_GGMALLSTAR");
				ItemDataSO itemDataSO = AddressablesManager.Instance.GetResource<ItemDataSO>("I_GGMALLSTAR");
				InventoryStaticSO.itemDatas.Add(itemDataSO);
				FindObjectOfType<ItemPopUpManager>(true).SetItemPopUp(itemDataSO.itemName);
				SaveManager.Save<InventoryData>(ref inventoryData);
			}

		}

	}
}
