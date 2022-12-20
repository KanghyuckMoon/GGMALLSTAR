using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Setting
{
	public class QuitGame : MonoBehaviour
	{
		/// <summary>
		/// 게임 정료
		/// </summary>
		public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit(); // 어플리케이션 종료
#endif
		}
	}
}
