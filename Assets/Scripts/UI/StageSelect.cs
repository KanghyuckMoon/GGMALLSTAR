using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loading;

namespace UI
{
	public class StageSelect : MonoBehaviour
	{
		/// <summary>
		/// 받은 씬 이름으로 씬 이동
		/// </summary>
		/// <param name="sceneName"></param>
		public void LoadScene(string sceneName)
		{
			LoadingScene.Instance.LoadScene(sceneName, LoadingScene.LoadingSceneType.Battle);
		}

		/// <summary>
		/// 아케이드 씬으로 씬 이동
		/// </summary>
		public void ArcadeLoadScene()
		{
			LoadingScene.Instance.LoadScene("Arcade", LoadingScene.LoadingSceneType.Normal);
		}
	}

}