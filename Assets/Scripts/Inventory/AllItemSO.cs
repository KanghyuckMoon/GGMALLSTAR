using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu]
	public class AllItemSO : ScriptableObject
	{
		public List<ItemDataSO> itemDataSOs = new List<ItemDataSO>();
		public List<string> allItemAddressNames = new List<string>();

		[ContextMenu("ItemDataSOToAllItemAddressName")]
		public void ItemDataSOToAllItemAddressName()
		{
			foreach(ItemDataSO itemDataSO in itemDataSOs)
			{
				allItemAddressNames.Add(itemDataSO.name);
			}
		}

	}
}
