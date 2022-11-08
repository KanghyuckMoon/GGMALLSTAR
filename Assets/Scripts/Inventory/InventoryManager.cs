using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Json;
using Addressable;

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
			Init();
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
			}
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
			while(allItemSO.allItemAddressNames.Count > 0)
			{
				string addressName = allItemSO.allItemAddressNames[Random.Range(0, allItemSO.allItemAddressNames.Count - 1)];
				if(inventoryData.itemAddressNames.Contains(addressName))
				{
					allItemSO.allItemAddressNames.Remove(addressName);
				}
				else
				{
					GetItem(addressName);
					break;
				}
			}

		}

	}
}
