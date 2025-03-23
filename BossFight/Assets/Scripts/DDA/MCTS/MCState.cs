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
    public int stateHierarchy = 0;
    public bool enemyLandsHit;
    public List<MCState> nextPossibleStates = new List<MCState>();
    PlayDataSingleton p = PlayDataSingleton.instance;


    //public MCState() 
    //{
    //    if(stateHierarchy < 3) // depth limit
    //    {
    //        for (int i = 1; i < 7; i++) // all except idle state
    //        {
    //            MCState state = new MCState();
    //            state.stateNum = i;
    //            state.stateHierarchy = this.stateHierarchy++;
    //            nextPossibleStates.Add(state);
    //        }
    //    }
    //}

    public void AddPossibleNextStates()
    {
        nextPossibleStates.Clear();
        for (int i = 1; i < 7; i++) // all except idle state
        {
            MCState state = new MCState();
            state.stateNum = i;
            state.stateHierarchy = this.stateHierarchy++;
            nextPossibleStates.Add(state);
        }
    }

    public List<MCState> GetPossibleNextStates()
    {
        AddPossibleNextStates();

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
        AddPossibleNextStates();
        int i = Random.Range(1, nextPossibleStates.Count - 1);
        MCState randomState = nextPossibleStates[i];
        return randomState;
    }

    public bool GetWinner() // win is defined as enemy hitting player, loss is defined as enemy missing player, estimate chance of hit - how? //DONE?
    {
        float accuracy = 0f;

        if(this.stateNum < 4) accuracy = p.chargeHits / p.chargeAttacks; // is charge state
        else accuracy = p.attackHits / p.attackAttacks; // is attack state

        float prediction = Random.Range(0f, 10f);
        prediction /= 10f;

        //if(p.difficulty >= 1.4f)
        //{
        //
        //}
        //else if(p.difficulty < 1.4f && p.difficulty >= 1f)
        //{
        //
        //}
        //else if (p.difficulty < 1f && p.difficulty >= 0.6f)
        //{
        //    
        //}
        //else // < 0.6f
        //{
        //
        //}

        prediction /= (p.difficulty * 0.5f);
        if (prediction > 1f) prediction = 1f;

        if(prediction <= accuracy) enemyLandsHit = true;
        else enemyLandsHit = false;

        //NEED TO MODIFY THESE VALUES BASED ON PERCEIVED DIFFICULTY --------------------------------------------------------------------------------------------

        return enemyLandsHit;
    }
}
