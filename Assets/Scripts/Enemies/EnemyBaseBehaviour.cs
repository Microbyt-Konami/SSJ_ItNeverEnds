using System.Collections;
using System.Collections.Generic;
using Synty.AnimationBaseLocomotion.Samples;
using Synty.AnimationBaseLocomotion.Samples.InputSystem;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyBaseBehaviour : MonoBehaviour
{
    protected EnemyController _enemyController;

    protected virtual void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }
}
