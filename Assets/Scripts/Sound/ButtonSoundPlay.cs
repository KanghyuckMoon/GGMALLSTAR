using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sound
{
    /// <summary>
    /// ��ư ȿ���� �����
    /// </summary>
    public class ButtonSoundPlay : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("highlightedeffName"), Header("���콺�� �÷��� �� ȿ����")]
        private string _highlightedeffName;

        [SerializeField, FormerlySerializedAs("clickEffName"), Header("Ŭ������ �� ȿ����")]
        private string _clickEffName;

        /// <summary>
        /// ���콺�� �÷��� �� ȿ���� ���
        /// </summary>
        public void HighlightedSound()
		{
            SoundManager.Instance.PlayEFF(_highlightedeffName);
        }

        /// <summary>
        /// Ŭ������ �� ȿ���� ���
        /// </summary>
        public void ClickSound()
        {
            SoundManager.Instance.PlayEFF(_clickEffName);
        }
    }

}