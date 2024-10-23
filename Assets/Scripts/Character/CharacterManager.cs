using Synty.AnimationBaseLocomotion.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
public class CharacterManager : MonoBehaviour
{
    [SerializeField] private protected HealthDisplay healthDisplay;

    protected CharacterHealth health;

    protected virtual void Awake()
    {
        health = GetComponent<CharacterHealth>();
    }

    protected virtual void Start()
    {
        healthDisplay.ShowDisplay(false);
    }
}
