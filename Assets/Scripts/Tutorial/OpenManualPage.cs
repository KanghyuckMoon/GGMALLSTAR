using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
	public class OpenManualPage : MonoBehaviour
	{
		private const string _tutorialPage = "https://south-boot-7be.notion.site/GGMALLSTAR-5b56144548ef4bcfadb1e544d11c35c5";

		/// <summary>
		/// 메뉴얼 링크 열기
		/// </summary>
		public void OpenManual()
		{
			Application.OpenURL(_tutorialPage);
		}

	}

}
