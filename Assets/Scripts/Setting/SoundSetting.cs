using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Setting
{
    public class SoundSetting : MonoBehaviour
    {
        [SerializeField] AudioMixer _audioMixer;

		/// <summary>
		/// 브금 크기 설정
		/// </summary>
		/// <param name="bgmValue"></param>
		public void SetBgmAudio(float bgmValue)
		{
			_audioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmValue) * 20);
		}

		/// <summary>
		/// 효과음 크기 설정
		/// </summary>
		/// <param name="effValue"></param>
		public void SetEffAudio(float effValue)
		{
			_audioMixer.SetFloat("EFFVolume", Mathf.Log10(effValue) * 20);
		}
	}
}
