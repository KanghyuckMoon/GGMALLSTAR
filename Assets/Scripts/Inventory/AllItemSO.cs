using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu]
	public class AllItemSO : ScriptableObject
	{
		public List<ItemDataSO> itemDataSOs = new List<ItemDataSO>();
		public AllItemStaticSO allItemStaticSO = new AllItemStaticSO();

		[ContextMenu("ItemDataSOToAllItemAddressName")]
		public void ItemDataSOToAllItemAddressName()
		{
			allItemStaticSO.allItemAddressNames.Clear();
			foreach (ItemDataSO itemDataSO in itemDataSOs)
			{
				allItemStaticSO.allItemAddressNames.Add(itemDataSO.name);
			}
		}
	}

	public class AllItemStaticSO
	{
		public List<string> allItemAddressNames = new List<string>();
	}
}
