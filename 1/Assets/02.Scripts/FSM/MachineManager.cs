using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    public enum StateType
    {
        Idle,
        Move,
        Attack,
        Skill,
        Hurt,
        Die,
    }

    [SerializeField] StateType state;
    [SerializeField] Vector3 velocity;

    private Rigidbody _rb;
    private Animator _animator;

    public bool ChagneState(StateType newType)
    {
        if (state == newType)
            return false;

        state = newType;
        _animator.SetInteger("StateNum", (int)newType);
        _animator.SetBool("ChangeState", true);
        return true;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (state == StateType.Move)
        {
            _rb.transform.position += velocity * Time.deltaTime;
        }
    }
}
