using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
	public class TutorialButton : MonoBehaviour
	{
		/// <summary>
		/// Ʃ�丮�� ������ ����
		/// </summary>
		public void SetTutorial()
		{
			SelectDataSO.isTutorial = true;
			SelectDataSO.characterSelectP1 = CharacterSelect.Jaeby;
			SelectDataSO.characterSelectP2 = CharacterSelect.Frog;
		}
	}

}
