using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMethod : DynamicAdjustmentMethod
{
    private void Start() { methodName = "Control"; }

    public override void CheckForAdjustments() { }

    public override void AdjustDifficulty() { }
}
