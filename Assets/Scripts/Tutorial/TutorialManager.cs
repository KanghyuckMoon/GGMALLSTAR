using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using System;

namespace Tutorial
{
	public class TutorialManager : MonoBehaviour
	{
		public int TutorialStrp => _tutorialStep;
		
		private int _tutorialStep = 0;


		[SerializeField, FormerlySerializedAs("tutorialStepSO")] 
		private TutorialStepSO _tutorialStepSO;
		[SerializeField, FormerlySerializedAs("tutorialText")] 
		private TextMeshProUGUI _tutorialText;

		private void Start()
		{
			SetText();
			StartCoroutine(UpdateTutorial());
			StartCoroutine(SetHP());
		}

		/// <summary>
		/// 다음 튜토리얼 표시
		/// </summary>
		public void NextStep()
		{
			++_tutorialStep;
			SetText();
		}

		private void SetText()
		{
			_tutorialText.text = $"{_tutorialStepSO.stepText[_tutorialStep]}";
		}

		private IEnumerator SetHP()
		{
			TutorialSpawner tutorialSpawner = FindObjectOfType<TutorialSpawner>();
			Character characterP1 = tutorialSpawner.Player1.GetComponent<Character>();
			CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
			CharacterLevel characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
			Character characterP2 = tutorialSpawner.Player2.GetComponent<Character>();
			CharacterStat characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
			CharacterLevel characterLevelP2 = characterP2.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
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
			CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
			CharacterLevel characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
			while (_tutorialStep < 11)
			{
				yield return new WaitForSeconds(5f);
				NextStep();
				switch (_tutorialStep)
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

}