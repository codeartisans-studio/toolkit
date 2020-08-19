using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [Serializable]
    public struct PreloadedPrefab
    {
        public GameObject prefab;
        public int count;
    }

    [AddComponentMenu("Toolkit/Controllers/ObjectPoolController")]
    public class ObjectPoolController : Singleton<ObjectPoolController>
    {
        public PreloadedPrefab[] preloadedPrefabs;

        // 记录原预制instance和原预制对象
        private static Dictionary<int, GameObject> originalObjects = new Dictionary<int, GameObject>();
        // 记录生成对象instanceID和原预制的instanceID
        private static Dictionary<int, int> instantiatedObjects = new Dictionary<int, int>();
        // 记录预制instanceID和对象栈
        private static Dictionary<int, Stack<GameObject>> gameObjectPool = new Dictionary<int, Stack<GameObject>>();

        // Start is called before the first frame update
        void Start()
        {
            if (preloadedPrefabs != null)
            {
                List<GameObject> list = new List<GameObject>();
                foreach (PreloadedPrefab preloadedPrefab in preloadedPrefabs)
                {
                    if (preloadedPrefab.prefab != null && preloadedPrefab.count > 0)
                    {
                        for (int i = 0; i < preloadedPrefab.count; i++)
                        {
                            list.Add(Instantiate(preloadedPrefab.prefab));
                        }

                        foreach (GameObject gameObject in list)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }

        private static GameObject GetObjectFromPool(int originalInstanceID, Vector3 position, Quaternion rotation, Transform parent)
        {
            Stack<GameObject> stack;
            if (gameObjectPool.TryGetValue(originalInstanceID, out stack))
            {
                while (stack.Count > 0)
                {
                    GameObject gameObject = stack.Pop();
                    if (gameObject != null)
                    {
                        gameObject.transform.position = position;
                        gameObject.transform.rotation = rotation;
                        gameObject.transform.SetParent(parent);
                        gameObject.SetActive(true);
                        return gameObject;
                    }
                }
            }
            return null;
        }

        private static void RemoveObjectFromPool(GameObject instantiatedObject, int originalInstanceID)
        {
            instantiatedObject.SetActive(false);
            instantiatedObject.transform.SetParent(Instance().transform);

            Stack<GameObject> stack;
            if (gameObjectPool.TryGetValue(originalInstanceID, out stack))
            {
                stack.Push(instantiatedObject);
            }
            else
            {
                stack = new Stack<GameObject>();
                stack.Push(instantiatedObject);
                gameObjectPool.Add(originalInstanceID, stack);
            }
        }

        public static bool IsPooledObject(GameObject instantiatedObject)
        {
            int instanceID = instantiatedObject.GetInstanceID();
            return instantiatedObjects.ContainsKey(instanceID);
        }

        public static int OriginalInstanceID(GameObject instantiatedObject)
        {
            int instanceID = instantiatedObject.GetInstanceID();
            int originalInstanceID;

            if (!instantiatedObjects.TryGetValue(instanceID, out originalInstanceID))
            {
                Debug.LogError(string.Format("Unable to get the original instance ID of {0}: has the object been placed in the ObjectPool?", instantiatedObject));
                return -1;
            }

            return originalInstanceID;
        }

        public static GameObject OriginalObject(GameObject instantiatedObject)
        {
            int instanceID = instantiatedObject.GetInstanceID();
            int originalInstanceID;

            if (!instantiatedObjects.TryGetValue(instanceID, out originalInstanceID))
                return null;

            GameObject originalObject;
            if (!originalObjects.TryGetValue(originalInstanceID, out originalObject))
                return null;

            return originalObject;
        }

        public static GameObject Instantiate(GameObject original)
        {
            return Instantiate(original, Vector3.zero, Quaternion.identity);
        }

        public static GameObject Instantiate(GameObject original, Transform parent)
        {
            return Instantiate(original, Vector3.zero, Quaternion.identity, parent);
        }

        public static GameObject Instantiate(GameObject original, Transform parent, bool instantiateInWorldSpace)
        {
            // 因为SetParent默认true会保留世界坐标位置
            if (instantiateInWorldSpace)
                // 在世界坐标的原点位置生成
                return Instantiate(original, Vector3.zero, Quaternion.identity, parent);
            else
                // 在世界坐标父对象原点位置生成
                return Instantiate(original, parent.position, parent.rotation, parent);
        }

        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation)
        {
            return Instantiate(original, position, rotation, null);
        }

        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            int originalInstanceID = original.GetInstanceID();
            GameObject gameObject = GetObjectFromPool(originalInstanceID, position, rotation, parent);
            if (gameObject == null)
            {
                gameObject = UnityEngine.Object.Instantiate(original, position, rotation, parent);
                if (!originalObjects.ContainsKey(originalInstanceID))
                    originalObjects.Add(originalInstanceID, original);
            }
            else
            {
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
                gameObject.transform.SetParent(parent);
            }

            instantiatedObjects.Add(gameObject.GetInstanceID(), originalInstanceID);

            return gameObject;
        }

        public static void Destroy(GameObject instantiatedObject)
        {
            int instanceID = instantiatedObject.GetInstanceID();
            int originalInstanceID;
            if (!instantiatedObjects.TryGetValue(instanceID, out originalInstanceID))
            {
                Debug.LogError(string.Format("Unable to pool {0} (instance {1}): the GameObject was not instantiated with ObjectPool.Instantiate.", instantiatedObject.name, instanceID));
                return;
            }

            instantiatedObjects.Remove(instanceID);

            RemoveObjectFromPool(instantiatedObject, originalInstanceID);
        }
    }
}
