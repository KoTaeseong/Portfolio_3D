using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMgr : MonoBehaviour
{
    public enum ClickObjectType
    {
        Ground,
        Object,
        Char,
        Enemy,
        UI
    }

    public Vector3 DirectionToMouse
    {
        get
        {
            return _directionToMouse;
        }
    }

    public float DistanceOfMouse
    {
        get
        {
            return _distanceOfMouse;
        }
    }

    private Vector3 _directionToMouse;
    private float _distanceOfMouse;

    private GameObject player;

    private Vector3 screenMousePosition;
    [SerializeField]private GameObject mousetIcon;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenMousePosition = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(screenMousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _directionToMouse = (hit.point - player.transform.position).normalized;
            _distanceOfMouse = Vector3.Distance(hit.point,player.transform.position);
        }
        else
        {
            _directionToMouse = default(Vector3);
            _distanceOfMouse = -1;
        }
    }

    private void LateUpdate()
    {
        mousetIcon.transform.position = screenMousePosition;
    }
}
