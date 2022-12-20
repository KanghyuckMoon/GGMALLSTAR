using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Addressable;

namespace Pool
{
    public static class PoolManager
    {
        private static Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();
        private static Dictionary<string, GameObject> _prefabDictionary = new Dictionary<string, GameObject>();
        private static List<string> _nameList = new List<string>();

        /// <summary>
        /// 풀 생성
        /// </summary>
        /// <param name="name"></param>
        public static void CreatePool(string name)
        {
            if (_pool.ContainsKey(name))
            {
                return;
            }

            Queue<GameObject> q = new Queue<GameObject>();
            GameObject prefab = AddressablesManager.Instance.GetResource<GameObject>(name);

            try
            {
                _pool.Add(name, q);
                _nameList.Add(name);
                _prefabDictionary.Add(name, prefab.gameObject);
            }
            catch (ArgumentException e)
            {
                Debug.Log(e.ToString());

                _pool.Clear();
                _prefabDictionary.Clear();
                _pool.Add(name, q);
                _nameList.Add(name);
                _prefabDictionary.Add(name, prefab.gameObject);
            }
        }

        /// <summary>
        /// 풀에 오브젝트 넣기
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public static void AddObjToPool(string name, GameObject obj)
        {
            if (!_pool.ContainsKey(name))
            {
                CreatePool(name);
            }
            _pool[name].Enqueue(obj);
        }

        /// <summary>
        /// 풀과 풀에 있는 모든 오브젝트 제거
        /// </summary>
        public static void DeleteAllPool()
        {
            for (int i = 0; i < _nameList.Count; ++i)
            {
                var q = _pool[_nameList[i]];
                while (q.Count > 0)
				{
                    GameObject.Destroy(q.Dequeue());
                }
            }
            GC.Collect();
        }

        /// <summary>
        /// 오브젝트 가져오기
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetItem(string name)
        {
            GameObject item = null;

            if (!_prefabDictionary.ContainsKey(name))
            {
                CreatePool(name);
            }

            if (_pool.ContainsKey(name))
            {
                Queue<GameObject> q = _pool[name];

                if (q.Count == 0)
                {  //???° ???????? ??? ?????????
                    GameObject prefab = _prefabDictionary[name];
                    GameObject g = GameObject.Instantiate(prefab);
                    item = g;
                }
                else
                {
                    item = q.Dequeue();
                }
                item.gameObject.SetActive(true);
            }
            else
            {
                GameObject prefab = _prefabDictionary[name];
                GameObject g = GameObject.Instantiate(prefab);
                item = g;
            }
            return item;
        }
    }

}