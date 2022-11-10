using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class ButtonSoundPlay : MonoBehaviour
    {
        public string highlightedeffName;
        public string clickEffName;

        public void HighlightedSound()
		{
            SoundManager.Instance.PlayEFF(highlightedeffName);
        }
        public void ClickSound()
        {
            SoundManager.Instance.PlayEFF(clickEffName);
        }
    }

}