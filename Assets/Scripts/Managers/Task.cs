using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public TaskId Id;
    public TaskGiverId GiverId;
    public UnityEvent OnActivate = new UnityEvent();

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
    }
}


public enum TaskId
{
    CaptainBottle,
    CaptainBoots,
    CaptainMusic,
    ChefStir,
    ChefHold,
    ChefChop,
    MechanicPatch,
    MechanicReroute,
    MechanicCrank,
    ScientistStablize,
    ScientistCatch,
    ScientistReboot,
    WeaponsRealign,
    WeaponsOverride,
    WeaponsHold,
    AlienMove,
    AlienHold,
    AlienSign
}