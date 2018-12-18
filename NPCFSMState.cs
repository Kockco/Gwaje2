using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCFSMManager))]
public class NPCFSMState : MonoBehaviour
{

    protected NPCFSMManager _manager;

    private void Awake()
    {
        _manager = GetComponent<NPCFSMManager>();
    }

    public virtual void BeginState()
    {

    }

    public virtual void EndState()
    {

    }
}
