using System.Collections;
using System.Collections.Generic;
using Synty.AnimationBaseLocomotion.Samples;
using Synty.AnimationBaseLocomotion.Samples.InputSystem;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerController))]
public class EnemyBaseBehaviour : MonoBehaviour
{
    protected InputReader _inputReader;
    protected PlayerController _playerController;

    protected virtual void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerController = GetComponent<PlayerController>();
        StartCoroutine(PruebaCouRotine());
    }

    IEnumerator PruebaCouRotine()
    {
        while (true)
        {
            //_playerController.IsStopped = false;
            //_playerController.IsGrounded = true;
            //_playerController.IsStarting = false;
            //_playerController.MovementInputTapped = false;
            //_playerController.MovementInputHeld = true;
            _inputReader._moveComposite = new Vector2(0, 1f);
            _inputReader._movementInputDetected = true;
            yield return new WaitForSeconds(0.1f);
            _inputReader._movementInputDetected = false;

            yield return new WaitUntil(() => _playerController.IsStopped && _playerController.IsGrounded && !_playerController.MovementInputTapped && !_playerController.MovementInputHeld);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
