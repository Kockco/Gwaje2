﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : NPCFSMState
{

    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    private void Update()
    {
        if (Vector3.Distance(_manager.MonsterTransform.position, transform.position) >= _manager.Stat.AttackRange)
        {
            _manager.SetState(NPCState.Run);
            return;
        }

    }
}
