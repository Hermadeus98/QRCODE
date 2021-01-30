using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine;

using QRTools.Singletons;

namespace QRTools
{
    public class PoolingManager : MonoBehaviourSingleton<PoolingManager>
    {
        private Dictionary<Type, List<IPoolable>> poolables;
        [SerializeField] bool expandablePool = true;

        private void Awake()
        {
            Init();
        }

        /// <summary>
        /// Initialise poolables on a dictionary.
        /// </summary>
        void Init()
        {
            poolables = new Dictionary<Type, List<IPoolable>>();
            var _poolables = FindObjectsOfType<MonoBehaviour>().OfType<IPoolable>().ToArray();
            for (int i = 0; i < _poolables.Length; i++)
            {
                TypeExisting(_poolables[i].GetType());
                poolables.TryGetValue(_poolables[i].GetType(), out var poolObjs);
                poolObjs.Add(_poolables[i]);
                ((MonoBehaviour)_poolables[i]).gameObject.SetActive(false);
            }
        }

        public IPoolable Pool<T>() where T : MonoBehaviour => Pool<T>(Vector3.zero, Quaternion.identity);

        public IPoolable Pool<T>(Vector3 position) where T : MonoBehaviour => Pool<T>(position, Quaternion.identity);

        /// <summary>
        /// Use this function to pull an object.
        /// </summary>
        public IPoolable Pool<T>(Vector3 position, Quaternion rotation) where T: MonoBehaviour
        {
            if (poolables == null || poolables.Count == 0)
                throw new Exception("Pool is empty.");

            for (int i = 0; i < poolables[typeof(T)].Count; i++)
            {
                if (!((MonoBehaviour)poolables[typeof(T)][i]).isActiveAndEnabled)
                {
                    MonoBehaviour poolable = poolables[typeof(T)][i] as MonoBehaviour;
                    poolable.gameObject.SetActive(true);
                    poolable.transform.position = position;
                    poolable.transform.rotation = rotation;
                    ((IPoolable)poolable).OnPool();

                    return poolable as IPoolable;
                }
            }

            if (expandablePool)
            {
                MonoBehaviour poolable = poolables[typeof(T)][0] as MonoBehaviour;
                IPoolable newObj = Instantiate(poolable, position, rotation, poolable.transform.parent) as IPoolable;
#if UNITY_EDITOR
                ((MonoBehaviour)newObj).name = poolable.name;
#endif
                newObj.OnPool();
                poolables.TryGetValue(newObj.GetType(), out var poolObjs);
                poolObjs.Add(newObj as IPoolable);
                return newObj;
            }

            Debug.LogError(string.Format(
                "There not enought {0} in pool.", poolables[typeof(T)][0].GetType().ToString()));

            return null;
        }

        public void Push(IPoolable poolable)
        {
            poolable.OnPush();
            ((MonoBehaviour)poolable).gameObject.SetActive(false);
        }

        bool TypeExisting(Type type)
        {
            if (!poolables.ContainsKey(type))
            {
                poolables.Add(type, new List<IPoolable>());
                return false;
            }
            return true;
        }
    }
}