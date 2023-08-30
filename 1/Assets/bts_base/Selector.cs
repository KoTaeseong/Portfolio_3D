using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : Node 
{
    /** The child nodes for this selector */
    protected List<Node> m_nodes = new List<Node>();

    //생성자로 자식노드들을 받는다.
    /** The constructor requires a lsit of child nodes to be 
     * passed in*/
    public Selector(List<Node> nodes) 
    {
        m_nodes = nodes;
    }

    /* If any of the children reports a success, the selector will
     * immediately report a success upwards. If all children fail,
     * it will report a failure instead.*/
    public override NodeStates Evaluate() 
    {
        //자식노드들을 순회
        foreach (Node node in m_nodes) 
        {
            //임의의 자식노드를 평가
            //<--자식노드 중 하나라도 true 리턴하면 selector도 true를 리턴하며 바로 종료
            switch (node.Evaluate()) 
            {
                case NodeStates.FAILURE:
                    continue;
                case NodeStates.SUCCESS:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
                case NodeStates.RUNNING:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
                default:
                    continue;
            }
        }
        m_nodeState = NodeStates.FAILURE;
        return m_nodeState;
    }
}
