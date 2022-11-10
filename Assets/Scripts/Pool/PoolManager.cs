using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Addressable;

namespace Pool
{
    public static class PoolManager
    {
        public static Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
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
                Debug.Log(e.ToString());

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
            pool[name].Enqueue(obj);
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
                Queue<GameObject> q = pool[name];

                if (q.Count == 0)
                {  //???¡Æ ???????? ??? ?????????
                    GameObject prefab = prefabDictionary[name];
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
                GameObject prefab = prefabDictionary[name];
                GameObject g = GameObject.Instantiate(prefab);
                item = g;
            }
            return item;
        }
    }

}