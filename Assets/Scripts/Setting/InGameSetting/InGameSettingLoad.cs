using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Setting
{
    public class InGameSettingLoad : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SceneManager.LoadScene("InGameSetting", LoadSceneMode.Additive);
        }
    }

}