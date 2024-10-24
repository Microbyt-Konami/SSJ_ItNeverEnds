using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
public class CharacterBaseController : MonoBehaviour
{
    [SerializeField] protected MagicWandController _magicWandController;
    [SerializeField] protected HealthDisplay _healthDisplay;
    [SerializeField] protected float _forceShoot = 20;

    private CharacterHealth _characterHealth;

    public void UpdateMagicWamd()
    {
        _magicWandController = GetComponentInChildren<MagicWandController>();
    }

    public void Shoot() => _magicWandController?.Shoot(_forceShoot);

    protected virtual void Awake()
    {
        _characterHealth = GetComponent<CharacterHealth>();
    }

    protected virtual void Start()
    {
        if (_magicWandController == null)
            UpdateMagicWamd();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

    }
}
