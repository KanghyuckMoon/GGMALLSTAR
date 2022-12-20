using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
	public class TutorialButton : MonoBehaviour
	{
		/// <summary>
		/// 튜토리얼 데이터 설정
		/// </summary>
		public void SetTutorial()
		{
			SelectDataSO.isTutorial = true;
			SelectDataSO.characterSelectP1 = CharacterSelect.Jaeby;
			SelectDataSO.characterSelectP2 = CharacterSelect.Frog;
		}
	}

}
