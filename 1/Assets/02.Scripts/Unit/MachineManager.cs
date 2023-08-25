using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Ground,
    Object,
}

public struct Target
{
    public TargetType targetType;
    public Vector3 point;
    public GameObject gameObject;
}

public class MachineManager : MonoBehaviour
{
    public bool isGrounded => Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, _groundCastMaxDistance + 1.0f, _groundMask);

    public StateType state;

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
            if (value <= 0.1f)
            {
                _distanceTarget = 0f;
                _isMoveable= false;
            }
            _distanceTarget = value;
        }
    }

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Player _player;

    private Target _target;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _distanceTarget;

    [SerializeField] private bool _isMoveable = true;
    //private float _speed;


    [SerializeField] private float _groundCastMaxDistance;
    [SerializeField] private LayerMask _groundMask;


    private void Awake()
    {
        _animator= GetComponent<Animator>();
        _rigidbody= GetComponent<Rigidbody>();
        _player= GetComponent<Player>();
    }

    private void Update()
    {
        if (target.targetType == TargetType.Ground)
        {
            Vector3 targetPoint = new Vector3(target.point.x, 0f, target.point.z);
            _direction = (targetPoint - this.transform.position).normalized;
            distanceTarget = Vector3.Distance(target.point, this.transform.position);

        }

        transform.forward = Vector3.Lerp(this.transform.forward, _direction, 20 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isMoveable == false)
            return;
        if(distanceTarget > 0.1f)
            _rigidbody.velocity = _direction * _player.Speed;
        Debug.Log(_player.Speed);
    }
}
