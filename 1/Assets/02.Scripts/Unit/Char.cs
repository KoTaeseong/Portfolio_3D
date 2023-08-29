using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour,IHP
{
    public float hp
    {
        get => _hp;
        set
        {
            if (_hp == value)
                return;

            float prev = _hp;
            _hp = value;

            OnHpChanged?.Invoke(value);

            if (prev > value)
            {
                OnHpDecreased?.Invoke(prev - value);
                if (_hp <= _hpMin)
                {
                    OnHpMin?.Invoke();
                    //state Die
                }
                else
                {
                    //state Hurt
                }
            }
            else if (prev < value)
            {
                OnHpIncrease?.Invoke(value - prev);
                if (value >= _hpMax)
                {
                    OnHpMax?.Invoke();
                }
            }
        }
    }
    public float hpMin => _hpMin;
    public float hpMax => _hpMax;
    public float Speed => _moveSpeed;
    public float AttackRange => _attackRange;

    [Header("Status")]
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _atk = 10f;
    [SerializeField] float _hpMax = 100f;
    [SerializeField] float _attackRange = 2f;

    float _hpMin;
    float _hp;

    public event Action<float> OnHpChanged;
    public event Action<float> OnHpDecreased;
    public event Action<float> OnHpIncrease;
    public event Action OnHpMin;
    public event Action OnHpMax;


    protected virtual void Start()
    {
        hp = hpMax;
    }

    public virtual void Damage(GameObject Attacker, float damage)
    {
        hp -= damage;
    }

    public virtual void Heal(GameObject gameObject, float heal)
    {
        hp += heal;
    }


}
