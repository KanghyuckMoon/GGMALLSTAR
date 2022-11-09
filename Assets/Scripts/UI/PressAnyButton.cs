using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loading;

public class PressAnyButton : MonoBehaviour
{

	public string sceneName;

	public void LoadScene()
	{
		LoadingScene.Instance.LoadScene(sceneName);
	}

	void Update()
    {
        if(Input.anyKeyDown)
		{
			LoadScene();
		}
    }
}
