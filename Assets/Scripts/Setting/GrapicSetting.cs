using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Setting
{
	public class GrapicSetting : MonoBehaviour
	{
		public Vector2[] resolutionList;

		//그래픽 품질 설정
		public void ChangeGrapicSetting(int index)
		{
			QualitySettings.SetQualityLevel(index, true);
			SettingManager.Instance.SettingData.grapicQualityIndex = index;
		}

		//전체화면 설정
		public void ChangeFullScreen(bool isFullScreen)
		{
			#if UNITY_EDITOR
			EditorWindow.focusedWindow.maximized = isFullScreen;
#else
				Screen.fullScreen = isFullScreen;
#endif
			SettingManager.Instance.SettingData.isFullScreen = isFullScreen;
		}

		//해상도 설정
		public void ChangeResolutionSetting(int index)
		{
			Screen.SetResolution((int)resolutionList[index].x, (int)resolutionList[index].y, Screen.fullScreen);
			SettingManager.Instance.SettingData.resolutionIndex = index;
		}

	}
}
