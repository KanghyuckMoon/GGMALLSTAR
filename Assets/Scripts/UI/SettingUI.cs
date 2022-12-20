using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
	/// <summary>
	/// 옵션창 UI
	/// </summary>
	public class SettingUI : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("panels")]
		private GameObject[] _panels;

		/// <summary>
		/// 선택한 패널 키기
		/// </summary>
		/// <param name="index"></param>
		public void ActivePanel(int index)
		{
			foreach (var panel in _panels)
			{
				panel.SetActive(false);
			}
			_panels[index].SetActive(true);
		}
	}
}
