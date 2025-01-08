using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCState : MonoBehaviour
{
    public int currentPlayer;

    public List<MCState> GetPossibleNextStates()
    {
        return null; // return all possible actions depending on state
    }

    public MCState Clone() 
    {
        return null; // return copy of state ??
    }

    public bool IsTerminal()
    {
        return false; // return true if at end of tree
    }

    public MCState GetRandomNextState()
    {
        return null; // get next state at random
    }

    public int GetWinner()
    {
        return 0; // playout needs to happen here, returns int ???, in current version, IF WINS, currentPlayer must equal simulationResult to add a win to the node
    }
}
