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
		Character characterP2 = tutorialSpawner.Player2.GetComponent<Character>();
		CharacterStat characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>();
		while (true)
		{
			yield return null;
			characterStatP2.SetHP(characterStatP2.MaxHP);
		}
	}

	private IEnumerator UpdateTutorial()
	{
		while (tutorialStep < 12)
		{
			yield return new WaitForSeconds(5f);
			NextStep();
		}
		Loading.LoadingScene.Instance.LoadScene("Main", Loading.LoadingScene.LoadingSceneType.Normal);
	}

}
