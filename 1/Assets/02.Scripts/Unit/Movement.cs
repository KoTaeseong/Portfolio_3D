using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isMoveable;
    public bool isDirectionChagneable;
    public event Action onDirectionChange;

    public Vector2 direction
    {
        get => _direction;
        set
        {
            if (isDirectionChagneable == false)
                return;
            if (_direction== value) 
                return;

            _direction= value;
            onDirectionChange?.Invoke();
        }
    }

    private Vector2 _direction;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
    }
}
