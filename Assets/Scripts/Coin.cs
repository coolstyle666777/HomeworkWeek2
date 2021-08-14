using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    public event Action Collected;

    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), _rotationSpeed).SetSpeedBased(true).SetRelative(true)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();

        if (player != null)
        {
            Collected?.Invoke();
            Destroy(gameObject);
        }
    }
}