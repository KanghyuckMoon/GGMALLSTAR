using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Json;

namespace Setting
{
	public class SettingSaveLoad : MonoBehaviour
	{
		[ContextMenu("Save")]
		public void Save()
		{
			SettingManager.Instance.Save();
		}

		[ContextMenu("Load")]
		public void Load()
		{
			SettingManager.Instance.Load();
		}
	}
}
