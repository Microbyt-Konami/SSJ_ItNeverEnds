using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[RequireComponent(typeof(AudioSource))]
public class EnemySound : MonoBehaviour
{
    public AudioClip LandingAudioClip;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    public AudioClip[] FootstepAudioClips;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);

                _audioSource.clip = FootstepAudioClips[index];
                _audioSource.volume = FootstepAudioVolume;
                _audioSource.Play();
            }
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f && LandingAudioClip != null)
        {
            _audioSource.clip = LandingAudioClip;
            _audioSource.volume = FootstepAudioVolume;
            _audioSource.Play();
        }
    }
}
