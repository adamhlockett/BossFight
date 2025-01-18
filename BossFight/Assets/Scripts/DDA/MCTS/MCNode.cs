using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCNode
{
    public MCState state;
    public MCNode parent;
    public List<MCNode> children;
    public int wins, visits;

    public MCNode(MCState s_state, MCNode p_parent)
    {
        state = s_state;
        parent = p_parent;
        children = new List<MCNode>();
        wins = 0;
        visits = 0;
    }
}
