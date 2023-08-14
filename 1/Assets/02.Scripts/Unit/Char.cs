using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour,IHP
{
    public float hp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float hpMin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float hpMax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action<float> OnHpChanged;
    public event Action<float> OnHpDecreased;
    public event Action<float> OnHpIncrease;
    public event Action OnHpMin;
    public event Action OnHpMax;

    
}
