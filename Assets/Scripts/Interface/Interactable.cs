using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private string interactionPrompt = "Interact";

    protected StateMachine stateMachine;

    public StateMachine StateMachine => stateMachine;

    private void Awake()
    {
        stateMachine = FindAnyObjectByType<StateMachine>();
    }

    public abstract void Interact(PlayerInteraction player);
}