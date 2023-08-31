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

public enum AttackState
{
    None,
    WaitUntilTargeting,
    TargetTracking,
    DoAttack,
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
    public AttackState actionState;

    public Target target
    {
        get => _target;
        set
        {
            _target = value;
        }
    }

    public void SetTarget(GameObject gameObject)
    {
        if (gameObject.layer == 13)
        {
            _target.point = gameObject.transform.position;
        }
        else
        {
            _target.point = new Vector3(gameObject.transform.position.x,0f,gameObject.transform.position.z);
        }
        
        _target.gameObject = gameObject;
        _target.targetType = (TargetType)gameObject.layer;
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


    [SerializeField] private LayerMask _enemyMask;


    public bool ChangeState(StateType newState)
    {
        if (state == newState)
            return false;

        _animator.Play(newState.ToString());
        state = newState;
        return true;
    }

    public bool ChangeActionState(AttackState newState)
    {
        if (actionState == newState)
            return false;

        actionState = newState;
        return true;
    }


    /*
     직접공격
        대상의 정보를 받고 공격명령을 받으면 행동트리 실행

        대상의 정보 : 대상의 위치, 대상과의 거리  계속 업데이트
        공격 사거리
        if 대상과의 거리 <= 공격 사거리
           이동을 멈추고 공격
        else
           계속 이동

    


    지점이동후 자동공격
      지점위치 정보
        지점이동하며 사거리내에 적 탐색
          if 적 탐색시
            이동멈추고 공격
          else
            계속이동
     
     */

    Sequence mRootNodeAttackBasic = null;

    NodeStates DoFindTarget()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(this.transform.position, _player.AttackRange, _enemyMask);
        if (enemyColliders.Length == 1)
        {
            //target = enemyColliders[0].gameObject;

            return NodeStates.SUCCESS;
        }
        else if(enemyColliders.Length > 1)
        {


            return NodeStates.SUCCESS;
        }

        return NodeStates.FAILURE;
    }

    void BuildBTsAttackBasic()
    {
        ActionNode tFollow = new ActionNode(DoFollowTarget);
        ActionNode tAttack = new ActionNode(DoAttack);

        List<Node> tLevel_2 = new List<Node>();
        tLevel_2.Add(tFollow);
        tLevel_2.Add(tAttack);

        mRootNodeAttackBasic = new Sequence(tLevel_2);
    }

    NodeStates DoFollowTarget()
    {
        if(distanceTarget <= _player.AttackRange)
        {
            _navMeshAgent.enabled= false;
            return NodeStates.SUCCESS;
        }

        return NodeStates.FAILURE;
    }

    NodeStates DoAttack()
    {
        //공격
        return NodeStates.SUCCESS;
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
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(actionState == AttackState.TargetTracking)
        {
            Gizmos.DrawWireSphere(this.transform.position, _player.AttackRange);
        }

        
    }
}
