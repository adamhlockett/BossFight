using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityHFSM
{
    public class EnemySpecial : EnemyStateBase
    {
        public EnemySpecial (
            bool needsExitTime,
            Enemy enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float exitTime = 0.33f)
            : base(needsExitTime, enemy, exitTime, onEnter)
        { }

        public override void OnEnter()
        {
            base.OnEnter();
            anim.Play("DOWN_special");
        }
    }
}
