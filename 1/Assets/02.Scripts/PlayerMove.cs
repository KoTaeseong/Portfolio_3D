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
                    Debug.Log("Click object");
                }

            }
        }

        velocity = (target - this.transform.position).normalized;
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
