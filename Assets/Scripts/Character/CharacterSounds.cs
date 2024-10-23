using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class CharacterSounds : MonoBehaviour
{
    public AudioClip LandingAudioClip;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    public AudioClip[] footWalkAudioClips;
    public AudioClip[] footRunClips;
    public AudioClip[] footJumpSound;

    protected AudioSource _audioSource;

    protected bool isStepSound, isJumpSound;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    protected void PlayFootstep(bool isRun = false)
    {
        var clip = isRun ? footRunClips : footWalkAudioClips;

        if (clip.Length > 0)
        {
            var index = Random.Range(0, clip.Length);

            _audioSource.clip = clip[index];
            _audioSource.volume = FootstepAudioVolume;
            _audioSource.Play();
        }
    }

    protected void PlayLand()
    {
        if (LandingAudioClip != null)
        {
            _audioSource.clip = LandingAudioClip;
            _audioSource.volume = FootstepAudioVolume;
            _audioSource.Play();
        }
    }

    protected void PlayJump()
    {
        if (footJumpSound.Length > 0)
        {
            var index = Random.Range(0, footJumpSound.Length);

            _audioSource.clip = footJumpSound[index];
            _audioSource.volume = FootstepAudioVolume;
            _audioSource.Play();
        }
    }

    protected void PlayFootSound(Action play, bool isStep)
    {
        StartCoroutine(PlayFootSoundCoroutine(play, isStep));
    }

    protected IEnumerator PlayFootSoundCoroutine(Action play, bool isStep)
    {
        if (_audioSource.isPlaying)
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

            yield return new WaitUntil(() => !_audioSource.isPlaying);
        }

        if (isStep)
            isStepSound = true;
        else
            isJumpSound = true;
        play();
        yield return new WaitUntil(() => !_audioSource.isPlaying);
        if (isStep)
            isStepSound = false;
        else
            isJumpSound = false;
    }
}
