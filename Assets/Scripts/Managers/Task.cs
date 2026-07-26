using UnityEngine;

public class Task : MonoBehaviour
{
    public TaskId Id;
    public TaskGiverId GiverId;

    private void Start()
    {
        TaskManager.Instance.AddTask(this);
    }

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

    }
}


public enum TaskId
{
    CaptainBottle,
    CaptainHat,
    CaptainMusic,
}