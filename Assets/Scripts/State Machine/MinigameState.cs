using UnityEngine;

public class MinigameState : State
{
    public Minigame minigame;

    public override void OnEnter()
    {
        InputManager.Instance.EnableDialogue();
        //InputManager.Instance.Actions.Minigame.Interact.performed += OnAdvance;
    }

    public override void OnUpdate(float deltaTime)
    {
        minigame.OnUpdate(deltaTime);
    }

    public override void OnExit()
    {
        //InputManager.Instance.Actions.Dialogue.Interact.performed -= OnAdvance;
    }
}