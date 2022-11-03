using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class BGMPlayer : MonoBehaviour
    {
        [SerializeField]
        private bool _isPlayerOnAwake = false;
        [SerializeField]
        private AudioBGMType _audioBGMType = AudioBGMType.Count;

        void Start()
        {
            if (_isPlayerOnAwake)
            {
                PlayBGM();
            }

        }

        private void PlayBGM()
        {
            SoundManager.Instance.PlayBGM(_audioBGMType);
        }
    }
}
