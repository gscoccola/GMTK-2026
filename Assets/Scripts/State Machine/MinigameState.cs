using UnityEngine.InputSystem;

public class MinigameState : State
{
    [ReadOnly] public Minigame minigame;

    public override void OnEnter()
    {
        InputManager.Instance.Actions.Minigame.Interact.performed += OnInteract;
    }

    public override void OnUpdate(float deltaTime)
    {
        minigame.OnUpdate(deltaTime);
    }

    public override void OnExit()
    {
        minigame = null;
        InputManager.Instance.Actions.Dialogue.Interact.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        minigame.HandleInput();
    }
}