using UnityEngine;

namespace UnityServiceLocator
{
    [AddComponentMenu("ServiceLocator/ServiceLocator Scene")]
    public class ServiceLocatorLocal : Bootstrapper
    {
        protected override void Bootstrap()
        {
            Locator.ConfigureForLocal();
        }
    }
}