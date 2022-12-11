using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RoundHUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI roundtext;
	[SerializeField] private Image[] roundCountP1;
	[SerializeField] private Image[] roundCountP2;
	[SerializeField] private Sprite winSprite;

	[SerializeField] private GameObject upperHud;
	[SerializeField] private GameObject frameImage;

	private RoundManager roundManager;

	private void Awake()
	{
		roundManager = FindObjectOfType<RoundManager>();
		roundManager.GameSetEvent += GameSet;
		roundManager.RoundSetEvent += RoundSet;
		roundManager.RoundReadyEvent += RoundReady;
		roundManager.RoundStartEvent += RoundStart;
		roundManager.RoundEndEvent += RoundEnd;
		roundManager.GameEndEvent += GameEnd;
	}
	private void GameSet()
	{
		upperHud.gameObject.SetActive(false);
		frameImage.gameObject.SetActive(true);
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
		upperHud.gameObject.SetActive(true);
		frameImage.gameObject.SetActive(false);
	}
	private void RoundStart()
	{
		SetText($"GO");
	}
	private void RoundEnd()
	{
		SetText($"KO");
		CountImageSetting();
		upperHud.gameObject.SetActive(false);
		frameImage.gameObject.SetActive(true);
	}
	private void RoundReady()
	{
		SetText($"Ready");
	}
	private void GameEnd()
	{
		SetText($"Game Set");
		CountImageSetting();
		upperHud.gameObject.SetActive(false);
		frameImage.gameObject.SetActive(true);
	}

	public void SetText(string str)
	{
		roundtext.gameObject.SetActive(true);
		roundtext.rectTransform.DOKill();
		roundtext.rectTransform.localScale = Vector3.zero;

		roundtext.text = str;

		roundtext.rectTransform.DOScale(2f, 1f).OnComplete(() => roundtext.gameObject.SetActive(false));
	}

	private void CountImageSetting()
	{
		for (int i = 0; i < roundManager.WinCountP1; ++i)
		{
			roundCountP1[i].sprite = winSprite;
		}

		for (int i = 0; i < roundManager.WinCountP2; ++i)
		{
			roundCountP2[i].sprite = winSprite;
		}
	}

}
