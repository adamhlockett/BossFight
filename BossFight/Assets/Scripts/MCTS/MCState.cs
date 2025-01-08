using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCState : MonoBehaviour
{
    public int currentPlayer;

    public List<MCState> GetLegalMoves()
    {
        return null; // return all possible actions
    }

    public MCState Clone() 
    {
        return null; // return copy of state??
    }

    public bool IsTerminal()
    {
        return false; //return true if at end of tree
    }

    public MCState GetRandomNextState()
    {
        return null; // get next state at random
    }

    public int GetWinner()
    {
        return 0;
    }
}
