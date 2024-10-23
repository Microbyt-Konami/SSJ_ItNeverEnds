using Synty.AnimationBaseLocomotion.Samples;
using Synty.AnimationBaseLocomotion.Samples.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerSoundsControler : CharacterSounds
{
    private PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.OnWalking.AddListener(PlayStepSound);
        playerController.OnJumpDone.AddListener(PlayJumpSound);
    }

    private void OnDisable()
    {
        playerController.OnWalking.RemoveListener(PlayStepSound);
        playerController.OnJumpDone.RemoveListener(PlayJumpSound);
    }

    private void PlayStepSound() => PlayFootSound(() => PlayFootstep(playerController.IsSprinting), true);

    private void PlayJumpSound()
    {
        PlayFootSound(PlayJump, false);
    }
}
