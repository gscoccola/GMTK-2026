using UnityEngine;

public class MinigameState : State
{
    public Minigame minigame;

    public override void OnEnter()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        minigame.OnUpdate(deltaTime);
    }

    public override void OnExit()
    {
    }
}