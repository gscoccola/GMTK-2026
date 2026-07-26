using UnityEngine;
using System.Collections.Generic;

public class NPC : Interactable
{
    [SerializeField] private TaskGiverId _npcId;

    [SerializeField] private NPCInfoSO _info;

    /*[SerializeField][ReadOnly]*/ private NPCTaskDialog _currentTaskDialog;
    [SerializeField][ReadOnly] private TaskId _currentTaskId;
    [SerializeField][ReadOnly] private bool _wasMissionBriefed;
    [SerializeField][ReadOnly] private TaskStatus _taskStatus;

    private Animator _animator;
    private GameObject _exclamationMark;

    private Dictionary<TaskId, NPCTaskDialog> _dialogDict = new();

    private void Awake()
    {
        //stateMachine = FindAnyObjectByType<StateMachine>();
        _exclamationMark = transform.GetChild(0).gameObject;

        foreach (var dialog in _info.TaskDialogs)
        {
            _dialogDict[dialog.ID] = dialog;
        }
    }


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _exclamationMark.SetActive(false);
        TaskManager.Instance.AddNPC(_npcId, this);
    }

    public void SetMissionFinished(TaskId taskId, bool succeded)
    {
        if (_currentTaskId != taskId) Debug.LogWarning("wrong task id");
        else OnTaskFinished(succeded);

    }

    private void OnTaskFinished(bool succeded)
    {
        if (succeded) _taskStatus = TaskStatus.Succeded;
        else _taskStatus = TaskStatus.Failed;
        SetInactive();
    }

    private void SetInactive()
    {
        _wasMissionBriefed = true;
        _animator.SetBool("IsActive", false);
        _exclamationMark.SetActive(false);
    }

    public void AssignTask(TaskId taskId)
    {
        if (_taskStatus == TaskStatus.Pending)
        {
            Debug.LogWarning("still has pending task");
            return;
        }
        _currentTaskId = taskId;
        _wasMissionBriefed = false;
        _taskStatus = TaskStatus.Pending;
        _animator.SetBool("IsActive", true);
        _exclamationMark.SetActive(true);
        _currentTaskDialog = _dialogDict[taskId];
    }

    public override void Interact(PlayerInteraction player)
    {
        if (!DialogueManager.Instance.HasLinesLoaded)
        {
            if (_taskStatus == TaskStatus.Unassigned) DialogueManager.Instance.SetDialogue(new string[1] {"get lost kid"});
            else if (!_wasMissionBriefed)
            {
                DialogueManager.Instance.SetDialogue(_currentTaskDialog.Briefing);
                _wasMissionBriefed = true;
            }
            else DialogueManager.Instance.SetDialogue(_currentTaskDialog.Pending);

            stateMachine.ChangeState<DialogueState>();
        }
        
        else
        {
            stateMachine.ChangeState<DialogueState>();
            DialogueManager.Instance.OnInteract(out bool dialogComplete);
            if (dialogComplete) SetInactive();
        }
    }
}

public enum TaskStatus
{
    Unassigned,
    Pending,
    Succeded,
    Failed,
}
public enum TaskGiverId
{
    Ship,
    Captain,
    Scientist,
    Mechanic,
    Weapons,
    Chef,
    Alien1,
    Alien2,
    Alien3,
}