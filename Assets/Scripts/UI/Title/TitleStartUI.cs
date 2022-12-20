using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Sound;

namespace UI
{
	public class TitleStartUI : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("screenImage")]
		private Image _screenImage;
		[SerializeField, FormerlySerializedAs("effSound")]
		private string _effSound;
		[SerializeField, FormerlySerializedAs("pressSpace")]
		private PressSpace _pressSpace;
		[SerializeField, FormerlySerializedAs("speed")]
		private float _speed = 1.0f;

		private void Start()
		{
			_pressSpace.CoroutineEvent += StartAnimation;
		}

		private IEnumerator StartAnimation()
		{
			SoundManager.Instance.PlayEFF(_effSound);
			for (float i = 0; i < 1.0f;)
			{
				i += Time.deltaTime * _speed;
				_screenImage.color = new Color(1, 1, 1, i);
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}
	}

}