using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Json;
using Utill;

namespace Setting
{
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


		public void Save()
		{
			SaveManager.Save<SettingData>(ref settingData);
		}

		public void Load()
		{
			SaveManager.Load<SettingData>(ref settingData);
		}
	}

}