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
        private Dictionary<EffectType, Transform> _effectParents = new Dictionary<EffectType, Transform>();
        private Dictionary<EffectType, GameObject> _effectPrefebs = new Dictionary<EffectType, GameObject>();
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
            SceneManager.sceneLoaded += LoadedsceneEvent;

            int count = (int)EffectType.Count;
            for (int i = 0; i < count; ++i)
            {
                GameObject obj = new GameObject(((EffectType)i).ToString());
                DontDestroyOnLoad(obj);
                _effectParents.Add((EffectType)i, obj.transform);
                _effectPrefebs.Add((EffectType)i, AddressablesManager.Instance.GetResource<GameObject>(((EffectType)i).ToString()));

            }
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
		/// �޺�ī��Ʈ����Ʈ
		/// </summary>
		/// <param name="count"></param>
		/// <param name="stunTime"></param>
		/// <param name="pos"></param>
		public void SetComboCountEffect(int count, float stunTime, Vector3 pos)
		{
            PoolManager.GetItem("ComboCountEff").GetComponent<ComboCountEffect>().SetComboCount(count, stunTime, pos);
        }

        /// <summary>
        /// �� �̵��� �����Ͽ� ����Ʈ ����
        /// </summary>
        public void MoveSceneToDeleteEffect()
        {
            int count = (int)EffectType.Count;
            for (int i = 0; i < count; ++i)
            {
                Transform parent = _effectParents[(EffectType)i];
                int childCount = parent.childCount;
                for (int j = 0; j < childCount; ++j)
                {
                    parent.GetChild(j).gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// ����Ʈ Ǯ��
        /// </summary>
        /// <param name="transform"></param>
        private GameObject Pool(EffectType effectType)
        {
            Transform parent = _effectParents[effectType];
            int count = parent.childCount;
            for (int i = 0; i < count; ++i)
            {
                GameObject effect = parent.GetChild(i).gameObject;
                if (!effect.activeSelf)
                {
                    return effect;
                }
            }

            GameObject newEffect = Instantiate(_effectPrefebs[effectType], parent);
            return newEffect;
        }

        private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
        {
            MoveSceneToDeleteEffect();
        }
    }

}