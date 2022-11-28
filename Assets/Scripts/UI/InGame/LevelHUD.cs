using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelHUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelTextP1;
	[SerializeField] private TextMeshProUGUI levelTextP2;

	[SerializeField] private TextMeshProUGUI expTextP1;
	[SerializeField] private TextMeshProUGUI expTextP2;

	private CharacterLevel characterLevelP1;
	private CharacterLevel characterLevelP2;

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
		levelTextP1.text = $"{characterLevelP1.Level}";
	}
	private void ChangeExpHUDP1()
	{
		expTextP1.text = $"{characterLevelP1.Exp}";
	}

	private void ChangeLevelHUDP2()
	{
		levelTextP2.text = $"{characterLevelP2.Level}";
	}
	private void ChangeExpHUDP2()
	{
		expTextP2.text = $"{characterLevelP2.Exp}";
	}
}
