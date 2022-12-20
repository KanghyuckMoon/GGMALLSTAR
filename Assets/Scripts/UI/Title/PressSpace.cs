using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;
using Loading;


namespace UI
{
	public delegate IEnumerator coroutineEvent();

	public class PressSpace : MonoBehaviour
	{
		public coroutineEvent CoroutineEvent
		{
			get
			{
				return _coroutineEvent;
			}
			set
			{
				_coroutineEvent = value;
			}
		}

		[SerializeField, FormerlySerializedAs("sceneName")]
		private string _sceneName;
		[SerializeField, FormerlySerializedAs("coroutineEvent")]
		private coroutineEvent _coroutineEvent;
		private bool _isInput = false;

		
		/// <summary>
		/// ¥Ÿ¿Ω æ¿ ¿Ãµø
		/// </summary>
		public void LoadScene()
		{
			LoadingScene.Instance.LoadScene(_sceneName);
		}

		private void Update()
		{
			if (!_isInput)
			{
				if (Input.anyKeyDown)
				{
					_isInput = true;
					StartCoroutine(SceneMove());
				}
			}
		}

		private IEnumerator SceneMove()
		{
			yield return _coroutineEvent();
			LoadScene();
		}
	}

}