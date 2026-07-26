using System;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public TaskId Id;
    public TaskGiverId GiverId;
    public UnityEvent OnActivate = new UnityEvent();
    [SerializeField] private GameObject Minigame;

    /*private void Start()
    {
        TaskManager.Instance.AddTask(this);
    }*/

    public void OnComplete()
    {
        TaskManager.Instance.OnTaskFinished(this, true);
    }

    public void OnFailed()
    {
        TaskManager.Instance.OnTaskFinished(this, false);
    }

    public void Activate()
    {
        OnActivate?.Invoke();
        if (GiverId == TaskGiverId.Ship) MinigameManager.Instance.StartMinigame(Minigame, Finished);
    }

    private void Finished(bool success) {
        TaskManager.Instance.OnTaskFinished(this, success);
        float timeDif = success ? 10f : -10f;
        CountdownManager.Instance.IncreaseTimer(timeDif);
    }
}


public enum TaskId
{
    CaptainBottle,
    CaptainHat,
    CaptainMusic,

    ChefStir,
    ChefTaste,
    ChefChop,

    Mech1, Mech2, Mech3,

    Sci1, Sci2, Sci3,

    Wpn1, Wpn2, Wpn3,

    Ali1, Ali2, Ali3,

    Minigame1, Minigame2, Minigame3, Minigame4, Minigame5, Minigame6,
}