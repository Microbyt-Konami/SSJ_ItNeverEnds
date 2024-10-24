using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] protected MagicWandController _magicWandController;

    public void RefreshMagicWamd()
    {
        _magicWandController = GetComponentInChildren<MagicWandController>();
    }

    public void Shoot() => _magicWandController?.Shoot();

    private void Start()
    {
        RefreshMagicWamd();
    }
}
