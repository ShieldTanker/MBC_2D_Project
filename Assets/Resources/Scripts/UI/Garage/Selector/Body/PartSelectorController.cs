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
            
        if (partContent.Length > 0)
        {
            foreach (var part in partContent)
                part.controller = this;
            
        }
    }

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            if(partContent.Length > 0)
                partContent[0]._button.Select();
        }
    }

    public abstract void SetBodyLoadoutData(PartDataBase data);
}