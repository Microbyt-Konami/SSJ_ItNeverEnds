using Synty.AnimationBaseLocomotion.Samples;
using Synty.AnimationBaseLocomotion.Samples.InputSystem;
using System;
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
    private bool isStepSound, isJumpSound;

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
        var clip = playerController.IsSprinting ? footRunSound : footWalkSound;

        PlayFootSound(clip, true);
    }

    private void PlayJumpSound()
    {
        PlayFootSound(footJumpSound, false);
    }

    private void PlayFootSound(AudioClip clip, bool isStep)
    {
        StartCoroutine(PlayFootSoundCoroutine(clip, isStep));
    }

    private IEnumerator PlayFootSoundCoroutine(AudioClip clip, bool isStep)
    {
        if (audioSource.isPlaying)
        {
            if (isStep)
            {
                if (isStepSound)
                    yield break;
            }
            else
            {
                if (isJumpSound)
                    yield break;
            }

            yield return new WaitUntil(() => !audioSource.isPlaying);
        }

        if (isStep)
            isStepSound = true;
        else
            isJumpSound = true;
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitUntil(() => !audioSource.isPlaying);
        if (isStep)
            isStepSound = false;
        else
            isJumpSound = false;
    }
}
