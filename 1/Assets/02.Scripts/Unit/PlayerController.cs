using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MachineManager _machineManager;
    private MouseMgr _mouseMgr;

    private void Awake()
    {
        _machineManager= GetComponent<MachineManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _mouseMgr = MouseMgr.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            _machineManager.target = _mouseMgr.MouseClick();
        }
    }
}
