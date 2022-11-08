using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory
{
	[CreateAssetMenu]
	public class ItemDataSO : ScriptableObject
	{
		//CollaborationGame
		public string itemName;
		public GameObject prefeb;
	}

}