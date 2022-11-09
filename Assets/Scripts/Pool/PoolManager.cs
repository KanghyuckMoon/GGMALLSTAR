using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Addressable;

namespace Pool
{
    public static class PoolManager
    {
        public static Dictionary<string, object> pool = new Dictionary<string, object>();
        public static Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();


        private static void CreatePool(string name)
        {
            Queue<GameObject> q = new Queue<GameObject>();
            GameObject prefab = AddressablesManager.Instance.GetResource<GameObject>(name);

            try
            {
                pool.Add(name, q);
                prefabDictionary.Add(name, prefab.gameObject);
            }
            catch (ArgumentException e)
            {
                pool.Clear();
                prefabDictionary.Clear();
                pool.Add(name, q);
                prefabDictionary.Add(name, prefab.gameObject);
            }
        }

        public static void AddObjToPool(string name, GameObject obj)
        {
            if (!pool.ContainsKey(name))
			{
                CreatePool(name);
			}
            ((Queue<GameObject>)pool[name]).Enqueue(obj);
        }

        public static void DeleteAllPool()
		{
            pool.Clear();
		}

        public static GameObject GetItem(string name)
        {
            GameObject item = null;

            if (!prefabDictionary.ContainsKey(name))
			{
                CreatePool(name);
			}

			if (pool.ContainsKey(name))
			{
				Queue<GameObject> q = (Queue<GameObject>)pool[name];

				if (q.Count == 0)
				{  //첫번째 아이템이 이미 사용중이라면
					GameObject prefab = prefabDictionary[name];
					GameObject g = GameObject.Instantiate(prefab);
                    item = g;
				}
				else
				{
					item = q.Dequeue();
				}

                ((GameObject)item).SetActive(true);
            }
            else
            {
                GameObject prefab = prefabDictionary[name];
                GameObject g = GameObject.Instantiate(prefab);
                item = g;
            }

			return item;
        }
    }

}