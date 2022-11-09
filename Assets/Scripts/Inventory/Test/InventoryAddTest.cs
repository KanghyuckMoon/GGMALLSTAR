using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class InventoryAddTest : MonoBehaviour
	{
		public string addressname;

		[ContextMenu("Add")]
		public void add()
		{
			InventoryManager.Instance.GetItem(addressname);
		}
	}

}