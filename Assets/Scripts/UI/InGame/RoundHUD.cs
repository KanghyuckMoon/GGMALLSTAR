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

	[SerializeField] private Material originMaterial;
	[SerializeField] private Material negativeMaterial;

	private Vector2 originVector;

	private RoundManager roundManager;

	private Coroutine coroutine;

	private void Awake()
	{
		roundManager = FindObjectOfType<RoundManager>();
		roundManager.GameSetEvent += GameSet;
		roundManager.RoundSetEvent += RoundSet;
		roundManager.RoundReadyEvent += RoundReady;
		roundManager.RoundStartEvent += RoundStart;
		roundManager.RoundEndEvent += RoundEnd;
		roundManager.GameEndEvent += GameEnd;

		originVector = roundtext.rectTransform.anchoredPosition;
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
			CoroutineStop();
			coroutine = StartCoroutine(RoundSetText("Round Final"));
		}
		else
		{
			CoroutineStop();
			coroutine = StartCoroutine(RoundSetText($"Round {roundManager.RoundNumber}"));
		}
		upperHud.gameObject.SetActive(true);
		frameImage.gameObject.SetActive(false);

	}

	private IEnumerator RoundSetText(string text)
	{
		roundtext.rectTransform.DOKill();
		roundtext.text = text;
		roundtext.fontMaterial = negativeMaterial;
		roundtext.rectTransform.localScale = Vector3.one * 2;
		roundtext.gameObject.SetActive(true);
		roundtext.rectTransform.DOShakePosition(0.2f, 10f);
		yield return new WaitForSeconds(0.2f);
		roundtext.fontMaterial = originMaterial;
		roundtext.DOKill();
		roundtext.rectTransform.DOScale(4f, 0.7f);
		roundtext.rectTransform.anchoredPosition = originVector;
		yield return new WaitForSeconds(0.7f);
		roundtext.gameObject.SetActive(false);
	}

	private void RoundStart()
	{
		CoroutineStop();
		coroutine = StartCoroutine(RoundStartText($"GO"));
	}

	private IEnumerator RoundStartText(string text)
	{
		roundtext.rectTransform.DOKill();
		roundtext.text = text;
		roundtext.rectTransform.localScale = Vector3.one * 20;
		roundtext.gameObject.SetActive(true);
		roundtext.rectTransform.DOScale(5f, 0.3f).SetEase(Ease.OutBack);
		yield return new WaitForSeconds(0.1f);
		roundtext.fontMaterial = negativeMaterial;
		yield return new WaitForSeconds(0.05f);
		roundtext.fontMaterial = originMaterial;
		yield return new WaitForSeconds(0.1f);
		roundtext.rectTransform.DOShakePosition(0.3f, 10f);
		yield return new WaitForSeconds(0.3f);
		roundtext.rectTransform.anchoredPosition = originVector;
		yield return new WaitForSeconds(1f);
		roundtext.gameObject.SetActive(false);
	}

	private void RoundEnd()
	{
		CoroutineStop();
		coroutine = StartCoroutine(RoundEndText($"KO"));
		CountImageSetting();
		upperHud.gameObject.SetActive(false);
		frameImage.gameObject.SetActive(true);
	}

	private IEnumerator RoundEndText(string text)
	{
		yield return null;
		roundtext.rectTransform.DOKill();
		roundtext.text = text;
		roundtext.fontMaterial = originMaterial;
		roundtext.rectTransform.localScale = Vector3.one * 2;
		roundtext.rectTransform.DOScale(4f, 1f).OnComplete(() => roundtext.gameObject.SetActive(false));
	}

	private void RoundReady()
	{
		CoroutineStop();
		coroutine = StartCoroutine(SetText($"Ready"));
	}
	private void GameEnd()
	{
		CoroutineStop();
		coroutine = StartCoroutine(RoundEndText($"Game Set"));
		CountImageSetting();
		upperHud.gameObject.SetActive(false);
		frameImage.gameObject.SetActive(true);
	}

	public IEnumerator SetText(string str)
	{
		roundtext.fontMaterial = negativeMaterial;
		yield return new WaitForSeconds(0.02f);
		roundtext.fontMaterial = originMaterial;

		roundtext.gameObject.SetActive(true);
		roundtext.rectTransform.DOKill();
		roundtext.rectTransform.localScale = Vector3.zero;

		roundtext.text = str;

		roundtext.rectTransform.DOScale(4f, 1f).OnComplete(() => roundtext.gameObject.SetActive(false));
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

	private void CoroutineStop()
	{
		if(coroutine != null)
		{
			StopCoroutine(coroutine);
		}
	}

}
