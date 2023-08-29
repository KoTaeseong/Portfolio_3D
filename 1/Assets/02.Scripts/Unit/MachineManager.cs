using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public enum StateType
{
    Idle,
    Move,
    Jump,
    Attack,
    Hurt,
    Die,
}

public enum TargetType
{
    None,
    Ground = 8,
    Object = 10,
    Enemy = 13,
}

public enum ActionState
{
    None,
    WaitUntilAction,
    DoAction,
}

[Serializable]
public class Target
{
    public TargetType targetType;
    public Vector3 point;
    public GameObject gameObject;
}


public class MachineManager : MonoBehaviour
{
    public bool isGrounded => Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, _groundCastMaxDistance + 1.0f, _groundMask);

    public StateType state;
    public ActionState actionState;

    public Target target
    {
        get => _target;
        set
        {
            _target = value;
        }
    }

    public float distanceTarget
    {
        get => _distanceTarget;
        set
        {
            _distanceTarget = value;

            if (value <= 0.05f)
            {
                _distanceTarget = 0f;
                //_isMoveable= false;
                target = null;
            }
        }
    }

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Player _player;

    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Target _target = null;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _distanceTarget;

    [SerializeField] private bool _isMoveable = true;
    //private float _speed;


    [SerializeField] private float _groundCastMaxDistance;
    [SerializeField] private LayerMask _groundMask;


    public bool ChangeState(StateType newState)
    {
        if (state == newState)
            return false;

        _animator.Play(newState.ToString());
        state = newState;
        return true;
    }

    public bool ChangeActionState(ActionState newState)
    {
        if (actionState == newState)
            return false;

        actionState = newState;
        return true;
    }


    private void Awake()
    {
        _animator= GetComponent<Animator>();
        _rigidbody= GetComponent<Rigidbody>();
        _player= GetComponent<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        BehaviourBase[] behaviours = _animator.GetBehaviours<BehaviourBase>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].Initialize(this);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            if(target.targetType == TargetType.Ground)
            {
                Vector3 targetPoint = new Vector3(target.point.x, 0f, target.point.z);
                _direction = (targetPoint - this.transform.position).normalized;
                distanceTarget = Vector3.Distance(target.point, this.transform.position);
                
            }
        }

        //transform.forward = Vector3.Lerp(this.transform.forward, _direction, 20 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (state != StateType.Move)
            return;
        //_rigidbody.velocity = _direction * _player.Speed;
        _navMeshAgent.SetDestination(target.point);
        Debug.Log(_player.Speed);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(actionState == ActionState.WaitUntilAction)
        {
            Gizmos.DrawWireSphere(this.transform.position, _player.AttackRange);
        }

        
    }
}
