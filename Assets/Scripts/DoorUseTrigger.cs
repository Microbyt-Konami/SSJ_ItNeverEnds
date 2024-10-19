using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class DoorUseTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI openDoorText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Use"))
            openDoorText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Use"))
            openDoorText.gameObject.SetActive(false);
    }
}
