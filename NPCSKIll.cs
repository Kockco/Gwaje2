using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSkill : NPCFSMState
{

    public float SkillTime = 1.0f;
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
        if (!GameLib.DetectCharacter(_manager.Sight, _manager.PlayerCC))
        {
            _manager.SetState(NPCState.Stand);
            return;
        }
        time += Time.deltaTime;
        if (time > SkillTime)
        {
            _manager.SetState(NPCState.Attack);
        }

    }
}