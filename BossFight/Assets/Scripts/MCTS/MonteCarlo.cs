using System.Collections.Generic;
using Unity.VisualScripting;
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

public class MCTS : MonoBehaviour
{
    public int maxIterations = 1000;

    public MCState FindBestMove(MCState currentState)
    {
        MCNode rootNode = new MCNode(currentState, null);

        for (int i = 0; i < maxIterations; i++)
        {
            MCNode selectedNode = Select(rootNode);
            MCNode expandedNode = Expand(selectedNode);
            int simulationResult = Simulate(expandedNode);
            Backpropagate(expandedNode, simulationResult);
        }

        return GetBestChildNode(rootNode).state;
    }

    private MCNode Select(MCNode rootNode)
    {
        MCNode node = rootNode;

        while(node.children.Count > 0)
        {
            float bestScore = float.MinValue;
            MCNode bestChild = null;

            foreach (MCNode child in node.children)
            {
                float ucb1Score = (float)child.wins / child.visits + 
                    Mathf.Sqrt(2 * Mathf.Log(node.visits) / child.visits); // upper confidence bound
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
        List<MCState> possibleStates = node.state.GetLegalMoves(); // replace with next possible actions

        foreach (MCState state in possibleStates)
        {
            MCNode child = new MCNode(state, node);
            node.children.Add(child);
        }

        return node.children[Random.Range(0, node.children.Count)]; // randomness
    }

    private int Simulate(MCNode node)
    {
        MCState currentState = node.state.Clone();

        while (!currentState.IsTerminal()) // means is end state
        {
            currentState = currentState.GetRandomNextState();
        }

        return currentState.GetWinner();
    }

    private void Backpropagate(MCNode node, int winner)
    {
        while (node != null)
        {
            node.visits++;
            if(node.state.currentPlayer == winner) { node.wins++; } // currentPlayer???????
            node = node.parent;
        }
    }

    private MCNode GetBestChildNode(MCNode rootNode)
    {
        int maxVisits = -1;
        MCNode bestChild = null;

        foreach (MCNode child in rootNode.children)
        {
            if (child.visits > maxVisits)
            {
                maxVisits = child.visits;
                bestChild = child;
            }
        }

        return bestChild;
    }
}


