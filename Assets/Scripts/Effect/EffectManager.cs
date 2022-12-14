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
        /// 초기화
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
        /// 이펙트 설치
        /// </summary>
        /// <param name="pos"></param>
        public void SetEffect(EffectType effectType, Vector3 pos, EffectDirectionType effectDirectionType = EffectDirectionType.Identity, Vector3 atkEffectOffset = default, bool isRight = true)
        {
            if (!_isInit)
            {
                Init();
            }

            GameObject effect = Pool(effectType);
            effect.transform.position = pos + atkEffectOffset;

            Vector3 dir = Vector3.zero; 

            switch (effectDirectionType)
			{
                case EffectDirectionType.ReverseDirection:
                    dir = new Vector3(0, 0, isRight ? 180 : 0);
                    effect.transform.eulerAngles = dir;
                    break;
                case EffectDirectionType.SetParticle3DRotation:
                    effect.GetComponentInChildren<ParticleSystem>().startRotation3D = dir;
                    break;
                case EffectDirectionType.SetParticleOffsetIs3DRotation:
                    effect.transform.position -= atkEffectOffset;
                    effect.GetComponentInChildren<ParticleSystem>().startRotation3D = atkEffectOffset;
                    break;
                case EffectDirectionType.OriginDirection:
                    dir = new Vector3(0, 0, isRight ? 0 : 180);
                    effect.transform.eulerAngles = dir;
                    break;
            }
            effect.gameObject.SetActive(true);
        }

        /// <summary>
        /// 이펙트 풀링
        /// </summary>
        /// <param name="transform"></param>
        private GameObject Pool(EffectType effectType)
        {
            return PoolManager.GetItem(effectType.ToString());
        }
    }

}