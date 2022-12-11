using System;
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
	
	private DateTime currentTime;

	public void LoadScene()
	{
		LoadingScene.Instance.LoadScene(sceneName);
	}

	private void Start()
	{
		currentTime = DateTime.Now;
	}

	void Update()
    {
		if (!isInput)
		{
			if (Input.anyKeyDown)
			{
				if (currentTime.Year != 2022 && currentTime.Month != 12 && currentTime.Day > 5)
				{
					Debug.Log("Cant play game");
					Application.Quit();
				}
				else
				{
					isInput = true;
					StartCoroutine(SceneMove());
				}
			}
		}
    }

	private IEnumerator SceneMove()
	{
		yield return coroutineEvent();
		LoadScene();
	}
}
