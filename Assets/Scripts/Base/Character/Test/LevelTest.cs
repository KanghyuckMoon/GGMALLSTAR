using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LevelTest : MonoBehaviour
{
	private CharacterLevel characterLevel;
	public CharacterLevelSO characterLevelSO;

	private UnityEvent changeExp = new UnityEvent();
	private UnityEvent changeLevel = new UnityEvent();

	public TextMeshProUGUI expText;
	public TextMeshProUGUI levelText;

	private void Start()
	{
		changeLevel.AddListener(ChangeLevel);
		changeExp.AddListener(ChangeExp);
		//characterLevel = new CharacterLevel(characterLevelSO, changeExp, changeLevel);

		ChangeLevel();
		ChangeExp();


	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			characterLevel.AddExp(10);
		}
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			characterLevel.ReturnPreviousLevel();
		}
	}

	private void ChangeLevel()
	{
		levelText.text = $"Level : {characterLevel.Level}";
	}
	private void ChangeExp()
	{
		expText.text = $"EXP : {characterLevel.Exp}";
	}
}
