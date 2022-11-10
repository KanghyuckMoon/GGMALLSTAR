using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Loading;

public delegate IEnumerator coroutineEvent();

public class PressSpace : MonoBehaviour
{
	public string sceneName;
	public coroutineEvent coroutineEvent;
	private bool isInput = false;

	public void LoadScene()
	{
		LoadingScene.Instance.LoadScene(sceneName);
	}

	void Update()
    {
		if (!isInput)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				isInput = true;
				StartCoroutine(SceneMove());
			}
		}
    }

	private IEnumerator SceneMove()
	{
		yield return coroutineEvent();
		LoadScene();
	}
}
