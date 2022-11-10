using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inventory
{
	public class ItemPopUpLoad : MonoBehaviour
	{
		public void Start()
		{
			SceneManager.LoadScene("ItemPopUp", LoadSceneMode.Additive);
		}
	}
}
