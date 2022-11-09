using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
	public class TestEFF : MonoBehaviour
	{
		public string effName;

		[ContextMenu("PlayEFF")]
		public void PlayEFF()
		{
			SoundManager.Instance.PlayEFF(effName);
		}

	}

}