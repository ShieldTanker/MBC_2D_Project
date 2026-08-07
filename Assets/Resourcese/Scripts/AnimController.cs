using UnityEngine;
using UnityEngine.Android;

public class AnimController : MonoBehaviour
{
    public ModelController model;

    public void SetFlaot(string message ,float value)
    {
        model.Anim.SetFloat(message, value);
    }
}