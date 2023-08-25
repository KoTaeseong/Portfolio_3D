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


    public Target MouseClick()
    {

        Target target = new Target();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            target.point = hit.point;
            target.gameObject = hit.collider.gameObject;
            target.targetType = (TargetType)1;
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
}
