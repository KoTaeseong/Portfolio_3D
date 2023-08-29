using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sequence : Node 
{
    /** Chiildren nodes that belong to this sequence */
    private List<Node> m_nodes = new List<Node>();

    //생성자로 자식노드들을 받는다.
    /** Must provide an initial set of children nodes to work */
    public Sequence(List<Node> nodes) 
    {
        m_nodes = nodes;
    }

    /* If any child node returns a failure, the entire node fails. Whence all 
     * nodes return a success, the node reports a success. */
    public override NodeStates Evaluate() 
    {
        bool anyChildRunning = false;
        //자식노드들을 순회
        foreach(Node node in m_nodes) 
        {
            //임의의 자식노드를 평가
            //<--자식노드 중 하나라도 false를 리턴하면 sequence도 false를 리턴하며 바로 종료
            switch (node.Evaluate()) 
            {
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;                    
                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
            }
        }
        m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
        return m_nodeState;
    }
}
