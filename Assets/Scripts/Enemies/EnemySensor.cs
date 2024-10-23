using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    [SerializeField] private Transform _eyeRayCastPost;
    [SerializeField] private Transform _chestRayCastPost;
    [SerializeField] private Transform _footRayCastPost;
    [SerializeField] private float _distance = 1F;
    [SerializeField] private float _distanceLookPlayer = 10f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _lookPlayerLayerMask;

    [field: SerializeField, Header("Debug")] public bool IsCollised { get; private set; }
    [field: SerializeField] public bool IslookingPlayer { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }

    public void UpdateSensors()
    {
        IsCollised =
            Physics.Raycast(_eyeRayCastPost.position, _eyeRayCastPost.TransformDirection(Vector3.forward), _distance, _layerMask)
            || Physics.Raycast(_chestRayCastPost.position, _chestRayCastPost.TransformDirection(Vector3.forward), _distance, _layerMask)
            || Physics.Raycast(_footRayCastPost.position, _footRayCastPost.TransformDirection(Vector3.forward), _distance, _layerMask);
        IslookingPlayer = Physics.Raycast(_eyeRayCastPost.position, _eyeRayCastPost.TransformDirection(Vector3.forward), out RaycastHit hit, _distanceLookPlayer, _lookPlayerLayerMask);
        Player = IslookingPlayer ? Player = hit.collider.gameObject : null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_eyeRayCastPost.position, _eyeRayCastPost.TransformDirection(Vector3.forward) * _distance);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_chestRayCastPost.position, _chestRayCastPost.TransformDirection(Vector3.forward) * _distance);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_footRayCastPost.position, _footRayCastPost.TransformDirection(Vector3.forward) * _distance);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(_eyeRayCastPost.position, _eyeRayCastPost.TransformDirection(Vector3.forward) * _distanceLookPlayer);
    }
}
