using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MachineManager _machineMgr;
    private MouseMgr _mouseMgr;

    private void Awake()
    {
        _machineMgr= GetComponent<MachineManager>();
        _mouseMgr= GetComponent<MouseMgr>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(_mouseMgr.Target)
                _machineMgr.ChagneState(MachineManager.StateType.Move);
        }
    }
}
