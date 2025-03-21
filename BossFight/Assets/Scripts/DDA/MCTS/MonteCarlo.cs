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
    public int depthLimit = 3;
    PlayDataSingleton p = PlayDataSingleton.instance;
    MCState bestState;
    EnemyStateMachine enemyFSM;

    private void Start()
    {
        methodName = "Monte Carlo";
        enemyFSM = GameObject.Find("Enemy").GetComponent<EnemyStateMachine>();
    }

    public override void CheckForAdjustments()
    {
        MCState currentState = new MCState();

        //if (enemyFSM.currentState.stateName == "Charge")
        //{
        //    switch ((int)p.difficulty)
        //    {
        //        case 0:
        //            currentState.stateNum = 0; // charge easy
        //            break;
        //
        //        case 1:
        //            currentState.stateNum = 1; // charge medium
        //            break;
        //
        //        default: //2+ difficulty 
        //            currentState.stateNum = 2; // charge hard
        //            break;
        //    }
        //}
        //else if (enemyFSM.currentState.stateName == "Attack")
        //{
        //    switch ((int)p.difficulty)
        //    {
        //        case 0:
        //            currentState.stateNum = 3; // attack easy
        //            break;
        //
        //        case 1:
        //            currentState.stateNum = 4; // attack medium
        //            break;
        //
        //        default: //2+ difficulty 
        //            currentState.stateNum = 5; // attack hard
        //            break;
        //    }
        //}
        //else return; // return if in idle state

        if (enemyFSM.currentState.stateName != "Idle") return;
        currentState.stateNum = 0;

        bestState = FindBestMove(currentState);

        AdjustDifficulty();
    }

    public override void AdjustDifficulty()
    {
        //calculation uses bestState
        switch (bestState.stateNum)
        {
            case 0: // charge easy
                p.difficulty = 0;
                //set next state as charge - FIND A WAY OF ADAPTING ENEMYFSM FOR THIS---------------------------------------------------------------------------------------------------
                ChangeStaffColour(p.difficulty);
                break;


            case 1: // charge med
                p.difficulty = 1;
                //set next state as charge----------------------------------------------------------
                ChangeStaffColour(p.difficulty);
                break;


            case 2: // charge hard
                p.difficulty = 2;
                //set next state as charge----------------------------------------------------------
                ChangeStaffColour(p.difficulty);
                break;


            case 3: // attack easy
                p.difficulty = 0;
                //set next state as attack----------------------------------------------------------
                ChangeStaffColour(p.difficulty);
                break;


            case 4: // attack med
                p.difficulty = 1;
                //set next state as attack----------------------------------------------------------
                ChangeStaffColour(p.difficulty);
                break;


            default: //5+ stateNum - 5 = attack hard
                p.difficulty = 2;
                //set next state as attack----------------------------------------------------------
                ChangeStaffColour(p.difficulty);
                break;
        }
    }

    public MCState FindBestMove(MCState currentState) // called to start the process
    {
        MCNode rootNode = new MCNode(currentState, null); // creates the initial node of the tree

        for (int i = 0; i < depthLimit; i++)
        {
            MCNode selectedNode = Select(rootNode); // selects best possible node based on wins and visits
            MCNode expandedNode = Expand(selectedNode); // creates children based on the possible states that can be moved to from each state
            bool simulationResult = Simulate(expandedNode); // perform a random playout from an expanded node, use the result to estimate the value of the expanded node
            Backpropagate(expandedNode, simulationResult); // update the visit and win count of each node along the chosen path back up to the root
        }

        return GetBestChildNode(rootNode).state; // return the next action node with the highest value (best action)
    }

    private MCNode Select(MCNode p_node) //DONE
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

    private MCNode Expand(MCNode node) //DONE
    {
        List<MCState> possibleStates = node.state.GetPossibleNextStates();

        foreach (MCState state in possibleStates)
        {
            MCNode child = new MCNode(state, node);
            node.children.Add(child);
        }

        return node.children[Random.Range(0, node.children.Count)]; // randomly choose one of the children nodes to return
    }

    private bool Simulate(MCNode node)
    {
        MCState currentState = node.state.Clone(); // presumably a copy of the current state ????????????????? does this need to be a copy? -------------------------

        while (!currentState.IsTerminal()) // means is end state - could be field instead, brings us to the end of the tree --------------------------------------------
        {
            currentState = currentState.GetRandomNextState();
        }

        return currentState.GetWinner(); // playout
    }

    private void Backpropagate(MCNode node, bool winner) //DONE
    {
        while (node != null) // while is NOT root node
        {
            node.visits++;
            if(winner) { node.wins++; }
            node = node.parent; // moves back up the tree
        }
    }

    private MCNode GetBestChildNode(MCNode rootNode) //DONE
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

    private void ChangeStaffColour(float d)
    {
        enemyMat.color = new Color(255 - (d * 255), d * 255, 0);
        //Scales a colour from green (0,255,0) to red (255,0,0) with the difficulty value.
    }
}
