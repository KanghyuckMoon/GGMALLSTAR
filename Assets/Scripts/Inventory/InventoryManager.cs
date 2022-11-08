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
		private bool isInit = false;
		public void Awake()
		{
			Init();
		}

		private void Init()
		{
			isInit = true;

			inventorySO = AddressablesManager.Instance.GetResource<InventorySO>("InventorySO");
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

	}
}
