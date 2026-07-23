using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [field: SerializeField] public State[] States { get; private set; }

    private State currentState;

    private void Awake()
    {
        foreach (var state in States)
            state.Initialize(this);
    }

    private void Start()
    {
        ChangeState(States.FirstOrDefault());
    }

    private void Update()
    {
        currentState?.OnUpdate(Time.deltaTime);
    }

    public void ChangeState(State newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    public void ChangeState<T>() where T : State
    {
        var newState = States.FirstOrDefault(s => s is T);
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}