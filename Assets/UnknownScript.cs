using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownScript : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private MeshRenderer _meshRenderer;

    public event Action Triggered;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.gameObject.GetComponentInParent<PlayerMovement>();

        if (player != null)
        {
            Triggered?.Invoke();
            _meshRenderer.material = _material;
        }
    }
}