using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRun : NPCFSMState
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
        if (!GameLib.DetectCharacter(_manager.Sight, _manager.PlayerCC))
        {
            _manager.SetState(NPCState.Stand);
            return;
        }

        if (Vector3.Distance(_manager.MonsterTransform.position, transform.position) < _manager.Stat.AttackRange)
        {
            _manager.SetState(NPCState.Attack);
            return;
        }

        _manager.CC.CKMove(_manager.MonsterTransform.position, _manager.Stat);
    }
}
