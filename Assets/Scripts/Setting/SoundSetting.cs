using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Setting
{
	/// <summary>
	/// 소리 설정
	/// </summary>
    public class SoundSetting : MonoBehaviour
    {
        [SerializeField] 
		private AudioMixer _audioMixer;

		/// <summary>
		/// 브금 크기 설정
		/// </summary>
		/// <param name="bgmValue"></param>
		public void SetBgmAudio(float bgmValue)
		{
			_audioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmValue) * 20);
			SettingManager.Instance.SettingData.bgmValue = bgmValue;
		}

		/// <summary>
		/// 효과음 크기 설정
		/// </summary>
		/// <param name="effValue"></param>
		public void SetEffAudio(float effValue)
		{
			_audioMixer.SetFloat("EFFVolume", Mathf.Log10(effValue) * 20);
			SettingManager.Instance.SettingData.effValue = effValue;
		}
	}
}
