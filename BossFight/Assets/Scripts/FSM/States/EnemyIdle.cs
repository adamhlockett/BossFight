using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityHFSM
{
    public class EnemyIdle : EnemyStateBase
    {
        public EnemyIdle(bool needsExitTime, Enemy enemy) : base(needsExitTime, enemy){}

        public override void OnEnter()
        {
            base.OnEnter();
            anim.Play("DOWN_fly");
        }

        public override void OnLogic()
        {
            base.OnLogic();
            Debug.Log("is idling");
        }
    }
}
