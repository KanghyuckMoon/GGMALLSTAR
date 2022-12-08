using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialManager : MonoBehaviour
{
	private int tutorialStep = 0;
	public int TutorialStrp => tutorialStep;

	[SerializeField] private TutorialStepSO tutorialStepSO;
	[SerializeField] private TextMeshProUGUI tutorialText;

	private void Start()
	{
		SetText();
		StartCoroutine(UpdateTutorial());
		StartCoroutine(SetHP());
	}

	public void NextStep()
	{
		++tutorialStep;
		SetText();
	}

	private void SetText()
	{
		tutorialText.text = $"{tutorialStepSO.stepText[tutorialStep]}";
	}

	private IEnumerator SetHP()
	{
		TutorialSpawner tutorialSpawner = FindObjectOfType<TutorialSpawner>();
		Character characterP1 = tutorialSpawner.Player1.GetComponent<Character>();
		CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>();
		CharacterLevel characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>();
		Character characterP2 = tutorialSpawner.Player2.GetComponent<Character>();
		CharacterStat characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>();
		CharacterLevel characterLevelP2 = characterP2.GetCharacterComponent<CharacterLevel>();
		characterLevelP2.AddExp(1000);
		while (true)
		{
			yield return null;
			characterStatP1.SetHP(characterStatP1.MaxHP);
			characterStatP2.SetHP(characterStatP2.MaxHP);
		}
	}

	private IEnumerator UpdateTutorial()
	{
		TutorialSpawner tutorialSpawner = FindObjectOfType<TutorialSpawner>();
		Character characterP1 = tutorialSpawner.Player1.GetComponent<Character>();
		CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>();
		CharacterLevel characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>();
		while (tutorialStep < 11)
		{
			yield return new WaitForSeconds(5f);
			NextStep();
			switch (tutorialStep)
			{
				case 5:
				case 7:
				case 9:
					characterLevelP1.AddExp(100);
				break;
			}
		}
		Loading.LoadingScene.Instance.LoadScene("Main", Loading.LoadingScene.LoadingSceneType.Normal);
	}

}
