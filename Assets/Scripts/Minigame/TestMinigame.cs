using UnityEngine;

public class TestMinigame : Minigame
{
    private float timer;

    public override void StartMinigame()
    {
        Debug.Log("Start");
        base.StartMinigame();
        timer = 2f;
    }

    public override void OnUpdate(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0)
        {
            Finish(true);
            Debug.Log("end");
        }
    }
}