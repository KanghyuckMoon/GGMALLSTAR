using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

namespace Loading
{
    /// <summary>
    /// �ε��� ��� �ʱ�ȭ�� Ǯ�Ŵ��� ������ ����
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
