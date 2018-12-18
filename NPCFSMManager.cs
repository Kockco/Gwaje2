using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    Stand = 0,
    Idle,
    Walk,
    Run,
    Attack,
    Skill,
    Death
}

[RequireComponent(typeof(NPCStat))]
[ExecuteInEditMode]
public class NPCFSMManager : MonoBehaviour, IFSMManager
{
    private bool _isinit = false;
    public NPCState startState = NPCState.Stand;
    private Dictionary<NPCState, NPCFSMState> _states = new Dictionary<NPCState, NPCFSMState>();

    [SerializeField]
    private NPCState _currentState;
    public NPCState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    private CharacterController _cc;
    public CharacterController CC { get { return _cc; } }

    private CharacterController _playercc;
    public CharacterController PlayerCC { get { return _playercc; } }

    private Transform _monsterTransform;
    public Transform MonsterTransform { get { return _monsterTransform; } }

    private NPCStat _stat;
    public NPCStat Stat { get { return _stat; } }

    private Animator _anim;
    public Animator Anim { get { return _anim; } }

    private Camera _sight;
    public Camera Sight { get { return _sight; } }

    public int sightAspect = 3;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _stat = GetComponent<NPCStat>();
        _anim = GetComponentInChildren<Animator>();
        _sight = GetComponentInChildren<Camera>();

        _playercc = GameObject.FindGameObjectWithTag("Monster").GetComponent<CharacterController>();
        _monsterTransform = _playercc.transform;

        NPCState[] stateValues = (NPCState[])System.Enum.GetValues(typeof(NPCState));
        foreach (NPCState s in stateValues)
        {
            System.Type FSMType = System.Type.GetType("NPC" + s.ToString());
            NPCFSMState state = (NPCFSMState)GetComponent(FSMType);
            if (null == state)
            {
                state = (NPCFSMState)gameObject.AddComponent(FSMType);
            }

            _states.Add(s, state);
            state.enabled = false;
        }

    }

    public void SetState(NPCState newState)
    {
        if (_isinit)
        {
            _states[_currentState].enabled = false;
            _states[_currentState].EndState();
        }
        _currentState = newState;
        _states[_currentState].BeginState();
        _states[_currentState].enabled = true;
        _anim.SetInteger("CurrentState", (int)_currentState);
    }

    private void Start()
    {
        SetState(startState);
        _isinit = true;
    }

    private void OnDrawGizmos()
    {
        if (_sight != null)
        {
            Gizmos.color = Color.green;
            Matrix4x4 temp = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(
                _sight.transform.position,
                _sight.transform.rotation,
                Vector3.one
                );

            Gizmos.DrawFrustum(
                _sight.transform.position,
                _sight.fieldOfView,
                _sight.farClipPlane,
                _sight.nearClipPlane,
                _sight.aspect
                );

            Gizmos.matrix = temp;
        }
    }

    public void NotifyTargetKilled()
    {
        SetState(NPCState.Stand);
    }

    public void SetDeadState()
    {
        SetState(NPCState.Death);
    }
}
