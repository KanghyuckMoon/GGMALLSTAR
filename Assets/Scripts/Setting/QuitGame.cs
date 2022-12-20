using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Setting
{
	public class QuitGame : MonoBehaviour
	{
		/// <summary>
		/// ���� ����
		/// </summary>
		public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit(); // ���ø����̼� ����
#endif
		}
	}
}
