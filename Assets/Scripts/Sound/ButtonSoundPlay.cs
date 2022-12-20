using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sound
{
    /// <summary>
    /// 버튼 효과음 재생기
    /// </summary>
    public class ButtonSoundPlay : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("highlightedeffName"), Header("마우스를 올렸을 때 효과음")]
        private string _highlightedeffName;

        [SerializeField, FormerlySerializedAs("clickEffName"), Header("클릭했을 때 효과음")]
        private string _clickEffName;

        /// <summary>
        /// 마우스를 올렸을 때 효과음 재생
        /// </summary>
        public void HighlightedSound()
		{
            SoundManager.Instance.PlayEFF(_highlightedeffName);
        }

        /// <summary>
        /// 클릭했을 때 효과음 재생
        /// </summary>
        public void ClickSound()
        {
            SoundManager.Instance.PlayEFF(_clickEffName);
        }
    }

}