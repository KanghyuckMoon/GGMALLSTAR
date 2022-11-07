using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loading
{
    public class QtoT : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            LoadingScene.Instance.LoadScene("Training");
        }
    }

}