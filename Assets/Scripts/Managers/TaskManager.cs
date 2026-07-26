//using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    [SerializeField] private float _initialTimer = 5f;
    [SerializeField] private float _taskTimer = 10f;
    [SerializeField] private float _npcTaskChance = 1f;

    [SerializeField] private Task[] _taskGameobjects;

    //[SerializeField] [ReadOnly] private List<Task> _availableShipTasks = new();
    //private List<Task> _assignedTasks = new();

    private Dictionary<TaskGiverId, NPC> _npcs = new();
    private Dictionary<TaskGiverId, List<Task>> _tasks = new();

    private List<TaskGiverId> _availableGivers = new();
    private List<TaskGiverId> _assignedGivers = new();
    [SerializeField][ReadOnly] private int _shipTasks;

    private float _timeleft;
    private bool _hasShuffled;

    protected override void Awake()
    {
        base.Awake();
        _tasks[TaskGiverId.Ship] = new List<Task>();
    }

    private void Start()
    {
        _timeleft = _initialTimer;

    }

    public void AddNPC(TaskGiverId npcId, NPC npc)
    {
        _npcs[npcId] = npc;
        _availableGivers.Add(npcId);
        _tasks[npcId] = new List<Task>();
    }

    public void AddTask(Task task)
    {
        _tasks[task.GiverId].Add(task);
    }

    public void OnTaskFinished(Task task, bool succeded)
    {
        if (task.GiverId != TaskGiverId.Ship)
        {
            _npcs[task.GiverId].SetMissionFinished(task.Id, succeded);
            _assignedGivers.Remove(task.GiverId);
            _availableGivers.Add(task.GiverId);
        }

        _tasks[task.GiverId].Remove(task);
        _tasks[task.GiverId].Add(task);

    }

    private void Update()
    {
        if (!_hasShuffled)
        {
            _hasShuffled = true;
            _availableGivers.Shuffle();
            _tasks[TaskGiverId.Ship].Shuffle();
            foreach (var id in _availableGivers)
            {
                _tasks[id].Shuffle();
                foreach (var task in _taskGameobjects)
                {
                    AddTask(task);
                }
            }
        }
        _timeleft -= Time.deltaTime;
        if (_timeleft < 0)
        {
            AssignNextTask();
            _timeleft = _taskTimer;
        }
    }

    private void AssignNextTask()
    {
        TaskGiverId giver;
        Task task = null;
        if (Random.Range(0, 1f) <= _npcTaskChance && _availableGivers.Count > 0)
        {
            if (_availableGivers.Count == 0) Debug.LogWarning("npc without tasks");
            giver = _availableGivers[Random.Range(0, _availableGivers.Count)];
            if(_tasks[giver].Count > 0)
            {
                task = _tasks[giver][0];

                _npcs[task.GiverId].AssignTask(task.Id);
                _availableGivers.Remove(giver);
                Debug.Log("task given from npc " + giver + " , " + task.Id);
            }
            
        }
        else if (_tasks[TaskGiverId.Ship].Count > 0)
        {
            giver = TaskGiverId.Ship;
            task = _tasks[TaskGiverId.Ship][0];
            Debug.Log("task given from npc " + giver);
        }
        else 
        {
            Debug.Log("no tasks available");
            return;
        }
        if (task!= null) task.Activate();
        _tasks[giver].Remove(task);
        _tasks[giver].Add(task);
    }

}

