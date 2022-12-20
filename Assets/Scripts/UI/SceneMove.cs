using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Loading;

public class SceneMove : MonoBehaviour
{
	[SerializeField, FormerlySerializedAs("sceneName")]
	private string _sceneName;

	[SerializeField, FormerlySerializedAs("loadingSceneType")]
	private LoadingScene.LoadingSceneType _loadingSceneType = LoadingScene.LoadingSceneType.Normal;

	public void LoadScene()
	{
		LoadingScene.Instance.LoadScene(_sceneName, _loadingSceneType);
	}
}
