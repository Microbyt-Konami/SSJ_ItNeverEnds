using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MagicWandController : MonoBehaviour
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject ballFire;

    public void Shoot(float force = 10)
    {
        var direction = (rayPoint.position - weapon.transform.position).normalized;
        //var rotation = Quaternion.LookRotation(direction);
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        //var rotation = Quaternion.identity;
        var ball = Instantiate(ballFire, rayPoint.position, rotation);

        ball.GetComponent<Rigidbody>().velocity = direction * force;
    }

    private void Awake()
    {
        if (weapon == null)
            weapon = transform;
    }
}
