using UnityEngine;
using UnityServiceLocator;
using System.Collections.Generic;

public class ServiceTest : MonoBehaviour
{
    public List<Object> services;
    public Transform transform;
    public ServiceLocator serviceLocator;

    private void Awake()
    {
        serviceLocator = ServiceLocator.For(this);
        // foreach (var service in services)
        //serviceLocator.Register(service.GetType(), service);

        serviceLocator.Get<Transform>(out transform);
        transform = serviceLocator.Get<Transform>();
    }
}