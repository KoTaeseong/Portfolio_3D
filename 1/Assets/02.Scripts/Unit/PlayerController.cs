using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MachineManager _machineManager;
    [SerializeField] private MouseMgr _mouseMgr;

    private void Awake()
    {
        _machineManager= GetComponent<MachineManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 우클릭시
        if(Input.GetMouseButton(1))
        {
            _machineManager.target = _mouseMgr.MouseClick();
            _machineManager.ChangeState(StateType.Move);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }


    private void FootL() { }
    private void FootR() { }
    private void Land() { }

    private void Hit() { }
}
