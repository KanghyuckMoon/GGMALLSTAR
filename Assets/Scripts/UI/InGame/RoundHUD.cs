using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UI.InGame;
using TMPro;
using DG.Tweening;

namespace UI.InGame
{
	public class RoundHUD : MonoBehaviour
	{
		//RoundTexts
		[SerializeField, FormerlySerializedAs("roundtext")] 
		private TextMeshProUGUI _roundtext;
		[SerializeField, FormerlySerializedAs("roundCountP1")] 
		private Image[] _roundCountP1;
		[SerializeField, FormerlySerializedAs("roundCountP2")] 
		private Image[] _roundCountP2;
		[SerializeField, FormerlySerializedAs("winSprite")] 
		private Sprite _winSprite;


		[SerializeField, FormerlySerializedAs("upperHud")]
		private GameObject _upperHud;
		[SerializeField, FormerlySerializedAs("frameImage")]
		private GameObject _frameImage;


		[SerializeField, FormerlySerializedAs("originMaterial")]
		private Material _originMaterial;
		[SerializeField, FormerlySerializedAs("negativeMaterial")]
		private Material _negativeMaterial;


		//Infos

		[SerializeField, FormerlySerializedAs("p1Info")]
		private GameObject _p1Info;
		[SerializeField, FormerlySerializedAs("p2Info")]
		private GameObject _p2Info;
		[SerializeField, FormerlySerializedAs("p1InfoTexts")]
		private TextMeshProUGUI[] _p1InfoTexts;
		[SerializeField, FormerlySerializedAs("p2InfoTexts")]
		private TextMeshProUGUI[] _p2InfoTexts;
		[SerializeField, FormerlySerializedAs("infoDatas")]
		private InfoDataSO[] _infoDatas;

		//RoundText Coroutine Variables
		private Vector2 _originVector;
		private RoundManager _roundManager;
		private Coroutine _coroutine;

		private void Awake()
		{
			//RoundText
			_roundManager = FindObjectOfType<RoundManager>();
			_roundManager.GameSetEvent += GameSet;
			_roundManager.RoundSetEvent += RoundSet;
			_roundManager.RoundReadyEvent += RoundReady;
			_roundManager.RoundStartEvent += RoundStart;
			_roundManager.RoundEndEvent += RoundEnd;
			_roundManager.GameEndEvent += GameEnd;
			_originVector = _roundtext.rectTransform.anchoredPosition;

			_upperHud.gameObject.SetActive(false);
			_frameImage.gameObject.SetActive(true);

			//Info
			InfoSetting();
		}
		private void GameSet()
		{
			_upperHud.gameObject.SetActive(false);
			_frameImage.gameObject.SetActive(true);

			//Info
			StartCoroutine(GametSetInfo());
		}
		private void InfoSetting()
		{
			InfoDataSO infoDataSOP1 = null;
			InfoDataSO infoDataSOP2 = null;
			infoDataSOP1 = ReturnInfoData(SelectDataSO.characterSelectP1);
			infoDataSOP2 = ReturnInfoData(SelectDataSO.characterSelectP2);

			//P1 Info Text Setting
			_p1InfoTexts[0].text = infoDataSOP1.originGame;
			_p1InfoTexts[1].text = infoDataSOP1.homeGround;
			_p1InfoTexts[2].text = infoDataSOP1.numbering;

			//P2 Info Text Setting
			_p2InfoTexts[0].text = infoDataSOP2.originGame;
			_p2InfoTexts[1].text = infoDataSOP2.homeGround;
			_p2InfoTexts[2].text = infoDataSOP2.numbering;

		}
		private InfoDataSO ReturnInfoData(CharacterSelect characterSelect)
		{
			switch (characterSelect)
			{
				default:
				case CharacterSelect.Jaeby:
					return _infoDatas[0];
				case CharacterSelect.Frog:
					return _infoDatas[1];
				case CharacterSelect.Dice:
					return _infoDatas[2];
				case CharacterSelect.Puppet:
					return _infoDatas[3];
				case CharacterSelect.Agent:
					return _infoDatas[4];
				case CharacterSelect.Damvi:
					return _infoDatas[5];
			}
		}
		private IEnumerator GametSetInfo()
		{
			yield return new WaitForSeconds(0.2f);
			_p1Info.gameObject.SetActive(true);
			_p2Info.gameObject.SetActive(false);
			yield return new WaitForSeconds(1f);
			_p1Info.gameObject.SetActive(false);
			_p2Info.gameObject.SetActive(true);
			yield return new WaitForSeconds(1f);
			_p1Info.gameObject.SetActive(false);
			_p2Info.gameObject.SetActive(false);
		}
		private void RoundSet()
		{
			if (_roundManager.RoundNumber == 5)
			{
				CoroutineStop();
				_coroutine = StartCoroutine(RoundSetText("Round Final"));
			}
			else
			{
				CoroutineStop();
				_coroutine = StartCoroutine(RoundSetText($"Round {_roundManager.RoundNumber}"));
			}
			_upperHud.gameObject.SetActive(true);
			_frameImage.gameObject.SetActive(false);

		}
		private IEnumerator RoundSetText(string text)
		{
			_roundtext.rectTransform.DOKill();
			_roundtext.text = text;
			_roundtext.fontMaterial = _negativeMaterial;
			_roundtext.rectTransform.localScale = Vector3.one * 2;
			_roundtext.gameObject.SetActive(true);
			_roundtext.rectTransform.DOShakePosition(0.2f, 10f);
			yield return new WaitForSeconds(0.2f);
			_roundtext.fontMaterial = _originMaterial;
			_roundtext.DOKill();
			_roundtext.rectTransform.DOScale(4f, 0.7f);
			_roundtext.rectTransform.anchoredPosition = _originVector;
			yield return new WaitForSeconds(0.7f);
			_roundtext.gameObject.SetActive(false);
		}
		private void RoundStart()
		{
			CoroutineStop();
			_coroutine = StartCoroutine(RoundStartText($"FIGHT"));
		}
		private IEnumerator RoundStartText(string text)
		{
			_roundtext.rectTransform.DOKill();
			_roundtext.text = text;
			_roundtext.rectTransform.localScale = Vector3.one * 20;
			_roundtext.gameObject.SetActive(true);
			_roundtext.rectTransform.DOScale(5f, 0.3f).SetEase(Ease.OutBack);
			yield return new WaitForSeconds(0.1f);
			_roundtext.fontMaterial = _negativeMaterial;
			yield return new WaitForSeconds(0.05f);
			_roundtext.fontMaterial = _originMaterial;
			yield return new WaitForSeconds(0.1f);
			_roundtext.rectTransform.DOShakePosition(0.3f, 10f);
			yield return new WaitForSeconds(0.3f);
			_roundtext.rectTransform.anchoredPosition = _originVector;
			yield return new WaitForSeconds(1f);
			_roundtext.gameObject.SetActive(false);
		}
		private void RoundEnd()
		{
			CoroutineStop();
			_coroutine = StartCoroutine(RoundEndText($"KO"));
			CountImageSetting();
			_upperHud.gameObject.SetActive(false);
			_frameImage.gameObject.SetActive(true);
		}
		private IEnumerator RoundEndText(string text)
		{
			yield return null;
			_roundtext.rectTransform.DOKill();
			_roundtext.gameObject.SetActive(true);
			_roundtext.text = text;
			_roundtext.fontMaterial = _originMaterial;
			_roundtext.rectTransform.localScale = Vector3.one * 2;
			_roundtext.rectTransform.DOScale(4f, 1f).OnComplete(() => _roundtext.gameObject.SetActive(false));
		}
		private void RoundReady()
		{
			CoroutineStop();
			_coroutine = StartCoroutine(SetText($"Ready"));
		}
		private void GameEnd()
		{
			CoroutineStop();
			_coroutine = StartCoroutine(RoundEndText($"Game Set"));
			CountImageSetting();
			_upperHud.gameObject.SetActive(false);
			_frameImage.gameObject.SetActive(true);
		}
		private IEnumerator SetText(string str)
		{
			_roundtext.fontMaterial = _negativeMaterial;
			yield return new WaitForSeconds(0.02f);
			_roundtext.fontMaterial = _originMaterial;

			_roundtext.gameObject.SetActive(true);
			_roundtext.rectTransform.DOKill();
			_roundtext.rectTransform.localScale = Vector3.zero;

			_roundtext.text = str;

			_roundtext.rectTransform.DOScale(4f, 1f).OnComplete(() => _roundtext.gameObject.SetActive(false));
		}
		private void CountImageSetting()
		{
			for (int i = 0; i < _roundManager.WinCountP1; ++i)
			{
				_roundCountP1[i].sprite = _winSprite;
			}

			for (int i = 0; i < _roundManager.WinCountP2; ++i)
			{
				_roundCountP2[i].sprite = _winSprite;
			}
		}
		private void CoroutineStop()
		{
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}
		}

	}

}