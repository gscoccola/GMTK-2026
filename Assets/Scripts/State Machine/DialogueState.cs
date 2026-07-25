using UnityEngine.InputSystem;

public class DialogueState : State
{
    public override void OnEnter()
    {
        InputManager.Instance.EnableDialogue();
        InputManager.Instance.Actions.Dialogue.Interact.performed += OnAdvance;
    }

    public override void OnExit()
    {
        InputManager.Instance.Actions.Dialogue.Interact.performed -= OnAdvance;
    }

    private void OnAdvance(InputAction.CallbackContext ctx)
    {
        DialogueManager.Instance.OnSkipPerformed();
    }
}