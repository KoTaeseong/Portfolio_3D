using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MouseMgr : MonoBehaviour
{
    public static MouseMgr instance
    {
        get
        {
            if(_instacne != null)
                return _instacne;
            _instacne = new MouseMgr();
            return _instacne;
        }
    }
    public static MouseMgr _instacne;


    [SerializeField] public GameObject mouseImage;

    private Vector3 mousePosition;


    public Target MouseClick()
    {
        Debug.Log("MouseClick");

        Target target = new Target();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hit");

            target.point = hit.point;
            target.gameObject = hit.collider.gameObject;
            target.targetType = 0;
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
