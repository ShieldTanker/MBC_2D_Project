using System.Collections.Generic;
using UnityEngine;

public abstract class PartSelectorController : MonoBehaviour
{
    public StatusUIController _statusUI;
    public LoadoutData LoadoutData;


    private void Awake()
    {
        OnAwake();
    }

    public virtual void OnAwake() { }
}

public abstract class BodyPartSelector : PartSelectorController
{
    public PartContent[] partContent;

    public override void OnAwake()
    {
        partContent = GetComponentsInChildren<PartContent>();
        foreach (var part in partContent)
        {
            part.controller = this;
        }
    }

    public abstract void SetBodyLoadoutData(PartDataBase data);
}