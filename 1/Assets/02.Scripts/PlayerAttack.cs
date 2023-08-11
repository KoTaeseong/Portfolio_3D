using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    public enum AttackStep
    {
        None,
        ready,
        attack,
    }

    [SerializeField] float AttackRange;
    AttackStep step= AttackStep.None;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            step= AttackStep.ready;
        }

        if (step == AttackStep.ready)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.layer == 10)
                    {
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (step == AttackStep.ready)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}
