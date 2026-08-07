using UnityEngine;
using UnityEngine.Android;

public class AnimationLeasoner : MonoBehaviour
{
    ModelController model;
    private void Awake()
    {
        model = GetComponent<ModelController>();
    }

    public void SetFlaot(string message ,float value)
    {
        model.Anim.SetFloat(message, value);
    }
}

public class ModelController : MonoBehaviour
{
    public Animator Anim;
    
}
