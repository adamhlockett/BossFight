using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicAdjustmentMethod : MonoBehaviour
{
    public string methodName;
    public DynamicAdjuster adj;
    public Material enemyMat;

    public void Start()
    {
        adj = GameObject.Find("Dynamic Adjuster").GetComponent<DynamicAdjuster>();
    }

    public abstract void CheckForAdjustments();

    public abstract void AdjustDifficulty();
}
