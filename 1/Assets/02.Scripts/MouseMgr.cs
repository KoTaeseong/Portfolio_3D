using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MouseMgr : MonoBehaviour
{
    [SerializeField] public GameObject mouseImage;

    private Vector3 mousePosition;
    [SerializeField] private MachineManager manager;


    public Target MouseClick()
    {

        Target target;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            target = new Target();
            target.point = hit.point;
            target.gameObject = hit.collider.gameObject;
            target.targetType = (TargetType)hit.collider.gameObject.layer;
        }
        else
        {
            target = null;
        }

        return target;
    }


    private void Update()
    {
        mousePosition = Input.mousePosition;
    }

    private void LateUpdate()
    {
        mouseImage.transform.position = mousePosition;
    }

    private void OnDrawGizmos()
    {
        if (manager.target != default)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(manager.target.point, radius: 0.5f);
            Gizmos.DrawLine(manager.transform.position, manager.target.point);
        }
    }
}
