using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    public class ServiceManager
    {
        readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
        public IEnumerable<object> RegisteredServices => services.Values;

        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out object obj))
            {
                service = obj as T;
                return true;
            }

            service = null;
            return false;
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out object obj))
            {
                return obj as T;
            }

            throw new ArgumentException($"ServiceManager.Get: 해당 서비스 {type.FullName} 는 등록되지 않았습니다");
        }

        public ServiceManager Register<T>(T service)
        {
            Type type = typeof(T);

            if (!services.TryAdd(type, service))
            {
                Debug.LogError($"ServiceManager.Register: 해당 서비스 {type.FullName} 는 이미 등록되어 있습니다");
            }

            // 메서드 체이닝을 위해 자기자신을 다시 반환
            return this;
        }

        public ServiceManager Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
            {
                throw new ArgumentException("서비스 타입이 서비스 interface 타입과 일치하지 않습니다", nameof(service));
            }

            if (!services.TryAdd(type, service))
            {
                Debug.LogError($"ServiceManager.Register: 해당 서비스 {type.FullName} 는 이미 등록되어 있습니다");
            }

            return this;
        }
    }
}