using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Close, CloseToOpen, Open, OpenToClose
}

public class DoorController : MonoBehaviour
{
    [SerializeField] private float angleOpenInit = 180;
    [SerializeField] private float angleCloseInit = 72;
    [SerializeField] private float timeMovement = 1f;
    [SerializeField] private bool isOpening;
    [field: SerializeField, Header("Debug")] public DoorState DoorState { get; private set; }

    private Transform doorTransform;

    private void Awake()
    {
        doorTransform = GetComponent<Transform>();
        SetUp();
    }

    private void SetUp()
    {
        if (doorTransform == null)
            return;

        float angle = doorTransform.localEulerAngles.y;

        if (angle == angleOpenInit)
        {
            DoorState = DoorState.Close;
            isOpening = false;
        }
        else if (angle == angleCloseInit)
        {
            DoorState = DoorState.Open;
            isOpening = true;
        }
        else if (isOpening)
        {
            DoorState = DoorState.OpenToClose;
        }
        else
            DoorState = DoorState.CloseToOpen;
    }

    public void Open()
    {
        if (!isOpening)
            isOpening = true;

        Movement();
    }

    public void Close()
    {
        if (isOpening)
            isOpening = false;

        Movement();
    }

    private void OnValidate()
    {
        SetUp();
    }

    private void Movement()
    {
        float angleInit = doorTransform.localEulerAngles.y;
        float angleEnd = (isOpening) ? angleCloseInit : angleOpenInit;
        StartCoroutine(OpeningOrCloseCoRoutine(angleInit, angleEnd));
    }

    private IEnumerator OpeningOrCloseCoRoutine(float angleInit, float angleEnd)
    {
        if (angleInit == angleEnd)
            yield break;

        float time;
        if (angleInit < angleEnd)
        {
            time = 0;
            while (time < timeMovement)
            {
                time += Time.deltaTime;
                float angle = Mathf.Lerp(angleInit, angleEnd, time / timeMovement);
                doorTransform.localEulerAngles = new Vector3(doorTransform.localEulerAngles.x, angle, doorTransform.localEulerAngles.z);

                yield return null;
            }
        }
        else
        {
            time = timeMovement;
            while (time > 0)
            {
                time -= Time.deltaTime;
                float angle = Mathf.Lerp(angleEnd, angleInit, time / timeMovement);
                doorTransform.localEulerAngles = new Vector3(doorTransform.localEulerAngles.x, angle, doorTransform.localEulerAngles.z);

                yield return null;
            }
        }

        isOpening = !isOpening;
    }
}
