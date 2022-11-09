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


        private static void CreatePool<T>(string name) where T : MonoBehaviour
        {
            Queue<T> q = new Queue<T>();
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

        public static void AddObjToPool<T>(string name, T obj) where T : MonoBehaviour
        {
            if (!pool.ContainsKey(name))
            {
                CreatePool<T>(name);
            }
            ((Queue<T>)pool[name]).Enqueue(obj);
        }

        public static void DeleteAllPool()
        {
            pool.Clear();
        }

        public static T GetItem<T>(string name) where T : MonoBehaviour
        {
            T item = null;

            if (!prefabDictionary.ContainsKey(name))
            {
                CreatePool<T>(name);
            }

            if (pool.ContainsKey(name))
            {
                Queue<T> q = (Queue<T>)pool[name];

                if (q.Count == 0)
                {  //ù��° �������� �̹� ������̶��
                    GameObject prefab = prefabDictionary[name];
                    GameObject g = GameObject.Instantiate(prefab);
                    item = g.GetComponent<T>();
                }
                else
                {
                    item = q.Dequeue();
                }

                IPoolable ipool = item.GetComponent<IPoolable>();

                if (ipool != null)
                {
                    ipool.OnPoolOut();
                }

            }
            else
            {
                GameObject prefab = prefabDictionary[name];
                GameObject g = GameObject.Instantiate(prefab);
                item = g.GetComponent<T>();
            }
            return item;
        }
    }

}