using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTrigger
{
    Close, CloseToOpen, Open, OpenToClose
}

public class UseTrigger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private protected StateTrigger _state;

    public StateTrigger State => _state;

    public virtual void DoUse() { }

    protected virtual void Awake()
    {
        SetUpState();
    }
    protected virtual void SetUpState() { }
}
