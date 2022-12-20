using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
	/// <summary>
	/// �ɼ�â UI
	/// </summary>
	public class SettingUI : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("panels")]
		private GameObject[] _panels;

		/// <summary>
		/// ������ �г� Ű��
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
