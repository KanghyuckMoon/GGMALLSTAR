using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

namespace Loading
{
    /// <summary>
    /// 로딩중 브금 초기화와 풀매니저 데이터 제거
    /// </summary>
    public class LoadingMemoryClean : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Sound.SoundManager.Instance.SetBGMSpeed(1.0f);
            PoolManager.DeleteAllPool();
        }
    }
}
