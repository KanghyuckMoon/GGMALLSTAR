using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utill;
using Addressable;

namespace Sound
{
	/// <summary>
	/// ȿ���� �� ��������� ����ϴ� �Ŵ���
	/// </summary>
	public class SoundManager : MonoSingleton<SoundManager>
	{
		public float BGMPitch
		{
			get
			{
				return _bgmPitch;
			}
		}

		private AudioMixer _audioMixer;
		private AudioMixerGroup _bgmAudioGroup;
		private AudioMixerGroup _effAudioGroup;
		private AudioSource _bgmAudioSource = null;
		private AudioSource _effAudioSource = null;
		private Dictionary<AudioBGMType, AudioClip> _bgmAudioClips = new Dictionary<AudioBGMType, AudioClip>();
		private AudioBGMType _currentBGMType = AudioBGMType.Count;
		private EFFSO _effSO;
		private float _bgmPitch = 1.0f;

		private bool _isInit = false; //���� �Ŵ��� �ʱ�ȭ ����


		/// <summary>
		/// ȿ���� ��� �Լ�
		/// </summary>
		/// <param name="audioName"></param>
		public void PlayEFF(string audioName)
		{
			if (!_isInit)
			{
				Init();
			}

			//EffSO���� ȿ������ �����´�
			AudioClip clip = _effSO.GetEFFClip(audioName);
			_effAudioSource.PlayOneShot(clip);
		}

		/// <summary>
		/// ������� ���
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

		/// <summary>
		/// BGM �ӵ� ����
		/// </summary>
		/// <param name="speed"></param>
		public void SetBGMSpeed(float speed)
		{
			_bgmPitch = speed;
			_bgmAudioSource.pitch = speed;
		}
		private void Start()
		{
			if (!_isInit)
			{
				if (Instance == this)
				{
					Init();
				}
				else
				{
					Instance.Init();
				}
			}
		}

		/// <summary>
		/// �ʱ�ȭ
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
		/// ��� ���� ��������
		/// </summary>
		private void GetBGMAudioSource()
		{

			//���ο� ����� �ҽ� �����
			GameObject obj = new GameObject("BGM");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _bgmAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_bgmAudioSource = audioSource;

			int count = (int)AudioBGMType.Count;

			//��ݵ� �̸� ����
			for (int i = 0; i < count; ++i)
			{
				string key = System.Enum.GetName(typeof(AudioBGMType), i);
				AudioClip audioClip = AddressablesManager.Instance.GetResource<AudioClip>(key);
				_bgmAudioClips.Add((AudioBGMType)i, audioClip);
			}
		}

		/// <summary>
		/// ����Ʈ ����� �ҽ��� ����
		/// </summary>
		private void GenerateEFFAudioSource()
		{
			//���ο� ����� �ҽ� �����
			GameObject obj = new GameObject("EFF");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _effAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_effAudioSource = audioSource;
		}

	}

}