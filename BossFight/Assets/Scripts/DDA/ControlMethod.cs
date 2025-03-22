using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMethod : DynamicAdjustmentMethod
{
    private void Start() { methodName = "Control"; enemyMat.color = new Color(125, 125, 0); }

    public override void CheckForAdjustments() { }

    public override void AdjustDifficulty() { }
}
