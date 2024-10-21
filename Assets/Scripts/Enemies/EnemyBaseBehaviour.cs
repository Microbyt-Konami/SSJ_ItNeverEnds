using System.Collections;
using System.Collections.Generic;
using Synty.AnimationBaseLocomotion.Samples.InputSystem;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class EnemyBaseBehaviour : MonoBehaviour
{
    protected InputReader _inputReader;

    protected virtual void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    IEnumerator PruebaCouRotine()
    {
        bool move = false;

        _inputReader._moveComposite = new Vector2(0, 0.5f);
        while (true)
        {
            _inputReader._movementInputDetected = move;
            move = !move;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
