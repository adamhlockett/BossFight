using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityHFSM
{
    public class EnemyFly : EnemyStateBase
    {
        private Transform target;
        public EnemyFly(bool needsExitTime, Enemy enemy, Transform p_target) : base(needsExitTime, enemy)
        {
            this.target = p_target;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            anim.Play("DOWN_fly");
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (!RequestedExit)
            {
                Debug.Log("set destination of enemy");
            }
            //else if (within distance){
            // fsm.StateCanExit();
            //}
        }
    }
}
