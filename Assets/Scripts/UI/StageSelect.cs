using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loading;

namespace UI
{
	public class StageSelect : MonoBehaviour
	{
		/// <summary>
		/// ���� �� �̸����� �� �̵�
		/// </summary>
		/// <param name="sceneName"></param>
		public void LoadScene(string sceneName)
		{
			LoadingScene.Instance.LoadScene(sceneName, LoadingScene.LoadingSceneType.Battle);
		}

		/// <summary>
		/// �����̵� ������ �� �̵�
		/// </summary>
		public void ArcadeLoadScene()
		{
			LoadingScene.Instance.LoadScene("Arcade", LoadingScene.LoadingSceneType.Normal);
		}
	}

}