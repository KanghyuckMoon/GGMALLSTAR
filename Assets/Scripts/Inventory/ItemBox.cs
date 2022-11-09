using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class ItemBox : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			gameObject.SetActive(false);
			InventoryManager.Instance.RandomGetItem();
		}
	}

}