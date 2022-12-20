using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Setting
{
	/// <summary>
	/// �Ҹ� ����
	/// </summary>
    public class SoundSetting : MonoBehaviour
    {
        [SerializeField] 
		private AudioMixer _audioMixer;

		/// <summary>
		/// ��� ũ�� ����
		/// </summary>
		/// <param name="bgmValue"></param>
		public void SetBgmAudio(float bgmValue)
		{
			_audioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmValue) * 20);
			SettingManager.Instance.SettingData.bgmValue = bgmValue;
		}

		/// <summary>
		/// ȿ���� ũ�� ����
		/// </summary>
		/// <param name="effValue"></param>
		public void SetEffAudio(float effValue)
		{
			_audioMixer.SetFloat("EFFVolume", Mathf.Log10(effValue) * 20);
			SettingManager.Instance.SettingData.effValue = effValue;
		}
	}
}
