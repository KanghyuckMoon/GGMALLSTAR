using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class EFFPlayer : MonoBehaviour
    {
        [SerializeField] private string effName;

		private void Start()
		{
            PlayEFF();
        }

		private void PlayEFF()
        {
            SoundManager.Instance.PlayEFF(effName);
        }
    }

}