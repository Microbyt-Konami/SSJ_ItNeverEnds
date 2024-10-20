using Synty.AnimationBaseLocomotion.Samples.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private bool _isTriggered = false;
    private UseTrigger _currentUseTrigger;

    private void OnEnable()
    {
        _inputReader.onUsePerformed += UseOn;
    }

    private void OnDisable()
    {
        _inputReader.onUsePerformed -= UseOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
            _currentUseTrigger = other.GetComponent<UseTrigger>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
            _currentUseTrigger = null;
    }

    private void UseOn()
    {
        _currentUseTrigger?.DoUse();
    }
}
