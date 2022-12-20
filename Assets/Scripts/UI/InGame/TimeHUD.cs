using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

namespace UI.InGame
{
	public class TimeHUD : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("timeText")] 
		private TextMeshProUGUI _timeText;
		private RoundManager _roundManager;

		private void Start()
		{
			_roundManager = FindObjectOfType<RoundManager>();
			_roundManager.TimeChangeEvent += TimeChange;
		}

		private void TimeChange()
		{
			_timeText.text = $"{(int)_roundManager.Time}";
		}

	}

}