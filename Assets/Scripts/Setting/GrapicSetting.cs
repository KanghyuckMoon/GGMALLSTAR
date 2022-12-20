using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Setting
{
	/// <summary>
	/// 그래픽 설정
	/// </summary>
	public class GrapicSetting : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("resolutionList")]
		private Vector2[] _resolutionList; //해상도 종류

		/// <summary>
		/// 그래픽 품질 설정
		/// </summary>
		/// <param name="index"></param>
		public void ChangeGrapicSetting(int index)
		{
			QualitySettings.SetQualityLevel(index, true);
			SettingManager.Instance.SettingData.grapicQualityIndex = index;
		}
		
		/// <summary>
		/// 전체화면 설정
		/// </summary>
		/// <param name="isFullScreen"></param>
		public void ChangeFullScreen(bool isFullScreen)
		{
			#if UNITY_EDITOR
			EditorWindow.focusedWindow.maximized = isFullScreen;
#else
				Screen.fullScreen = isFullScreen;
#endif
			SettingManager.Instance.SettingData.isFullScreen = isFullScreen;
		}

		/// <summary>
		/// 해상도 설정
		/// </summary>
		/// <param name="index"></param>
		public void ChangeResolutionSetting(int index)
		{
			Screen.SetResolution((int)_resolutionList[index].x, (int)_resolutionList[index].y, Screen.fullScreen);
			SettingManager.Instance.SettingData.resolutionIndex = index;
		}

	}
}
