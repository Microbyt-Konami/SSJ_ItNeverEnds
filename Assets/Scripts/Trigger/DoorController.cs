using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DoorState
{
    Close, CloseToOpen, Open, OpenToClose
}

[RequireComponent(typeof(AudioSource))]
public class DoorController : MonoBehaviour
{
    /*
        OpenIni: angleOpenInit
        OpenEnd: angleCloseInit
        CloseIni: angleEnd = angleOpenInit
        CloseEnd: angleEnd = angleOpenInit
    */
    [SerializeField] private float angleOpenInit = 180;
    [SerializeField] private float angleCloseInit = 72;
    [SerializeField] private float timeMovement = 1f;
    [SerializeField] private bool isOpening;
    [SerializeField] private bool inMovement;
    [SerializeField] private AudioClip audioOpen;
    [SerializeField] private AudioClip audioClose;
    [field: SerializeField, Header("Debug")] public DoorState DoorState { get; private set; }

    private Transform doorTransform;
    private AudioSource audioSource;
    private Coroutine movementCoroutine;

    public UnityEvent OnMovementBegin;
    public UnityEvent OnMovementEnd;

    private void Awake()
    {
        doorTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
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
        if (audioClose != null)
        {
            audioSource.clip = audioOpen;
            audioSource.Play();
        }

        Movement(DoorState.Open);
    }

    public void Close()
    {
        if (audioOpen != null)
        {
            audioSource.clip = audioClose;
            audioSource.Play();
        }

        Movement(DoorState.Close);
    }

    private void OnValidate()
    {
        SetUp();
    }

    private void Movement(DoorState doorStateEnd)
    {
        float angleInit = doorTransform.localEulerAngles.y;
        float angleEnd;

        switch (doorStateEnd)
        {
            case DoorState.Open:
                // OpenEnd
                angleEnd = angleCloseInit;
                break;
            case DoorState.Close:
            default:
                // CloseEnd
                angleEnd = angleOpenInit;
                break;
        }

        if (movementCoroutine != null)
            StopMovement();

        movementCoroutine = StartCoroutine(MovementCoRoutine(angleInit, angleEnd, doorStateEnd));
    }

    private void StopMovement()
    {
        StopCoroutine(movementCoroutine);
        movementCoroutine = null;
        OnMovementEnd.Invoke();
    }

    private IEnumerator MovementCoRoutine(float angleInit, float angleEnd, DoorState doorState)
    {
        inMovement = true;
        OnMovementBegin.Invoke();
        switch (doorState)
        {
            case DoorState.Open:
                switch (DoorState)
                {
                    case DoorState.Close:
                        isOpening = true;
                        DoorState = DoorState.CloseToOpen;
                        break;
                    case DoorState.OpenToClose:
                        DoorState = DoorState.CloseToOpen;
                        break;
                    case DoorState.Open:
                    case DoorState.CloseToOpen:
                        break;
                }
                break;
            case DoorState.Close:
                switch (DoorState)
                {
                    case DoorState.Open:
                    case DoorState.CloseToOpen:
                        DoorState = DoorState.OpenToClose;
                        break;
                    case DoorState.Close:
                    case DoorState.OpenToClose:
                        break;
                }
                break;
        }

        if (angleInit != angleEnd)
        {
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
        }

        isOpening = false;
        inMovement = false;
        DoorState = doorState;
        movementCoroutine = null;
        OnMovementEnd.Invoke();
    }
}
