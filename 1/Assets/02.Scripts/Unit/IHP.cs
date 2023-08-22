using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHP
{
    public float hp { get; set; }
    public float hpMin { get;}
    public float hpMax { get;}

    event Action<float> OnHpChanged;
    event Action<float> OnHpDecreased;
    event Action<float> OnHpIncrease;
    event Action OnHpMin;
    event Action OnHpMax;
}
