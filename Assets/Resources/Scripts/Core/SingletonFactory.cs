using System;
using System.Collections.Generic;
using UnityEngine;

public static class SingletonFactory
{
    static Dictionary<Type, MonoBehaviour> singletonDic = new Dictionary<Type, MonoBehaviour>();

    public static T Get<T>() where T : MonoBehaviour
    {
        // 딕셔너리에서 찾음
        if (singletonDic.TryGetValue(typeof(T), out MonoBehaviour cache))
        {
            return cache as T;
        }

        // 못찾으면 씬에서 찾음
        T instance = GameObject.FindAnyObjectByType<T>();
        if (instance == null)
        {
            // 못찾으면 새로 만듬
            GameObject go = new GameObject(typeof(T).Name);
            instance = go.AddComponent<T>();
        }

        // 딕셔너리에 등록
        singletonDic.Add(typeof(T), instance);
        return instance;
    }
}