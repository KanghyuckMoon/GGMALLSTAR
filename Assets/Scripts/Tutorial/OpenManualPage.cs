using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenManualPage : MonoBehaviour
{
	private const string tutorialPage = "https://south-boot-7be.notion.site/GGMALLSTAR-5b56144548ef4bcfadb1e544d11c35c5";

	public void OpenManual()
	{
		Application.OpenURL(tutorialPage);
	}

}
