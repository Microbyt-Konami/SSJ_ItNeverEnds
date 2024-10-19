using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class DoorUseTrigger : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject iconsTrigger;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject padLockOpen;
    [SerializeField] private GameObject padLockClosed;

    public void OpenDoor()
    {

    }

    private void Awake()
    {
        iconsTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iconsTrigger.SetActive(true);
            padLockOpen.SetActive(isOpen);
            padLockClosed.SetActive(!isOpen);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            iconsTrigger.SetActive(false);
    }
}
