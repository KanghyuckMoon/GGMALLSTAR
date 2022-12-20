using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Json;

namespace Setting
{
	/// <summary>
	/// 세팅 데이터 불러오기 및 저장
	/// </summary>
	public class SettingSaveLoad : MonoBehaviour
	{
		/// <summary>
		/// 세팅 데이터 세이브
		/// </summary>
		[ContextMenu("Save")]
		public void Save()
		{
			SettingManager.Instance.Save();
		}

		/// <summary>
		/// 세팅 데이터 로드
		/// </summary>
		[ContextMenu("Load")]
		public void Load()
		{
			SettingManager.Instance.Load();
		}
	}
}
