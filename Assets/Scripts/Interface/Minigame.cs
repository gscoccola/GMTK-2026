using System;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public event Action<bool> OnMinigameFinished;
    protected bool finished = false;

    public virtual void StartMinigame()
    {
        gameObject.SetActive(true);
        finished = false;
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    protected void Finish(bool success)
    {
        if (finished)
        {
            return;
        }
        finished = true;
        gameObject.SetActive(false);
        OnMinigameFinished?.Invoke(success);
    }
}