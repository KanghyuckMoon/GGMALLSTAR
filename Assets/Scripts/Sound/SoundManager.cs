using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utill;
using Addressable;

namespace Sound
{
	public class SoundManager : MonoSingleton<SoundManager>
	{
		private AudioMixer _audioMixer;
		private AudioMixerGroup _bgmAudioGroup;
		private AudioMixerGroup _effAudioGroup;
		private AudioSource _bgmAudioSource = null;
		private AudioSource _effAudioSource = null;
		private Dictionary<AudioBGMType, AudioClip> _bgmAudioClips = new Dictionary<AudioBGMType, AudioClip>();
		private AudioBGMType _currentBGMType = AudioBGMType.Count;
		private EFFSO _effSO;
		private bool _isInit = false;

		public void Awake()
		{
			Init();
		}

		/// <summary>
		/// 초기화
		/// </summary>
		private void Init()
		{
			if (_isInit)
			{
				return;
			}
			_isInit = true;

			_effSO = AddressablesManager.Instance.GetResource<EFFSO>("EFFSO");
			_audioMixer = AddressablesManager.Instance.GetResource<AudioMixer>("MainMixer");

			var groups = _audioMixer.FindMatchingGroups(string.Empty);
			_bgmAudioGroup = groups[1];
			_effAudioGroup = groups[2];

			GetBGMAudioSource();
			GenerateEFFAudioSource();
		}

		/// <summary>
		/// 배경 음악 가져오기
		/// </summary>
		private void GetBGMAudioSource()
		{

			//새로운 오디오 소스 만들기
			GameObject obj = new GameObject("BGM");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _bgmAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_bgmAudioSource = audioSource;

			int count = (int)AudioBGMType.Count;
			for (int i = 0; i < count; ++i)
			{
				string key = System.Enum.GetName(typeof(AudioBGMType), i);
				AudioClip audioClip = AddressablesManager.Instance.GetResource<AudioClip>(key);
				_bgmAudioClips.Add((AudioBGMType)i, audioClip);
			}
		}

		/// <summary>
		/// 이펙트 오디오 소스들 생성
		/// </summary>
		private void GenerateEFFAudioSource()
		{
			//새로운 오디오 소스 만들기
			GameObject obj = new GameObject("EFF");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _bgmAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_effAudioSource = audioSource;
		}

		/// <summary>
		/// 효과음 재생 개량중 사용금지
		/// </summary>
		/// <param name="audioEFFType"></param>
		public void PlayEFF(AudioEFFType audioEFFType)
		{
			if (!_isInit)
			{
				Init();
			}

			//_effAudioSource[audioEFFType].Play();
		}

		public void PlayEFF(string audioName)
		{
			if (!_isInit)
			{
				Init();
			}

			_effAudioSource.PlayOneShot(_effSO.GetEFFClip(audioName));
		}
		public void PlayEFF(AudioClip clip)
		{
			if (!_isInit)
			{
				Init();
			}

			_effAudioSource.PlayOneShot(clip);
		}


		/// <summary>
		/// 배경음악 재생
		/// </summary>
		/// <param name="audioBGMType"></param>
		public void PlayBGM(AudioBGMType audioBGMType)
		{
			if (!_isInit)
			{
				Init();
			}

			if (_currentBGMType == audioBGMType)
			{
				return;
			}

			_currentBGMType = audioBGMType;

			_bgmAudioSource.Stop();
			_bgmAudioSource.clip = _bgmAudioClips[audioBGMType];
			_bgmAudioSource.Play();
		}
	}

}