using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill;
using Pool;
using Addressable;

namespace Effect
{

    public class EffectManager : MonoSingleton<EffectManager>
    {
        private bool _isInit = false;

        public void Start()
        {
            if (!_isInit)
            {
                if (Instance == this)
                {
                    Init();
                }
                else
                {
                    Instance.Init();
                }
            }
        }


        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        private void Init()
        {
            if (_isInit)
            {
                return;
            }
            _isInit = true;
        }

        /// <summary>
        /// ����Ʈ ��ġ
        /// </summary>
        /// <param name="pos"></param>
        public void SetEffect(EffectType effectType, Vector3 pos, EffectDirectionType effectDirectionType = EffectDirectionType.Identity, Vector3 direction = default)
        {
            if (!_isInit)
            {
                Init();
            }

            GameObject effect = Pool(effectType);
            effect.transform.position = pos;
            effect.gameObject.SetActive(true);

			switch (effectDirectionType)
			{
                case EffectDirectionType.ReverseDirection:
                    effect.transform.eulerAngles = direction;
                    break;
                case EffectDirectionType.SetParticle3DRotation:
                    effect.GetComponentInChildren<ParticleSystem>().startRotation3D = direction;
                    break;
			}
		}

        /// <summary>
        /// ����Ʈ Ǯ��
        /// </summary>
        /// <param name="transform"></param>
        private GameObject Pool(EffectType effectType)
        {
            return PoolManager.GetItem(effectType.ToString());
        }
    }

}