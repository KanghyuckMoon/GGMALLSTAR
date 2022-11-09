using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loading;

public class StageSelect : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		LoadingScene.Instance.LoadScene(sceneName);
	}
}
