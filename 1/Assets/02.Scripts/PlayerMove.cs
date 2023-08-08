using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 velocity;
    private Rigidbody rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obejctLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //우클릭을 할경우 해당 위치로 이동
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

            }
        }

        //목적지와 현재지점을 이용해 이동방향 결정
        velocity = (target - this.transform.position).normalized;

        //이동방향으로 부드러운 회전을 하기위해 선형보간 사용
        transform.forward= Vector3.Lerp(this.transform.forward,velocity,10*Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * moveSpeed;
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
