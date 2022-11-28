using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeHUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timeText;
	private RoundManager roundManager;

	private void Start()
	{
		roundManager = FindObjectOfType<RoundManager>();
		roundManager.TimeChangeEvent += TimeChange;
	}

	private void TimeChange()
	{
		timeText.text = $"{(int)roundManager.Time}";
	}

}
