using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Json;
using Utill;

namespace Setting
{
	/// <summary>
	/// 세팅 매니저
	/// </summary>
    public class SettingManager : MonoSingleton<SettingManager>
    {
        public SettingData SettingData
		{
            get
			{
				return settingData;
			}
		}
		private SettingData settingData = new SettingData();

		/// <summary>
		/// 세팅데이터 저장
		/// </summary>
		public void Save()
		{
			SaveManager.Save<SettingData>(ref settingData);
		}

		/// <summary>
		/// 세팅데이터 불러오기
		/// </summary>
		public void Load()
		{
			SaveManager.Load<SettingData>(ref settingData);
		}
	}

}