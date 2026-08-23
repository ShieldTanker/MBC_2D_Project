using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityServiceLocator
{
    public class ServiceLocator : MonoBehaviour
    {
        /// <summary> 전역적으로 존재하는 글로벌 로케이터 </summary>
        static ServiceLocator globalLocator;

        /// <summary> 씬 마다 존재하는 로컬 로케이터 목록 </summary>
        static Dictionary<Scene, ServiceLocator> sceneLocalContainers;
        static List<GameObject> tmpSceneGameObjectList;

        readonly ServiceManager _servicesManager = new ServiceManager();

        /// <summary> 글로벌 로케이터 새로 생성시 이름으로 사용 </summary>
        const string k_globalServiceLocatorName = "ServiceLocator [Global]";
        /// <summary> 로컬 로케이터 새로 생성시 이름으로 사용 </summary>
        const string k_sceneServiceLocatorName = "ServiceLocator [Scene]";

        /// <summary> 글로벌 로케이터 등록 </summary> <param name="dontDestroyOnLoad"></param>
        internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
        {
            if (globalLocator == this)
                Debug.LogWarning("글로벌 서비스 로케이터 등록 : 이미 등록된 글로벌 서비스 로케이터 입니다", this);
            
            else if (globalLocator != null)
                Debug.LogError("글로벌 서비스 로케이터 등록 : 이미 글로벌 로케이터가 있습니다", this);
            
            else
            {
                globalLocator = this;
                if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary> 현재 씬을 Key로 로컬 로케이터 목록에 등록 </summary>
        internal void ConfigureForLocal()
        {
            Scene scene = gameObject.scene;

            if (sceneLocalContainers.ContainsKey(scene))
            {
                Debug.LogError("해당 씬에 이미 로케이터가 등록되어 있습니다", this);
                return;
            }

            sceneLocalContainers.Add(scene, this);
        }

        /// <summary> 글로벌 서비스 로케이터 객체를 반환, 없으면 새로 생성해서 반환 </summary>        
        public static ServiceLocator Global
        {
            get
            {
                if (globalLocator != null)
                    return globalLocator;

                // 씬에서 해당 글로벌 로케이터를 찾은뒤 null이 아니면 foundLocator에 넣은뒤 아래 블럭 실행
                if (FindAnyObjectByType<ServiceLocatorGlobal>() is { } foundLocator)
                {
                    foundLocator.BootstrapOnDemand(); // 부트스트랩 요청
                    return globalLocator;
                }

                // 씬에도 없으면 새로 생성
                var locator = new GameObject(k_globalServiceLocatorName, typeof(ServiceLocator));
                locator.AddComponent<ServiceLocatorGlobal>().BootstrapOnDemand();

                return globalLocator;
            }
        }

        /// <summary>
        /// 해당 모노비헤이비어가 있는 씬의 <see cref="ServiceLocator"/>를 반환. 씬에 로컬 로케이터가 없으면 글로벌 로케이터 반환.
        /// </summary>
        public static ServiceLocator ForSceneOfLocal(MonoBehaviour monoBehaviour)
        {
            Scene scene = monoBehaviour.gameObject.scene;

            // 해당 씬에 로컬로케이터가 있고, 해당 로케이터가 요청한 객체와 같지 않으면 반환
            if (sceneLocalContainers.TryGetValue(scene, out ServiceLocator locator) && locator != monoBehaviour)
            {
                return locator;
            }

            // 리스트를 비우고 해당 씬의 부모객체가 없는 객체들을 전부 담음
            tmpSceneGameObjectList.Clear();
            scene.GetRootGameObjects(tmpSceneGameObjectList);

            // 루트 오브젝트들에서 로컬 로케이터컴포넌트가 있는 오브젝트만 foreach문을 돌림
            foreach (GameObject go in tmpSceneGameObjectList.Where(go => go.GetComponent<ServiceLocatorLocal>() != null))
            {
                // 로컬 로케이터 컴포넌트를 가져올 수 있고 해당 로컬 로케이터의 로케이터가 요청한 모노비헤이비어가 아니면 부트스트랩 실행
                if (go.TryGetComponent(out ServiceLocatorLocal bootstrapper) && bootstrapper.Locator != monoBehaviour)
                {
                    bootstrapper.BootstrapOnDemand();
                    return bootstrapper.Locator;
                }
            }

            // 전부 해당 없으면 글로벌 로케이터의 서비스 로케이터 반환
            return Global;
        }

        /// <summary> 제공된 MonoBehaviour의 하이어라키에서 가장 가까운 <see cref="ServiceLocator"/>인스턴스,
        /// <br>(없으면) 해당 씬의 <see cref="ServiceLocator"/>,</br>
        /// <br>(그것도 없으면) 글로벌 <see cref="ServiceLocator"/>를 가져옵니다.</br>
        /// </summary>
        public static ServiceLocator For(MonoBehaviour monoBehavior)
        {
            // null이 아니면 반환 ?? null이면 실행후 반환
            return monoBehavior.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOfLocal(monoBehavior) ?? Global;
        }

        /// <summary> 서비스의 타입을 사용하여 ServiceLocator에 서비스를 등록합니다. </summary>
        /// <param name="service">등록할 서비스.</param>
        /// <typeparam name="T">등록할 서비스의 클래스 타입.</typeparam>
        /// <returns>서비스 등록 후의 ServiceLocator 인스턴스.</returns>
        public ServiceLocator Register<T>(T service)
        {
            _servicesManager.Register(service);
            // 메서드 체이닝을 위해 자기 자신 반환
            return this;
        }

        /// <summary>
        /// 특정 타입을 사용하여 ServiceLocator에 서비스를 등록합니다.
        /// </summary>
        /// <param name="type">등록에 사용할 타입.</param>
        /// <param name="service">등록할 서비스.</param>
        /// <returns>서비스 등록 후의 ServiceLocator 인스턴스.</returns>
        public ServiceLocator Register(Type type, object service)
        {
            _servicesManager.Register(type, service);
            return this;
        }

        /// <summary>
        /// 특정 타입의 서비스를 가져옵니다. 요청한 타입의 서비스를 찾지 못하면 에러(예외)가 발생합니다.
        /// </summary>
        /// <param name="service">가져올 T 타입의 서비스.</param>
        /// <typeparam name="T">가져올 서비스의 클래스 타입.</typeparam>
        /// <returns>서비스 조회를 시도한 후의 ServiceLocator 인스턴스.</returns>
        public ServiceLocator Get<T>(out T service) where T : class
        {
            if (TryGetService(out service)) return this;

            if (TryGetNextInHierarchy(out ServiceLocator container))
            {
                container.Get(out service);
                return this;
            }

            throw new ArgumentException($"ServiceLocator.Get: 서비스 타입 {typeof(T).FullName} 이 등록되어있지 않습니다");
        }

        /// <summary>
        /// 특정 타입의 서비스를 가져올 수 있게 합니다. 요청한 서비스가 존재하지 않으면 에러(예외)가 발생합니다.
        /// </summary>
        /// <typeparam name="T">가져올 서비스의 클래스 타입.</typeparam>
        /// <returns>T 타입 서비스의 인스턴스.</returns>
        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            T service = null;

            if (TryGetService(type, out service)) return service;

            if (TryGetNextInHierarchy(out ServiceLocator container))
                return container.Get<T>();

            throw new ArgumentException($"해결할 수 없는 유형 '{typeof(T).FullName}'.");
        }

        /// <summary>
        /// 특정 타입의 서비스를 가져오는 것을 시도합니다. 조회 성공 여부를 반환합니다.
        /// </summary>
        /// <param name="service">가져올 T 타입의 서비스.</param>
        /// <typeparam name="T">가져올 서비스의 클래스 타입.</typeparam>
        /// <returns>서비스 조회에 성공했으면 true, 그렇지 않으면 false.</returns>
        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            service = null;

            if (TryGetService(type, out service))
                return true;

            return TryGetNextInHierarchy(out ServiceLocator container) && container.TryGet(out service);
        }

        bool TryGetService<T>(out T service) where T : class
        {
            return _servicesManager.TryGet(out service);
        }

        bool TryGetService<T>(Type type, out T service) where T : class
        {
            return _servicesManager.TryGet(out service);
        }

        bool TryGetNextInHierarchy(out ServiceLocator container)
        {
            if (this == globalLocator)
            {
                container = null;
                return false;
            }

            container = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOfLocal(this);
            return container != null;
        }

        void OnDestroy()
        {
            if (this == globalLocator)
            {
                globalLocator = null;
            }
            else if (sceneLocalContainers.ContainsValue(this))
            {
                // 딕셔너리에 자기에 해당하는 값이 있으면 그 해당 키값 제거
                sceneLocalContainers.Remove(gameObject.scene);
            }
        }

        // https://docs.unity3d.com/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics()
        {
            globalLocator = null;
            sceneLocalContainers = new Dictionary<Scene, ServiceLocator>();
            tmpSceneGameObjectList = new List<GameObject>();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/ServiceLocator/Add Global")]
        static void AddGlobal()
        {
            var go = new GameObject(k_globalServiceLocatorName, typeof(ServiceLocatorGlobal));
        }

        [MenuItem("GameObject/ServiceLocator/Add Scene")]
        static void AddScene()
        {
            var go = new GameObject(k_sceneServiceLocatorName, typeof(ServiceLocatorLocal));
        }
#endif
    }
}