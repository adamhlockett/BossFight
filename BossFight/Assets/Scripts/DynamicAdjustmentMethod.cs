using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicAdjustmentMethod : MonoBehaviour
{
    public string methodName;
    public DynamicAdjuster adj;

    public void Start()
    {
        adj = GameObject.Find("Dynamic Adjuster").GetComponent<DynamicAdjuster>();
    }

    public abstract void Adjust();
}
