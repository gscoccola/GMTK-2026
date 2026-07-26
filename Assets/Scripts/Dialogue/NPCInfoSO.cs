using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCInfo", menuName = "NPCInfo")]

public class NPCInfoSO : ScriptableObject
{
    public NPCTaskDialog[] TaskDialogs;
}

[Serializable]
public class NPCTaskDialog
{
    public TaskId ID;

    public string[] Briefing;
    public string[] Pending;
    public string[] Succeded;
    public string[] Failed;
}