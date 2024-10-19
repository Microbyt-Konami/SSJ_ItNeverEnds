using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class DoorUseTrigger : UseTrigger
{
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject iconsTrigger;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject padLockOpen;
    [SerializeField] private GameObject padLockClosed;
    [SerializeField] private DoorController[] doors;

    public override void DoUse()
    {
        switch (_state)
        {
            case StateTrigger.Close:
            case StateTrigger.CloseToOpen:
                foreach (DoorController door in doors)
                    door.Open();
                _state = StateTrigger.Close;
                break;
            case StateTrigger.Open:
            case StateTrigger.OpenToClose:
                foreach (DoorController door in doors)
                    door.Close();
                _state = StateTrigger.Open;
                break;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        iconsTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iconsTrigger.SetActive(true);
            switch (_state)
            {
                case StateTrigger.Close:
                case StateTrigger.CloseToOpen:
                    padLockOpen.SetActive(false);
                    padLockClosed.SetActive(true);
                    break;
                case StateTrigger.Open:
                case StateTrigger.OpenToClose:
                    padLockOpen.SetActive(true);
                    padLockClosed.SetActive(false);
                    break;
            }
            padLockOpen.SetActive(isOpen);
            padLockClosed.SetActive(!isOpen);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            iconsTrigger.SetActive(false);
    }

    protected override void SetUpState()
    {
        base.SetUpState();
        DoorState? state = null;

        foreach (DoorController door in doors)
            if (!state.HasValue)
                state = door.DoorState;
            else if (door.DoorState != state)
            {
                _state = StateTrigger.OpenToClose;
                isOpen = true;

                return;
            }

        _state = !state.HasValue || state.Value == DoorState.Close ? StateTrigger.Close : StateTrigger.Open;

        return;
    }
}
