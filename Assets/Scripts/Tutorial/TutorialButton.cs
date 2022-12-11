using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
	public void SetTutorial()
	{
		SelectDataSO.isTutorial = true;
		SelectDataSO.characterSelectP1 = CharacterSelect.Jaeby;
		SelectDataSO.characterSelectP2 = CharacterSelect.Frog;
	}
}
