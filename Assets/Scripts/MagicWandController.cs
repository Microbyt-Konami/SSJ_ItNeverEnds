using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWandController : MonoBehaviour
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject ballFire;

    public void Shoot(float force = 10)
    {
        var ball = Instantiate(ballFire, rayPoint.position, Quaternion.identity);

        ball.GetComponent<Rigidbody>().velocity = weapon.forward * force;
    }

    private void Awake()
    {
        if (weapon == null)
            weapon = transform;
    }
}
