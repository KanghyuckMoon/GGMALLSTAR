using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Json;
using Utill;

namespace Setting
{
	/// <summary>
	/// ���� �Ŵ���
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
		/// ���õ����� ����
		/// </summary>
		public void Save()
		{
			SaveManager.Save<SettingData>(ref settingData);
		}

		/// <summary>
		/// ���õ����� �ҷ�����
		/// </summary>
		public void Load()
		{
			SaveManager.Load<SettingData>(ref settingData);
		}
	}

}