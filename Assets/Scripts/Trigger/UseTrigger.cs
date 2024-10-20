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
    [SerializeField] private protected bool inTrigger;
    [SerializeField] private protected bool inPlayerTrigger;

    public StateTrigger State => _state;

    public virtual void DoUse() { }

    protected virtual void Awake()
    {
        SetUpState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = inPlayerTrigger = true;
            OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = inPlayerTrigger = false;
            OnPlayerExit();
        }
    }

    protected virtual void SetUpState() { }
    protected virtual void OnPlayerEnter() { }
    protected virtual void OnPlayerExit() { }
}
