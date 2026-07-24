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

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Finish(true);
            Debug.Log("end");
        }
    }
}