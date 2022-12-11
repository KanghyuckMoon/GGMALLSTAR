using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Effect;
using DG.Tweening;

public class LevelHUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelTextP1;
	[SerializeField] private TextMeshProUGUI levelTextP2;

	[SerializeField] private Image expImageP1;
	[SerializeField] private Image expImageP2;

	private CharacterLevel characterLevelP1;
	private CharacterLevel characterLevelP2;

	public RectTransform LevelTextP1
	{
		get
		{
			return levelTextP1.rectTransform;
		}
	}
	public RectTransform LevelTextP2
	{
		get
		{
			return levelTextP2.rectTransform;
		}
	}

	private void Start()
	{
		CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
		var characterP1 = characterSpawner.Player1.GetComponent<Character>();
		var characterP2 = characterSpawner.Player2.GetComponent<Character>();

		characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>();
		characterLevelP2 = characterP2.GetCharacterComponent<CharacterLevel>();

		characterLevelP1.AddChangeExpEvent(ChangeExpHUDP1);
		characterLevelP1.AddChangeLevelEvent(ChangeLevelHUDP1);

		characterLevelP2.AddChangeExpEvent(ChangeExpHUDP2);
		characterLevelP2.AddChangeLevelEvent(ChangeLevelHUDP2);

		ChangeLevelHUDP1();
		ChangeLevelHUDP2();
		ChangeExpHUDP1();
		ChangeExpHUDP2();
	}

	private void ChangeLevelHUDP1()
	{
		if(characterLevelP1.Level == 4)
		{
			levelTextP1.text = "ALL STAR";
		}
		else
		{
			levelTextP1.text = $"{characterLevelP1.Level}STAR";
		}
	}
	private void ChangeExpHUDP1()
	{
		if(characterLevelP1.Level == 4)
		{
			expImageP1.fillAmount = 1f;
		}
		else
		{
			expImageP1.fillAmount = ((float)characterLevelP1.Exp - characterLevelP1.PreviouseExp) / characterLevelP1.NeedExp;
		}
	}

	private void ChangeLevelHUDP2()
	{
		if (characterLevelP2.Level == 4)
		{
			levelTextP2.text = "ALL STAR";
			EffectManager.Instance.SetEffect(EffectType.Hit_1, expImageP1.rectTransform.position);
		}
		else
		{
			levelTextP2.text = $"{characterLevelP2.Level}STAR";
		}
	}
	private void ChangeExpHUDP2()
	{
		if (characterLevelP2.Level < 4)
		{
			expImageP2.fillAmount = ((float)characterLevelP2.Exp - characterLevelP2.PreviouseExp) / characterLevelP2.NeedExp;
		}
		else
		{
			expImageP2.fillAmount = 1f;
		}
	}
}
