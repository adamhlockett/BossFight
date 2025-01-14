using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilisticMethod : DynamicAdjustmentMethod
{
    private void Start()
    {
        methodName = "Probabilistic";
    }

    public override void CheckForAdjustments()
    {
        //progression trajectories are:
        //how many times the player has lost - 1,
        //the health of the player - 2,
        //the health of the enemy - 2,
        //----- calculate health gap
        //how many hits the player has hit/missed - 3,
        //how many hits the enemy has hit/missed - 4,
        //----- calculate hit accuracy
        //how many times the player has retried - 5

        //establish probability of:
        //the player winning - 1 2,
        //the player losing - 1 2,
        //the player hitting their attack - 3,
        //the enemy hitting their attack - 4,
        //the player retrying the level - 5
    }

    public override void Adjust()
    {

    }
}
