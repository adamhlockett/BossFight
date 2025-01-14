using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//public class MCNode
//{
//    public MCState state;
//    public MCNode parent;
//    public List<MCNode> children;
//    public int wins, visits;
//
//    public MCNode(MCState s_state, MCNode p_parent)
//    {
//        state = s_state;
//        parent = p_parent;
//        children = new List<MCNode>();
//        wins = 0;
//        visits = 0;
//    }
//}

public class MCTS : DynamicAdjustmentMethod
{
    public int maxIterations = 5; // does this act as a depth limit????

    private void Start()
    {
        methodName = "Monte Carlo";
    }

    public override void Adjust()
    {
        
    }

    public MCState FindBestMove(MCState currentState) // called to start the process
    {
        MCNode rootNode = new MCNode(currentState, null); // creates the initial node of the tree

        for (int i = 0; i < maxIterations; i++)
        {
            MCNode selectedNode = Select(rootNode); // selects best possible node based on wins and visits
            MCNode expandedNode = Expand(selectedNode); // creates children based on the possible states that can be moved to from each state
            int simulationResult = Simulate(expandedNode); // perform a random playout from an expanded node, use the result to estimate the value of the expanded node
            Backpropagate(expandedNode, simulationResult); // update the visit and win count of each node along the chosen path back up to the root
        }

        return GetBestChildNode(rootNode).state; // return the next action node with the highest value (best action)
    }

    private MCNode Select(MCNode p_node)
    {
        MCNode node = p_node; 

        while(node.children.Count > 0) // only after the tree has been expanded atleast once
        {
            float bestScore = float.MinValue;
            MCNode bestChild = null;

            foreach (MCNode child in node.children)
            {
                float ucb1Score = (float)child.wins / child.visits + 
                    Mathf.Sqrt(2 * Mathf.Log(node.visits) / child.visits); // upper confidence bound, log returns the power (base e)
                if (ucb1Score > bestScore) 
                {
                    bestScore = ucb1Score;
                    bestChild = child;
                }
            }

            node = bestChild;
        }

        return node;
    }

    private MCNode Expand(MCNode node) 
    {
        List<MCState> possibleStates = node.state.GetPossibleNextStates();

        foreach (MCState state in possibleStates)
        {
            MCNode child = new MCNode(state, node);
            node.children.Add(child);
        }

        return node.children[Random.Range(0, node.children.Count)]; // randomly choose one of the children nodes to return
    }

    private int Simulate(MCNode node)
    {
        MCState currentState = node.state.Clone(); // presumably a copy of the current state ????????????????? does this need to be a copy?

        while (!currentState.IsTerminal()) // means is end state - could be field instead, brings us to the end of the tree
        {
            currentState = currentState.GetRandomNextState();
        }

        return currentState.GetWinner(); // playout
    }

    private void Backpropagate(MCNode node, int winner)
    {
        while (node != null) // while is NOT root node
        {
            node.visits++;
            if(node.state.currentPlayer == winner) { node.wins++; } // currentPlayer ??? - applies to a turn based two player game
            node = node.parent; // moves back up the tree
        }
    }

    private MCNode GetBestChildNode(MCNode rootNode)
    {
        int maxVisits = -1;
        MCNode bestChild = null;

        foreach (MCNode child in rootNode.children)
        {
            if (child.visits > maxVisits) // algorithm visits the better nodes more often, so more visits = better
            {
                maxVisits = child.visits; // useful for debugging
                bestChild = child;
            }
        }

        return bestChild;
    }
}


