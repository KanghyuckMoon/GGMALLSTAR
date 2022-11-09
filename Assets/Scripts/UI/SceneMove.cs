using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loading;

public class SceneMove : MonoBehaviour
{
	public string sceneName;

	public void LoadScene()
	{
		LoadingScene.Instance.LoadScene(sceneName);
	}
}
