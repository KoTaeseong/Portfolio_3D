using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;





public class PlayerMove : MonoBehaviour
{
    public enum StateType
    {
        Idle,
        Move,
    }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 velocity;
    private Rigidbody rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obejctLayer;

    [SerializeField] StateType state = StateType.Idle;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //��Ŭ���� �Ұ�� �ش� ��ġ�� �̵�
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    target = hit.point;
                    Debug.Log("Click ground");
                }
                else
                {
                    target = hit.collider.transform.position;
                    target.y = transform.position.y;
                    Debug.Log("Click object");
                }
                state = StateType.Move;
                animator.SetFloat("Blend", 1);
            }
        }

        //�������� ���������� �̿��� �̵����� ����
        velocity = (target - this.transform.position).normalized;
        Debug.Log(velocity.ToString());

        //�̵��������� �ε巯�� ȸ���� �ϱ����� �������� ���
        transform.forward= Vector3.Lerp(this.transform.forward,velocity,20*Time.deltaTime);

        if (state == StateType.Move &&
            Vector3.Distance(target,transform.position) <= 0.1f)
        {
            state = StateType.Idle;
            animator.SetFloat("Blend", 0);
        }
    }

    private void FixedUpdate()
    {
        if (state == StateType.Move)
            rb.position += velocity * moveSpeed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if (target != default)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(target, radius: 0.5f);
            Gizmos.DrawLine(transform.position, target);
        }
    }
}
