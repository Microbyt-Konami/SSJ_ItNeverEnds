using Synty.AnimationBaseLocomotion.Samples;
using Synty.AnimationBaseLocomotion.Samples.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerSoundsControler : MonoBehaviour
{
    [SerializeField] private AudioClip footWalkSound;
    [SerializeField] private AudioClip footRunSound;
    [SerializeField] private AudioClip footJumpSound;

    private PlayerController playerController;
    private AudioSource audioSource;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
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

    private void PlayStepSound()
    {
        audioSource.clip = playerController.IsSprinting ? footRunSound : footWalkSound;
        if (audioSource.clip != null)
            audioSource.Play();
    }

    private void PlayJumpSound()
    {
        audioSource.clip = footJumpSound;
        if (audioSource.clip != null)
            audioSource.Play();
    }
}
