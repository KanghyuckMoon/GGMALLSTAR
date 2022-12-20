using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Json;

namespace Setting
{
	/// <summary>
	/// ���� ������ �ҷ����� �� ����
	/// </summary>
	public class SettingSaveLoad : MonoBehaviour
	{
		/// <summary>
		/// ���� ������ ���̺�
		/// </summary>
		[ContextMenu("Save")]
		public void Save()
		{
			SettingManager.Instance.Save();
		}

		/// <summary>
		/// ���� ������ �ε�
		/// </summary>
		[ContextMenu("Load")]
		public void Load()
		{
			SettingManager.Instance.Load();
		}
	}
}
