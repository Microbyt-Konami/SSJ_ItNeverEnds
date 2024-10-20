using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DoorUseTrigger : UseTrigger
{
    [SerializeField] private bool isOpen;
    [Header("Settings")]
    [SerializeField] private GameObject iconsTrigger;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject padLockOpen;
    [SerializeField] private GameObject padLockClosed;
    [SerializeField] private DoorController[] doors;

    public override void DoUse()
    {
        if (isOpen)
        {
            foreach (DoorController door in doors)
                door.Close();
        }
        else
        {
            foreach (DoorController door in doors)
                door.Open();
        }
        isOpen = !isOpen;
    }

    protected override void Awake()
    {
        base.Awake();
        iconsTrigger.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (DoorController door in doors)
        {
            door.OnMovementBegin.AddListener(OnBeginMovement);
            door.OnMovementEnd.AddListener(OnEndMovement);
        }
    }

    private void OnDisable()
    {
        foreach (DoorController door in doors)
        {
            door.OnMovementBegin.RemoveListener(OnBeginMovement);
            door.OnMovementEnd.RemoveListener(OnEndMovement);
        }
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

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        iconsTrigger.SetActive(true);
        ShowIcons();
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        iconsTrigger.SetActive(false);
    }

    private void ShowIcons()
    {
        switch (_state)
        {
            case StateTrigger.Close:
            case StateTrigger.CloseToOpen:
                padLockOpen.SetActive(true);
                padLockClosed.SetActive(false);
                break;
            case StateTrigger.Open:
            case StateTrigger.OpenToClose:
                padLockOpen.SetActive(false);
                padLockClosed.SetActive(true);
                break;
        }
        padLockOpen.SetActive(isOpen);
        padLockClosed.SetActive(!isOpen);
    }

    private void OnBeginMovement()
    {
        if (inPlayerTrigger)
            iconsTrigger.SetActive(false);
    }

    private void OnEndMovement()
    {
        if (inPlayerTrigger)
            iconsTrigger.SetActive(true);
    }
}
