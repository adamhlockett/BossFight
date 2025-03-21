using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateNumbers
{
    Idle = 0,
    Charge_Easy = 1,
    Charge_Med = 2,
    Charge_Hard = 3,
    Attack_Easy = 4,
    Attack_Med = 5,
    Attack_Hard = 6
}

public class MCState : MonoBehaviour
{
    public int stateNum; // corresponds to StateNumbers enum
    public bool enemyLandsHit;
    private List<MCState> nextPossibleStates = new List<MCState>();
    PlayDataSingleton p = PlayDataSingleton.instance;

    public List<MCState> GetPossibleNextStates()
    {
        for(int i = 1; i < 7; i++) // all except idle state
        {
            MCState state = new MCState();
            state.stateNum = i;
            nextPossibleStates.Add(state);
        }
        return nextPossibleStates;
    }

    public MCState Clone()  // return copy of state //DONE?
    {
        MCState state = new MCState();
        state = this;
        return state;
    }

    public bool IsTerminal() // do any states need to be terminal? depth limit limits the search //DONE?
    {
        return false; // return true if at end of tree
    }

    public MCState GetRandomNextState() 
    {
        int i = Random.Range(1, nextPossibleStates.Count);
        MCState randomState = nextPossibleStates[i];
        return randomState;
    }

    public bool GetWinner() // win is defined as enemy hitting player, loss is defined as enemy missing player, estimate chance of hit - how?
    {
        //get accuracies from singleton - may need to separate accuracies
        
        return enemyLandsHit;
    }
}
