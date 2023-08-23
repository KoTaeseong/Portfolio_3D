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

public class MachineManager : MonoBehaviour
{
    public bool isGrounded => Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, _groundCastMaxDistance + 1.0f, _groundMask);

    public StateType state;

    public Vector2 direction;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Player _player;

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
        _speed = _player.Speed;


        //이동방향으로 부드러운 회전을 하기위해 선형보간 사용
        transform.forward = Vector3.Lerp(this.transform.forward, direction, 20 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isMoveable == false)
            return;

        _rigidbody.transform.position = direction * _speed * Time.deltaTime;
    }
}
