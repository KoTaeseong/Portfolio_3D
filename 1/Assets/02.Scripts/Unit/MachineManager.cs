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

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Player _player;

    private Target _target;
    [SerializeField] private Vector3 _direction;

    private bool _isMoveable;
    private float _speed;


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
            _direction = new Vector3(target.point.x, 0f, target.point.z);
        else
            _direction = new Vector3(target.gameObject.transform.position.x, 0f, gameObject.transform.position.z);

        //이동방향으로 부드러운 회전을 하기위해 선형보간 사용
        transform.forward = Vector3.Lerp(this.transform.forward, _direction, 20 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isMoveable == false)
            return;

        _rigidbody.transform.position = _direction * _speed * Time.deltaTime;
    }
}
