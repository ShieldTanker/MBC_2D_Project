using UnityEngine;
using UnityEngine.Android;

public class AnimController : MonoBehaviour
{
    public ModelController model;

    private void Awake()
    {
        model = GetComponentInChildren<ModelController>();
    }

    public void SetFlaot(string message ,float value)
    {
        model.Anim.SetFloat(message, value);
    }

    public void SetBool(string message ,bool value)
    {
        model.Anim.SetBool(message, value);
    }
}