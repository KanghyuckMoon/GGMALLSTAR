using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.InGame
{
	public class HUDLoadScene : MonoBehaviour
	{
		public void Start()
		{
			SceneManager.LoadScene("HUDScene", LoadSceneMode.Additive);
		}
	}

}