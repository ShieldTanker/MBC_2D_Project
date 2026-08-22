using UnityEngine;
using UnityEngine.Android;

public class AnimController : MonoBehaviour
{
    public ModelController model;

    private void Awake()
    {
        model = GetComponentInChildren<ModelController>();
    }
}