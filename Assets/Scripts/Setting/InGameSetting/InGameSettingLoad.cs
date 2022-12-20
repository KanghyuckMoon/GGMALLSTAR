using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Setting
{
    /// <summary>
    /// InGameSceneÀ» ºÒ·¯¿È
    /// </summary>
    public class InGameSettingLoad : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene("InGameSetting", LoadSceneMode.Additive);
        }
    }

}