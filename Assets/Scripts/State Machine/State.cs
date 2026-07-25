using UnityEngine;

public class State : MonoBehaviour
{
    protected StateMachine StateMachine { get; private set; }

    public void Initialize(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
        //gameObject.SetActive(false);
    }

    public virtual void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnExit()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }
}