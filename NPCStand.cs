using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStand: NPCFSMState {

    public float idleTime = 2.0f;
    private float time = 0.0f;

    public override void BeginState()
    {
        base.BeginState();

        time = 0.0f;
    }

    public override void EndState()
    {
        base.EndState();
    }

    private void Update()
    {
        if (GameLib.DetectCharacter(_manager.Sight, _manager.PlayerCC))
        {
            _manager.SetState(NPCState.Run);
            return;
        }

        time += Time.deltaTime;
        if (time > idleTime)
        {
            int rand = Random.Range(1, 2);
            if(rand == 1)
                 _manager.SetState(NPCState.Walk);
            else if (rand == 2)
                _manager.SetState(NPCState.Idle);
        }

    }
}
