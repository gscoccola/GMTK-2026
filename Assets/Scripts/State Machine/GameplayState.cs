using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayState : State
{
    private PlayerMovement playerMovement;
    private PlayerInteraction playerInteraction;

    private void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerInteraction = FindAnyObjectByType<PlayerInteraction>();
    }

    public override void OnEnter()
    {
        InputManager.Instance.EnableGameplay();
        InputManager.Instance.Actions.Player.Move.performed += playerMovement.Move;
        InputManager.Instance.Actions.Player.Move.canceled += playerMovement.Move;
        InputManager.Instance.Actions.Player.Interact.performed += OnInteract;
    }

    public override void OnUpdate(float deltaTime)
    {
        playerMovement.OnUpdate();
    }

    public override void OnExit()
    {
        InputManager.Instance.Actions.Player.Move.performed -= playerMovement.Move;
        InputManager.Instance.Actions.Player.Move.canceled -= playerMovement.Move;
        InputManager.Instance.Actions.Player.Interact.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        playerInteraction.OnInteract();
    }
}