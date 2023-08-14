using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHP
{
    public float hp { get; set; }
    public float hpMin { get; set; }
    public float hpMax { get; set; }

    event Action<float> OnHpChanged;
    event Action<float> OnHpDecreased;
    event Action<float> OnHpIncrease;
    event Action OnHpMin;
    event Action OnHpMax;
}
