using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RoundHUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI roundtext;

	private RoundManager roundManager;

	private void Start()
	{
		roundManager = FindObjectOfType<RoundManager>();
		roundManager.RoundSetEvent += RoundSet;
		roundManager.RoundReadyEvent += RoundReady;
		roundManager.RoundStartEvent += RoundStart;
		roundManager.RoundEndEvent += RoundEnd;
		roundManager.GameEndEvent += GameEnd;
	}

	private void RoundSet()
	{
		if (roundManager.RoundNumber == 5)
		{
			SetText("Round Final");
		}
		else
		{
			SetText($"Round {roundManager.RoundNumber}");
		}
	}
	private void RoundStart()
	{
		SetText($"GO");
	}
	private void RoundEnd()
	{
		SetText($"KO");
	}
	private void RoundReady()
	{
		SetText($"Ready");
	}
	private void GameEnd()
	{
		SetText($"Game Set");
	}

	public void SetText(string str)
	{
		roundtext.gameObject.SetActive(true);
		roundtext.rectTransform.DOKill();
		roundtext.rectTransform.localScale = Vector3.zero;

		roundtext.text = str;

		roundtext.rectTransform.DOScale(2f, 1f).OnComplete(() => roundtext.gameObject.SetActive(false));
	}
}
